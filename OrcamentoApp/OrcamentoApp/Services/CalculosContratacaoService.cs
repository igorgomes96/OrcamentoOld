using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Services
{
    public static class CalculosContratacaoService
    {
        //Para cáculo de DSR, necessita-se da qtda de dias úteis e não úteis em cada mês.
        //Para isso, usa-se a tabela QtdaDias, que tem associada à ela uma escala de trabalho.
        //Foi criada a escala 1 (Dias úteis) e 2 (Dias não úteis) para esse cálculo
        public static IEnumerable<CalculoEventoContratacao> CalculaContratacaoCiclo(Contratacao cont, Ciclo ciclo)
        {
            Contexto db = new Contexto();
            if (cont == null || ciclo == null) return null;

            List<CalculoEventoContratacao> lista = new List<CalculoEventoContratacao>();
            PAT pat = db.PAT.Find(cont.CargaHoraria, cont.Filial.SindicatoCod);
            ConvenioMed conveMed = null;
            ConvenioOdo convOdo = null;
            Encargos encargos = db.Encargos.Find(cont.EmpresaCod);
            EscalaTrabalho diasUteis = db.EscalaTrabalho.Find(1), diasNaoUteis = db.EscalaTrabalho.Find(2);

            float salario = 0, salarioBase = cont.Salario, valorConvMed = 0, valorDep = 0, valorConvOdo = 0;
            float valorPasse = db.Cidade.Find(cont.CidadeNome).VTPasse, valorPat = pat.Valor, peric = 0;
            float adCondutor = (float)(salarioBase * 0.1), vtDescontado = 0, somaHEs = 0, somaHNs = 0, baseEncargos = 0;
            int qtdaPasses = db.CargaHoraria.Find(cont.CargaHoraria).QtdaPassesDiario;

            if (cont.ConvenioPlanoCod != null)
            {
                conveMed = db.ConvenioMed.Find(cont.ConvenioPlanoCod);
                valorConvMed = (float)conveMed.Valor;
                valorDep = (float)conveMed.ValorDependentes;
            }

            if (cont.ConvenioOdoCod != null)
            {
                convOdo = db.ConvenioOdo.Find(cont.ConvenioOdoCod);
                valorConvOdo = convOdo.Valor;
            }

            foreach (MesOrcamento mes in ciclo.MesesOrcamento)
            {
                int numAno = mes.Mes.Year, numMes = mes.Mes.Month;
                int qtda = db.ContratacaoMes.Find(cont.Codigo, mes.Codigo).Qtda;
                int qtdaDias = db.QtdaDias.Find(numMes, cont.CodEscala).Qtda;

                QtdaDias qtDiasObj = db.QtdaDias.Find(numMes, diasUteis.Codigo);
                int qtdaDiasUteis = qtDiasObj == null ? 22 : qtDiasObj.Qtda;
                qtDiasObj = db.QtdaDias.Find(numMes, diasNaoUteis.Codigo);
                int qtdaDiasNaoUteis = qtDiasObj == null ? 8 : qtDiasObj.Qtda;

                //Horas Extras
                List<HEContratacao> hes = db.HEContratacao
                    .Where(x => x.ContratacaoCod == cont.Codigo && x.CodMesOrcamento == mes.Codigo)
                    .ToList();

                //Horas Noturnas
                List<AdNoturnoContratacao> hns = db.AdNoturnoContratacao
                    .Where(x => x.CodContratacao == cont.Codigo && x.CodMesOrcamento == mes.Codigo)
                    .ToList();


                //**************** REAJUSTES ****************
                Reajuste reajSalario = db.Reajuste.Find(numAno, numMes, cont.Filial.SindicatoCod);
                if (reajSalario != null) { 
                    salarioBase *= (float)(reajSalario.PercentualReajuste + 1);
                    peric = (float)(salarioBase * 0.3);
                    adCondutor = (float)(salarioBase * 0.1);
                    salario = salarioBase;
                }

                if (conveMed != null) { 
                    ReajConvenioMed reajConvMed = db.ReajConvenioMed.Find(numAno, numMes, cont.ConvenioPlanoCod);
                    if (reajConvMed != null) valorConvMed *= (float)(reajConvMed.PercentualReajuste + 1);
                }

                if (convOdo != null)
                {
                    ReajConvenioOdo reajConvenioOdo = db.ReajConvenioOdo.Find(numAno, cont.ConvenioOdoCod, numMes);
                    if (reajConvenioOdo != null) valorConvOdo *= (reajConvenioOdo.PercentualReajuste + 1);
                }

                ReajPAT reajPAT = db.ReajPAT.Find(numAno, numMes, cont.Filial.SindicatoCod);
                if (reajPAT != null)
                    valorPat *= (reajPAT.PercentualReajuste + 1);

                ReajVTPasse reajPasse = db.ReajVTPasse.Find(numAno, cont.CidadeNome, numMes);
                if (reajPasse != null)
                    valorPasse *= (float)(reajPasse.PercentualReajuste + 1);


                //****************** Cálculos ******************
                if (qtda > 0)
                {

                    //********************** SALÁRIOS E ADICIONAIS **********************
                    lista.Add(new CalculoEventoContratacao
                    {
                        CodEvento = "SALARIO",
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = mes.Codigo,
                        Valor = salario
                    });


                    if (cont.Periculosidade)
                        lista.Add(new CalculoEventoContratacao
                        {
                            CodEvento = "PERICUL",
                            CodContratacao = cont.Codigo,
                            CodMesOrcamento = mes.Codigo,
                            Valor = peric
                        });

                    if (cont.AdCondutor)
                        lista.Add(new CalculoEventoContratacao
                        {
                            CodEvento = "ADI-COND",
                            CodContratacao = cont.Codigo,
                            CodMesOrcamento = mes.Codigo,
                            Valor = adCondutor
                        });

                    somaHEs = 0;
                    hes.ForEach(x => {
                        somaHEs += salario / cont.CargaHoraria * x.QtdaHoras * x.PercentualHoras / 100;

                        lista.Add(new CalculoEventoContratacao
                        {
                            CodEvento = "HE-" + x.PercentualHoras.ToString(),
                            CodContratacao = cont.Codigo,
                            CodMesOrcamento = mes.Codigo,
                            Valor = somaHEs
                        });
                    });

                    somaHNs = 0;
                    hns.ForEach(x => {
                        somaHNs = salario / x.PercentualHoras / 100 / cont.CargaHoraria * x.QtdaHoras;
                        lista.Add(new CalculoEventoContratacao
                        {
                            CodEvento = "AD-N-" + x.PercentualHoras.ToString(),
                            CodContratacao = cont.Codigo,
                            CodMesOrcamento = mes.Codigo,
                            Valor = somaHNs
                        });
                    });


                    //************************** BENEFÍCIOS **************************
                    vtDescontado = (valorPasse * qtdaDias) - (float)(salarioBase * 0.6);
                    if (vtDescontado > 0)
                    {
                        lista.Add(new CalculoEventoContratacao
                        {
                            CodEvento = "VT-PASSE",
                            CodContratacao = cont.Codigo,
                            CodMesOrcamento = mes.Codigo,
                            Valor = vtDescontado
                        });
                    }

                    lista.Add(new CalculoEventoContratacao
                    {
                        CodEvento = "PAT",
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = mes.Codigo,
                        Valor = valorPat * pat.Percentual
                    });

                    lista.Add(new CalculoEventoContratacao
                    {
                        CodEvento = "CONV-MED",
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = mes.Codigo,
                        Valor = valorConvMed
                    });

                    lista.Add(new CalculoEventoContratacao
                    {
                        CodEvento = "CONV-ODO",
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = mes.Codigo,
                        Valor = valorConvOdo
                    });


                    //************************** ENCARGOS **************************
                    baseEncargos = salario + (cont.Periculosidade ? peric : 0) + (cont.AdCondutor ? adCondutor : 0)  + somaHEs + somaHNs;

                    lista.Add(new CalculoEventoContratacao
                    {
                        CodEvento = "INSS",
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = mes.Codigo,
                        Valor = baseEncargos * encargos.INSS
                    });

                    lista.Add(new CalculoEventoContratacao
                    {
                        CodEvento = "INCRA",
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = mes.Codigo,
                        Valor = baseEncargos * encargos.INCRA
                    });

                    lista.Add(new CalculoEventoContratacao
                    {
                        CodEvento = "SAL-EDU",
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = mes.Codigo,
                        Valor = baseEncargos * encargos.SalEducacao
                    });

                    lista.Add(new CalculoEventoContratacao
                    {
                        CodEvento = "SEBRAE",
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = mes.Codigo,
                        Valor = baseEncargos * encargos.Sebrae
                    });

                    lista.Add(new CalculoEventoContratacao
                    {
                        CodEvento = "SESI",
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = mes.Codigo,
                        Valor = baseEncargos * encargos.SESI
                    });

                }
            }

            return lista;
        }
    }
}
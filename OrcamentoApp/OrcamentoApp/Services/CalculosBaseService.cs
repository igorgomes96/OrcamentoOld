using OrcamentoApp.Exceptions;
using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Services
{
    public static class CalculosBaseService
    {
        //O valor do Convênio Médico e do Convênio Odontológico será um valor aberto, na base.
        //As premissas serão somente para novas contratações
        public static IEnumerable<CalculoEventoBase> CalculaFuncionarioCiclo(Funcionario func, Ciclo ciclo)
        {
            Contexto db = new Contexto();

            List<CalculoEventoBase> lista = new List<CalculoEventoBase>();

            PAT pat = db.PAT.Find(func.CargaHoraria, func.SindicatoCod);
            if (pat == null) throw new PATNaoCadastradaException();
            Encargos encargos = db.Encargos.Find(func.EmpresaCod);
            if (encargos == null) throw new EncargosNaoEncontradosException();

            //db.Database.ExecuteSqlCommand("insert into CalculoEventoBase (CodEvento, MatriculaFuncionario, CodMesOrcamento, Valor) select CodEvento, MatriculaFuncionario, CodMesOrcamento, Valor from ValoresAbertosBase a inner join MesOrcamento b on a.CodMesOrcamento = b.Codigo where MatriculaFuncionario = {0} and b.CicloCod = {1}", func.Matricula, ciclo.Codigo);

            float baseEncargos = 0;
            float somaHEs, somaHNs;
            float salario = func.Salario, salarioBase = func.Salario;
            float valorConvMed = func.ValorConvMedico;
            float valorConvOdo = func.ValorConvOdontologico;
            float passe = func.Filial.Cidade.VTPasse;
            float frete = func.Filial.Cidade.VTFretadoValor;
            int qtdaPasses = db.CargaHoraria.Find(func.CargaHoraria).QtdaPassesDiario;

            foreach (MesOrcamento m in ciclo.MesesOrcamento)
            {

                int ano = m.Mes.Year, mes = m.Mes.Month;
                int qtdaDias = db.QtdaDias.Find(mes, func.CodEscalaTrabalho).Qtda;

                //Reajustes
                Reajuste reajSal = db.Reajuste.Find(ano, mes, func.SindicatoCod);
                ReajConvenioMed reajMed = db.ReajConvenioMed.Find(ano, mes, func.ConvenioMedCod);
                ReajConvenioOdo reajOdo = db.ReajConvenioOdo.Find(ano, func.CodConvenioOdo, mes);
                ReajVTFretado reajVTFretado = db.ReajVTFretado.Find(ano, func.CidadeNome, mes);
                ReajVTPasse reajVTPasse = db.ReajVTPasse.Find(ano, func.CidadeNome, mes);

                if (reajSal != null)
                {
                    salarioBase *= (float)(reajSal.PercentualReajuste + 1);
                    if (salarioBase < reajSal.PisoSalarial) salarioBase = reajSal.PisoSalarial;
                }

                if (reajMed != null)
                    valorConvMed *= (float)(reajMed.PercentualReajuste + 1);

                if (reajOdo != null)
                    valorConvOdo *= (reajOdo.PercentualReajuste + 1);

                if (reajVTFretado != null)
                    frete *= (float)(reajVTFretado.PercentualReajuste + 1);

                if (reajVTPasse != null)
                    passe *= (float)(reajVTPasse.PercentualReajuste + 1);

                //Horas Extras
                List<HEBase> hes = db.HEBase
                    .Where(x => x.FuncionarioMatricula == func.Matricula && x.CodMesOrcamento == m.Codigo)
                    .ToList();

                //Horas Noturnas
                List<AdNoturnoBase> hns = db.AdNoturnoBase
                    .Where(x => x.FuncionarioMatricula == func.Matricula && x.CodMesOrcamento == m.Codigo)
                    .ToList();

                FuncionarioFerias ferias = func.FuncionarioFerias.Where(x => x.CodMesOrcamento == m.Codigo).FirstOrDefault();

                if (ferias == null)
                    salario = salarioBase;
                else
                    salario = salarioBase * (30 - ferias.QtdaDias) / 30;   //Encontra salário proporcional aos dias trabalhados

                // ********************** SALÁRIOS E ADICIONAIS **********************
                CalculoEventoBase calculoSalario = new CalculoEventoBase
                {
                    CodEvento = "SALARIO",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = salario
                };
                lista.Add(calculoSalario);

                CalculoEventoBase calculoPeric = new CalculoEventoBase { Valor = 0 };
                if (func.Periculosidade)
                {
                    calculoPeric.CodEvento = "PERICUL";
                    calculoPeric.MatriculaFuncionario = func.Matricula;
                    calculoPeric.CodMesOrcamento = m.Codigo;
                    calculoPeric.Valor = (float)(salario * 0.3);
                    lista.Add(calculoPeric);
                }

                CalculoEventoBase calculoAdCondutor = new CalculoEventoBase { Valor = 0 };
                if (func.AdCondutor) {
                    calculoAdCondutor.CodEvento = "ADI-COND";
                    calculoAdCondutor.MatriculaFuncionario = func.Matricula;
                    calculoAdCondutor.CodMesOrcamento = m.Codigo;
                    calculoAdCondutor.Valor = (float)(salario * 0.1);
                    lista.Add(calculoAdCondutor);
                }

                somaHEs = 0;
                hes.ForEach(x => {
                    somaHEs += salario / func.CargaHoraria * x.QtdaHoras * x.PercentualHoras / 100;

                    lista.Add(new CalculoEventoBase
                    {
                        CodEvento = "HE-" + x.PercentualHoras.ToString(),
                        MatriculaFuncionario = func.Matricula,
                        CodMesOrcamento = m.Codigo,
                        Valor = somaHEs
                    });
                });

                somaHNs = 0;
                hns.ForEach(x => {
                    somaHNs = salario / x.PercentualHoras / 100 / func.CargaHoraria * x.QtdaHoras;
                    lista.Add(new CalculoEventoBase
                    {
                        CodEvento = "AD-N-" + x.PercentualHoras.ToString(),
                        MatriculaFuncionario = func.Matricula,
                        CodMesOrcamento = m.Codigo,
                        Valor = somaHNs
                    });
                });


                //BASE DE ENCARGOS
                baseEncargos = calculoSalario.Valor + calculoPeric.Valor + calculoAdCondutor.Valor + somaHEs + somaHNs;


                // ********************** BENEFÍCIOS **********************
                float vtDescontado = 0;
                if (func.VTFretadoFlag) {
                    vtDescontado = frete - (float)(salarioBase * 0.6);
                    if (vtDescontado > 0) 
                        lista.Add(new CalculoEventoBase
                        {
                            CodEvento = "VT-FRETE",
                            MatriculaFuncionario = func.Matricula,
                            CodMesOrcamento = m.Codigo,
                            Valor = vtDescontado
                        });
                }
                else {
                    vtDescontado = (passe * qtdaDias) - (float)(salarioBase * 0.6);
                    if (vtDescontado > 0)
                        lista.Add(new CalculoEventoBase
                        {
                            CodEvento = "VT-PASSE",
                            MatriculaFuncionario = func.Matricula,
                            CodMesOrcamento = m.Codigo,
                            Valor = vtDescontado
                        });
                }

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "PAT",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = pat.Valor * pat.Percentual
                });

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "AUX-CRE",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = func.AuxCreche
                });

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "PREV-PRI",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = func.PrevidenciaPrivada
                });

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "CONV-MED",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = func.ValorConvMedico
                });

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "CONV-ODO",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = func.ValorConvOdontologico
                });



                // ********************** ENCARGOS **********************
                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "INSS",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = baseEncargos * encargos.INSS
                });

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "INCRA",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = baseEncargos * encargos.INCRA
                });

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "SAL-EDU",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = baseEncargos * encargos.SalEducacao
                });

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "SENAI",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = baseEncargos * encargos.Senai
                });

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "SEBRAE",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = baseEncargos * encargos.Sebrae
                });

                lista.Add(new CalculoEventoBase
                {
                    CodEvento = "SESI",
                    MatriculaFuncionario = func.Matricula,
                    CodMesOrcamento = m.Codigo,
                    Valor = baseEncargos * encargos.SESI
                });

            }
            return lista;
        }
    }
}
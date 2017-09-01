using OrcamentoApp.Exceptions;
using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrcamentoApp.DTO
{
    public partial class HistoricoCRsFuncionario {
        public string MatriculaFuncionario { get; set; }
        public string CR { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
    }

    public partial class FuncionarioDTO
    {

        public void SetFuncionario(Funcionario f)
        {
            if (f == null) return;
            Matricula = f.Matricula;
            AuxCreche = f.AuxCreche;
            AuxEducacao = f.AuxEducacao;
            AuxFilhoExc = f.AuxFilhoExc;
            PrevidenciaPrivada = f.PrevidenciaPrivada;
            AuxSeguro = f.AuxSeguro;
            AvosFerias = f.AvosFerias;
            DataAdmissao = f.DataAdmissao;
            MesDesligamento = f.MesDesligamento;
            Nome = f.Nome;
            Periculosidade = f.Periculosidade;
            QtdaDependentes = f.QtdaDependentes;
            QtdaDiasVendidosFerias = f.QtdaDiasVendidosFerias;
            Salario = f.Salario;
            TipoAviso = f.TipoAviso;
            CentroCustoCod = f.CentroCustoCod;
            CidadeNome = f.CidadeNome;
            ConvenioMedCod = f.ConvenioMedCod;
            EmpresaCod = f.EmpresaCod;
            CargaHoraria = f.CargaHoraria;
            Situacao = f.Situacao;
            CargoCod = f.CargoCod;
            SindicatoCod = f.SindicatoCod;
            ValorConvMedico = f.ValorConvMedico;
            ValorConvOdontologico = f.ValorConvOdontologico;
            AdCondutor = f.AdCondutor;
            VTFretadoFlag = f.VTFretadoFlag;
            CodEscalaTrabalho = f.CodEscalaTrabalho;
            CodConvenioOdo = f.CodConvenioOdo;
        }
        public FuncionarioDTO(Funcionario f)
        {
            SetFuncionario(f);
            Historico = GetHistoricoGeral(f);
        }

        public FuncionarioDTO(Funcionario f, Ciclo c)
        {
            SetFuncionario(f);

            Historico = GetHistoricoGeral(f)
                .Where(x => (x.Inicio == null && x.Fim > c.DataInicio) ||
                (x.Fim == null && x.Inicio < c.DataFim.Value) ||
                (x.Inicio < c.DataFim && x.Fim > c.DataInicio));
            

        }

        public FuncionarioDTO(Funcionario f, string cr)
        {
            SetFuncionario(f);
            Historico = GetHistoricoGeral(f).Where(x => x.CR == cr);

        }

        public FuncionarioDTO(Funcionario f, Ciclo c, string cr)
        {
            SetFuncionario(f);

            Historico = GetHistoricoGeral(f)
                .Where(x => x.CR == cr && ((x.Inicio == null && x.Fim > c.DataInicio) ||
                (x.Fim == null && x.Inicio < c.DataFim.Value) ||
                (x.Inicio < c.DataFim && x.Fim > c.DataInicio)));


        }


        private HashSet<HistoricoCRsFuncionario> GetHistoricoGeral(Funcionario f)
        {
            Contexto db = new Contexto();
            HashSet<HistoricoCRsFuncionario> retorno = new HashSet<HistoricoCRsFuncionario>();
            IEnumerable<Transferencia> transfs = db.Transferencia
                .Where(x => x.Aprovado.Value && x.FuncionarioMatricula == f.Matricula)
                .OrderBy(x => x.MesOrcamento.Mes)
                .ToList();

            //Adiciona o primeiro
            if (transfs.Count() > 0)
            {
                HistoricoCRsFuncionario anterior = new HistoricoCRsFuncionario
                {
                    MatriculaFuncionario = f.Matricula,
                    CR = transfs.First().CROrigem
                };
                retorno.Add(anterior);

                //Adiciona os demais
                foreach (Transferencia t in transfs)
                {
                    if (anterior != null && anterior.CR != t.CROrigem)
                    {
                        throw new TransferenciaInconsistenteException();
                    }

                    anterior.Fim = (new DateTime(t.MesOrcamento.Mes.Year, t.MesOrcamento.Mes.Month, 1)).AddDays(-1);

                    HistoricoCRsFuncionario h = new HistoricoCRsFuncionario
                    {
                        MatriculaFuncionario = f.Matricula,
                        CR = t.CRDestino,
                        Inicio = t.MesOrcamento.Mes
                    };

                    retorno.Add(h);
                    anterior = h;
                }
            }

            return retorno;
        }




        public string Matricula { get; set; }
        public float AuxCreche { get; set; }
        public float AuxEducacao { get; set; }
        public float AuxFilhoExc { get; set; }
        public float PrevidenciaPrivada { get; set; }
        public float AuxSeguro { get; set; }
        public int AvosFerias { get; set; }
        public System.DateTime DataAdmissao { get; set; }
        public Nullable<System.DateTime> MesDesligamento { get; set; }
        public string Nome { get; set; }
        public bool Periculosidade { get; set; }
        public int QtdaDependentes { get; set; }
        public int QtdaDiasVendidosFerias { get; set; }
        public float Salario { get; set; }
        public string TipoAviso { get; set; }
        public string CentroCustoCod { get; set; }
        public string CidadeNome { get; set; }
        public int EmpresaCod { get; set; }
        public int CargaHoraria { get; set; }
        public string Situacao { get; set; }
        public int CargoCod { get; set; }
        public int SindicatoCod { get; set; }
        public Nullable<int> ConvenioMedCod { get; set; }
        public float ValorConvMedico { get; set; }
        public float ValorConvOdontologico { get; set; }
        public bool AdCondutor { get; set; }
        public bool VTFretadoFlag { get; set; }
        public int CodEscalaTrabalho { get; set; }
        public Nullable<int> CodConvenioOdo { get; set; }
        public IEnumerable<HistoricoCRsFuncionario> Historico { get; set; }

    }
}

using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class SolicitacaoTHViewDTO
    {

        public SolicitacaoTHViewDTO(SolicitacaoTH s)
        {
            if (s == null) return;
            Codigo = s.Codigo;
            TipoSolicitacao = s.TipoSolicitacao;
            LoginSolicitante = s.LoginSolicitante;
            DataSolicitacao = s.DataSolicitacao;
            DescricaoSolicitacao = s.DescricaoSolicitacao;
            Parecer = s.Parecer;
            DataResposta = s.DataResposta;
            DescricaoResposta = s.DescricaoResposta;
            StatusSolicitacao = s.StatusSolicitacao;

            if (s.AlteracaoSalario != null)
                MatriculaFuncionario = s.AlteracaoSalario.FuncionarioMatricula;
            else if (s.Desligamento != null)
                MatriculaFuncionario = s.Desligamento.FuncionarioMatricula;
            else if (s.SolicitacaoAlteracaoCargo != null)
                MatriculaFuncionario = s.SolicitacaoAlteracaoCargo.FuncionarioMatricula;

            if (s.ACTs != null)
                ACTs = s.ACTs.ToList().Select(x => new ACTDTO(x));

            if (s.STHs != null)
                STHs = s.STHs.ToList().Select(x => new STHDTO(x));

        }

        public int Codigo { get; set; }
        public string TipoSolicitacao { get; set; }
        public string LoginSolicitante { get; set; }
        public System.DateTime DataSolicitacao { get; set; }
        public string DescricaoSolicitacao { get; set; }
        public string Parecer { get; set; }
        public Nullable<System.DateTime> DataResposta { get; set; }
        public string DescricaoResposta { get; set; }
        public string StatusSolicitacao { get; set; }
        public string MatriculaFuncionario { get; set; }
        public IEnumerable<ACTDTO> ACTs { get; set; }
        public IEnumerable<STHDTO> STHs { get; set; }
    }
}
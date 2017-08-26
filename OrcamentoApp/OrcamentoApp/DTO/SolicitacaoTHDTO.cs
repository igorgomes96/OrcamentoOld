using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class SolicitacaoTHDTO
    {
        public SolicitacaoTHDTO(SolicitacaoTH s)
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
    }
}
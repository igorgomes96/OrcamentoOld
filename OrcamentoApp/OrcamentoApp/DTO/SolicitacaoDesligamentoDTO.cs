using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class SolicitacaoDesligamentoDTO
    {

        public SolicitacaoDesligamentoDTO(SolicitacaoDesligamento s)
        {
            if (s == null) return;
            SolicitacaoCod = s.SolicitacaoCod;
            FuncionarioMatricula = s.FuncionarioMatricula;
            DataDesligamento = s.DataDesligamento;
            Motivo = s.Motivo;
            TipoAviso = s.TipoAviso;
        }
        public int SolicitacaoCod { get; set; }
        public string FuncionarioMatricula { get; set; }
        public System.DateTime DataDesligamento { get; set; }
        public string Motivo { get; set; }
        public string TipoAviso { get; set; }
    }
}
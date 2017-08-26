using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class SolicitacaoAlteracaoCargoDTO
    {
        public SolicitacaoAlteracaoCargoDTO(SolicitacaoAlteracaoCargo s)
        {
            if (s == null) return;
            SolicitacaoCod = s.SolicitacaoCod;
            FuncionarioMatricula = s.FuncionarioMatricula;
            CargoCodAnterior = s.CargoCodAnterior;
            CargoCodNovo = s.CargoCodNovo;
            EmpresaCodAnterior = s.EmpresaCodAnterior;
            CHAnterior = s.CHAnterior;
            EmpresaCodNovo = s.EmpresaCodNovo;
            CHNovo = s.CHNovo;
        }
        public int SolicitacaoCod { get; set; }
        public string FuncionarioMatricula { get; set; }
        public int CargoCodAnterior { get; set; }
        public int CargoCodNovo { get; set; }
        public int EmpresaCodAnterior { get; set; }
        public int CHAnterior { get; set; }
        public int EmpresaCodNovo { get; set; }
        public int CHNovo { get; set; }
    }
}
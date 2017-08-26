using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class SolicitacaoAlteracaoSalarioDTO
    {

        public SolicitacaoAlteracaoSalarioDTO(SolicitacaoAlteracaoSalario s)
        {
            if (s == null) return;
            SolicitacaoCod = s.SolicitacaoCod;
            FuncionarioMatricula = s.FuncionarioMatricula;
            SalarioAnterior = s.SalarioAnterior;
            SalarioNovo = s.SalarioNovo;
        }

        public int SolicitacaoCod { get; set; }
        public string FuncionarioMatricula { get; set; }
        public float SalarioAnterior { get; set; }
        public float SalarioNovo { get; set; }
    }
}
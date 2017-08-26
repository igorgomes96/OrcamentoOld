using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class HEBaseDTO
    {
        public HEBaseDTO(HEBase h)
        {
            if (h == null) return;
            FuncionarioMatricula = h.FuncionarioMatricula;
            CodMesOrcamento = h.CodMesOrcamento;
            PercentualHoras = h.PercentualHoras;
            QtdaHoras = h.QtdaHoras;
        }
        public string FuncionarioMatricula { get; set; }
        public int CodMesOrcamento { get; set; }
        public double PercentualHoras { get; set; }
        public Nullable<int> QtdaHoras { get; set; }
    
    }
}

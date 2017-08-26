using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class HEContratacaoDTO
    {

        public HEContratacaoDTO(HEContratacao h)
        {
            if (h == null) return;
            ContratacaoCod = h.ContratacaoCod;
            CodMesOrcamento = h.CodMesOrcamento;
            PercentualHoras = h.PercentualHoras;
            QtdaHoras = h.QtdaHoras;
        }
        public int ContratacaoCod { get; set; }
        public int CodMesOrcamento { get; set; }
        public double PercentualHoras { get; set; }
        public int QtdaHoras { get; set; }
    }
}

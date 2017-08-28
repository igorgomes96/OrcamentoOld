using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class FeriasPorCRDTO
    {
        public FeriasPorCRDTO() { }

        public FeriasPorCRDTO(FeriasPorCR f)
        {
            if (f == null) return;
            CodigoCR = f.CodigoCR;
            CodMesOrcamento = f.CodMesOrcamento;
            Percentual = f.Percentual;
        }
        public string CodigoCR { get; set; }
        public int CodMesOrcamento { get; set; }
        public float Percentual { get; set; }
    }
}
using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoAPI.DTO
{
    
    public partial class SindicatoDTO
    {
        public SindicatoDTO() { }
        public SindicatoDTO(Sindicato s)
        {
            if (s == null) return;
            NomeSindicato = s.NomeSindicato;
            Codigo = s.Codigo;
        }
    
        public string NomeSindicato { get; set; }
        public int Codigo { get; set; }
    
    }
}

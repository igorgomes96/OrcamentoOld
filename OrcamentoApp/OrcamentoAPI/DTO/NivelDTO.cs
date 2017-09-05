using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoAPI.DTO
{
    
    public partial class NivelDTO
    {
        public NivelDTO() { }
        public NivelDTO(Nivel n)
        {
            if (n == null) return;
            NivelCod = n.NivelCod;
            Nome = n.Nome;
        }
    
        public int NivelCod { get; set; }
        public string Nome { get; set; }
    
    }
}

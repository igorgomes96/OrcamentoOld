using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class NivelDTO
    {
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

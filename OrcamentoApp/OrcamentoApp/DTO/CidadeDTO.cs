using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class CidadeDTO
    {
        public CidadeDTO(Cidade c)
        {
            if (c == null) return;
            NomeCidade = c.NomeCidade;
            VTPasse = c.VTPasse;
            EhCapital = c.EhCapital;
        }
    
        public string NomeCidade { get; set; }
        public double VTPasse { get; set; }
        public bool EhCapital { get; set; }
    
    }
}

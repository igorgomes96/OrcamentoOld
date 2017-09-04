using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoAPI.DTO
{
    
    public partial class CidadeDTO
    {
        public CidadeDTO(Cidade c)
        {
            if (c == null) return;
            NomeCidade = c.NomeCidade;
            VTPasse = c.VTPasse;
            EhCapital = c.EhCapital;
            VTFretadoValor = c.VTFretadoValor;
            VTValorMensal = c.VTValorMensal;
        }
    
        public string NomeCidade { get; set; }
        public float VTPasse { get; set; }
        public bool EhCapital { get; set; }
        public float VTFretadoValor { get; set; }
        public Nullable<float> VTValorMensal { get; set; } 
    }
}

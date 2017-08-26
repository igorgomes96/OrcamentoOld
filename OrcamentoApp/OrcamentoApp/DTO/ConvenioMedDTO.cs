using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class ConvenioMedDTO
    {
        public ConvenioMedDTO(ConvenioMed c)
        {
            if (c == null) return;
            Codigo = c.Codigo;
            Plano = c.Plano;
            Valor = c.Valor;
            ValorDependentes = c.ValorDependentes;
        }
        
        public int Codigo { get; set; }
        public string Plano { get; set; }
        public double Valor { get; set; }
        public double ValorDependentes { get; set; }
    
    }
}

using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class CargoDTO
    {
        public CargoDTO(Cargo c)
        {
            if (c == null) return;
            NomeCargo = c.NomeCargo;
            TipoCargo = c.TipoCargo;
            Plano1Cod = c.Plano1Cod;
            Plano2Cod = c.Plano2Cod;
            Plano3Cod = c.Plano3Cod;
            Codigo = c.Codigo;
        }
    
        public string NomeCargo { get; set; }
        public string TipoCargo { get; set; }
        public Nullable<int> Plano1Cod { get; set; }
        public Nullable<int> Plano2Cod { get; set; }
        public Nullable<int> Plano3Cod { get; set; }
        public int Codigo { get; set; }
    
    }
}

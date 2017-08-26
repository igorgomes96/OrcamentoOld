using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class EncargosDTO
    {
        public EncargosDTO(Encargos e)
        {
            if (e == null) return;
            Enc13 = e.Enc13;
            AvisoPrevio = e.AvisoPrevio;
            Ferias = e.Ferias;
            FGTS = e.FGTS;
            INSS = e.INSS;
            EmpresaCod = e.EmpresaCod;
        }
        public double Enc13 { get; set; }
        public double AvisoPrevio { get; set; }
        public double Ferias { get; set; }
        public double FGTS { get; set; }
        public double INSS { get; set; }
        public double SistemaS { get; set; }
        public int EmpresaCod { get; set; }
    
    }
}

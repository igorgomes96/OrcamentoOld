using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoAPI.DTO
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
            INCRA = e.INCRA;
            SalEducacao = e.SalEducacao;
            Sebrae = e.Sebrae;
            Senai = e.Senai;
            SESI = e.SESI;
        }
        public float Enc13 { get; set; }
        public float AvisoPrevio { get; set; }
        public float Ferias { get; set; }
        public float FGTS { get; set; }
        public float INSS { get; set; }
        public float SistemaS { get; set; }
        public int EmpresaCod { get; set; }
        public float INCRA { get; set; }
        public float SalEducacao { get; set; }
        public float Sebrae { get; set; }
        public float Senai { get; set; }
        public float SESI { get; set; }

    }
}

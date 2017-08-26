using OrcamentoApp.Models;
using System;
using System.Collections.Generic;


namespace OrcamentoApp.DTO
{
    public partial class PATDTO
    {
        public PATDTO(PAT p)
        {
            if (p == null) return;
            CargaHoraria = p.CargaHoraria;
            Valor = p.Valor;
            SindicatoCod = p.SindicatoCod;
        }
        public int CargaHoraria { get; set; }
        public double Valor { get; set; }
        public int SindicatoCod { get; set; }

    }
}

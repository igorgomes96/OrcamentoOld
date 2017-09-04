using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;


namespace OrcamentoAPI.DTO
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
        public float Valor { get; set; }
        public int SindicatoCod { get; set; }

    }
}

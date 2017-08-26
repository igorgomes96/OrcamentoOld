using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class ReajusteDTO
    {
        public ReajusteDTO (Reajuste r)
        {
            if (r == null) return;
            Ano = r.Ano;
            PercentualReajuste = r.PercentualReajuste;
            MesFechamento = r.MesFechamento;
            PisoSalarial = r.PisoSalarial;
            SindicatoCod = r.SindicatoCod;
        }
        public int Ano { get; set; }
        public double PercentualReajuste { get; set; }
        public int MesFechamento { get; set; }
        public int MesReajuste { get; set; }
        public double PisoSalarial { get; set; }
        public int SindicatoCod { get; set; }
    
    }
}

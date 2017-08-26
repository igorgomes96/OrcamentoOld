using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class ReajConvenioMedDTO
    {
        public ReajConvenioMedDTO (ReajConvenioMed r)
        {
            if (r == null) return;
            Ano = r.Ano;
            ConvenioMedCod = r.ConvenioMedCod;
            PercentualReajuste = r.PercentualReajuste;
            MesReajuste = r.MesReajuste;
        }
        public int Ano { get; set; }
        public Nullable<int> ConvenioMedCod { get; set; }
        public double PercentualReajuste { get; set; }
        public Nullable<int> MesReajuste { get; set; }
    
    }
}

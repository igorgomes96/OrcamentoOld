using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoAPI.DTO
{
    
    public partial class ReajConvenioMedDTO
    {
        public ReajConvenioMedDTO() { }
        public ReajConvenioMedDTO (ReajConvenioMed r)
        {
            if (r == null) return;
            Ano = r.Ano;
            ConvenioMedCod = r.ConvenioMedCod;
            PercentualReajuste = r.PercentualReajuste;
            MesReajuste = r.MesReajuste;
            if (r.ConvenioMed != null) NomePlano = r.ConvenioMed.Plano;
        }
        public int Ano { get; set; }
        public Nullable<int> ConvenioMedCod { get; set; }
        public double PercentualReajuste { get; set; }
        public Nullable<int> MesReajuste { get; set; }
        public string NomePlano { get; set; }
    
    }
}

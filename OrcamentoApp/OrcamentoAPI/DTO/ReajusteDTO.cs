using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoAPI.DTO
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
            if (r.Sindicato != null) NomeSindicato = r.Sindicato.NomeSindicato;
        }
        public int Ano { get; set; }
        public double PercentualReajuste { get; set; }
        public int MesFechamento { get; set; }
        public int MesReajuste { get; set; }
        public float PisoSalarial { get; set; }
        public int SindicatoCod { get; set; }
        public string NomeSindicato { get; set; }
    
    }
}

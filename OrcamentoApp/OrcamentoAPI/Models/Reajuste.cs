//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrcamentoAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reajuste
    {
        public int Ano { get; set; }
        public double PercentualReajuste { get; set; }
        public int MesFechamento { get; set; }
        public int MesReajuste { get; set; }
        public float PisoSalarial { get; set; }
        public int SindicatoCod { get; set; }
    
        public virtual Sindicato Sindicato { get; set; }
    }
}
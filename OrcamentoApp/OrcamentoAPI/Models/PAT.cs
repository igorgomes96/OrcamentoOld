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
    
    public partial class PAT
    {
        public int CargaHoraria { get; set; }
        public float Valor { get; set; }
        public int SindicatoCod { get; set; }
        public float Percentual { get; set; }
    
        public virtual CargaHoraria CargaHorariaObj { get; set; }
        public virtual Sindicato Sindicato { get; set; }
    }
}
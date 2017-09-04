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
    
    public partial class Cargo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cargo()
        {
            this.Variaveis = new HashSet<Variaveis>();
        }
    
        public string NomeCargo { get; set; }
        public string TipoCargo { get; set; }
        public int Codigo { get; set; }
        public Nullable<int> Plano1Cod { get; set; }
        public Nullable<int> Plano2Cod { get; set; }
        public Nullable<int> Plano3Cod { get; set; }
        public string NomeCargo2 { get; set; }
        public string Tipo2 { get; set; }
    
        public virtual ConvenioMed Plano1 { get; set; }
        public virtual ConvenioMed Plano2 { get; set; }
        public virtual ConvenioMed Plano3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Variaveis> Variaveis { get; set; }
    }
}
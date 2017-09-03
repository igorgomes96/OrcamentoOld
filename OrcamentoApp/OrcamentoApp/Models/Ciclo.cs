//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrcamentoApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ciclo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ciclo()
        {
            this.MesesOrcamento = new HashSet<MesOrcamento>();
            this.Contratacao = new HashSet<Contratacao>();
        }
    
        public int Codigo { get; set; }
        public System.DateTime DataInicio { get; set; }
        public System.DateTime DataFim { get; set; }
        public Nullable<int> StatusCod { get; set; }
        public int TipoCod { get; set; }
        public string Descricao { get; set; }
    
        public virtual StatusCiclo StatusCiclo { get; set; }
        public virtual TipoCiclo TipoCiclo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MesOrcamento> MesesOrcamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contratacao> Contratacao { get; set; }
    }
}

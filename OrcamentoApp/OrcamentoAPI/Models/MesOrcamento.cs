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
    
    public partial class MesOrcamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MesOrcamento()
        {
            this.AdNoturnoBase = new HashSet<AdNoturnoBase>();
            this.AdNoturnoContratacao = new HashSet<AdNoturnoContratacao>();
            this.CalculoEventoBase = new HashSet<CalculoEventoBase>();
            this.CalculoEventoContratacao = new HashSet<CalculoEventoContratacao>();
            this.ContratacaoMes = new HashSet<ContratacaoMes>();
            this.FeriasPorCR = new HashSet<FeriasPorCR>();
            this.HEBase = new HashSet<HEBase>();
            this.HEContratacao = new HashSet<HEContratacao>();
            this.Simulacao = new HashSet<Simulacao>();
            this.ValoresAbertosBase = new HashSet<ValoresAbertosBase>();
            this.ValoresAbertosContratacao = new HashSet<ValoresAbertosContratacao>();
            this.ValoresAbertosCR = new HashSet<ValoresAbertosCR>();
            this.FuncionarioFerias = new HashSet<FuncionarioFerias>();
            this.Transferencia = new HashSet<Transferencia>();
        }
    
        public int Codigo { get; set; }
        public System.DateTime Mes { get; set; }
        public int CicloCod { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdNoturnoBase> AdNoturnoBase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdNoturnoContratacao> AdNoturnoContratacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalculoEventoBase> CalculoEventoBase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalculoEventoContratacao> CalculoEventoContratacao { get; set; }
        public virtual Ciclo Ciclo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContratacaoMes> ContratacaoMes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeriasPorCR> FeriasPorCR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HEBase> HEBase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HEContratacao> HEContratacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Simulacao> Simulacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValoresAbertosBase> ValoresAbertosBase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValoresAbertosContratacao> ValoresAbertosContratacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValoresAbertosCR> ValoresAbertosCR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FuncionarioFerias> FuncionarioFerias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transferencia> Transferencia { get; set; }
    }
}
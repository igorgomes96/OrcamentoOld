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
    
    public partial class CentroCusto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CentroCusto()
        {
            this.Receitas = new HashSet<Receita>();
            this.SolicitacaoContratacao = new HashSet<SolicitacaoContratacao>();
            this.Funcionario = new HashSet<Funcionario>();
            this.ValoresAbertosCR = new HashSet<ValoresAbertosCR>();
            this.FeriasPorCR = new HashSet<FeriasPorCR>();
            this.Simulacao = new HashSet<Simulacao>();
            this.TransferenciaOrigem = new HashSet<Transferencia>();
            this.TransferenciaDestino = new HashSet<Transferencia>();
            this.Contratacoes = new HashSet<Contratacao>();
        }
    
        public string Codigo { get; set; }
        public string Cliente { get; set; }
        public Nullable<System.DateTime> DataFim { get; set; }
        public System.DateTime DataInicio { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> EmpresaCod { get; set; }
        public Nullable<int> SetorCod { get; set; }
        public string Descricao2 { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        public virtual Setor Setor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receita> Receitas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitacaoContratacao> SolicitacaoContratacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funcionario> Funcionario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValoresAbertosCR> ValoresAbertosCR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeriasPorCR> FeriasPorCR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Simulacao> Simulacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transferencia> TransferenciaOrigem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transferencia> TransferenciaDestino { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contratacao> Contratacoes { get; set; }
    }
}

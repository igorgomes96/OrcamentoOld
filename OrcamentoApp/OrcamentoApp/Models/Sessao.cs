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
    
    public partial class Sessao
    {
        public string Chave { get; set; }
        public string LoginUsuario { get; set; }
        public System.DateTime Inicio { get; set; }
        public System.DateTime Fim { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}

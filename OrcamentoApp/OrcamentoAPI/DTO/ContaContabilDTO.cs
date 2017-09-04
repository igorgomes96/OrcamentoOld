using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoAPI.DTO
{
    
    public partial class ContaContabilDTO
    {
        public ContaContabilDTO(ContaContabil c)
        {
            if (c == null) return;
            Codigo = c.Codigo;
            Nome = c.Nome;
        }
    
        public string Codigo { get; set; }
        public string Nome { get; set; }

    
    }
}

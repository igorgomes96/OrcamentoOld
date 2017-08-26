using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class EmpresaDTO
    {
        public EmpresaDTO(Empresa e)
        {
            if (e == null) return;
            Codigo = e.Codigo;
            Nome = e.Nome;
        }
    
        public int Codigo { get; set; }
        public string Nome { get; set; }
    
    }
}

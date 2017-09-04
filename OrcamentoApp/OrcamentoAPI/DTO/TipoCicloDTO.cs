using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class TipoCicloDTO
    {
        public TipoCicloDTO() { }
        public TipoCicloDTO(TipoCiclo t)
        {
            if (t == null) return;
            Codigo = t.Codigo;
            Nome = t.Nome;
        }
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}
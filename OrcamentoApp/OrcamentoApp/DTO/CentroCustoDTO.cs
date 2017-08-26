using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class CentroCustoDTO
    {
        public CentroCustoDTO(CentroCusto c)
        {
            if (c == null) return;
            Codigo = c.Codigo;
            Cliente = c.Cliente;
            DataFim = c.DataFim;
            Descricao = c.Descricao;
            Observacoes = c.Observacoes;
            Tipo = c.Tipo;
            EmpresaCod = c.EmpresaCod;
            SetorCod = c.SetorCod;
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
    
    }
}

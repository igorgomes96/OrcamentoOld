using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class ReceitaDTO
    {
        public ReceitaDTO(Receita r)
        {
            if (r == null) return;
            Atividade = r.Atividade;
            CentroCustoCod = r.CentroCustoCod;
            Mes = r.Mes;
            Valor = r.Valor;
        }
        public string Atividade { get; set; }
        public string CentroCustoCod { get; set; }
        public System.DateTime Mes { get; set; }
        public double Valor { get; set; }
    
    }
}

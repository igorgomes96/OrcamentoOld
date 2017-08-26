using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class ContratacaoDTO
    {
        public ContratacaoDTO(Contratacao c)
        {
            if (c == null) return;
            Codigo = c.Codigo;
            Insalubridade = c.Insalubridade;
            Motivo = c.Motivo;
            Periculosidade = c.Periculosidade;
            CentroCustoCod = c.CentroCustoCod;
            ConvenioPlanoCod = c.ConvenioPlanoCod;
            CargaHoraria = c.CargaHoraria;
            EmpresaCod = c.EmpresaCod;
            CidadeNome = c.CidadeNome;
            CargoCod = c.CargoCod;
            Salario = c.Salario;
        }
    
        public int Codigo { get; set; }
        public double Insalubridade { get; set; }
        public string Motivo { get; set; }
        public bool Periculosidade { get; set; }
        public string CentroCustoCod { get; set; }
        public Nullable<int> ConvenioPlanoCod { get; set; }
        public int CargaHoraria { get; set; }
        public int EmpresaCod { get; set; }
        public string CidadeNome { get; set; }
        public Nullable<int> CargoCod { get; set; }
        public float Salario { get; set; }
    
    }
}

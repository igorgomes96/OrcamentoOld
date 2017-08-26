using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class SalarioDTO
    {
        public SalarioDTO(Salario s)
        {
            if (s == null) return;
            CargoCod = s.CargoCod;
            CargaHoraria = s.CargaHoraria;
            EmpresaCod = s.EmpresaCod;
            CidadeNome = s.CidadeNome;
            Faixa1 = s.Faixa1;
            Faixa2 = s.Faixa2;
            Faixa3 = s.Faixa3;
            Faixa4 = s.Faixa4;
        }
        public int CargoCod { get; set; }
        public int CargaHoraria { get; set; }
        public int EmpresaCod { get; set; }
        public string CidadeNome { get; set; }
        public Nullable<float> Faixa1 { get; set; }
        public Nullable<float> Faixa2 { get; set; }
        public Nullable<float> Faixa3 { get; set; }
        public Nullable<float> Faixa4 { get; set; }
    }
}
using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class VariaveisDTO
    {
        public VariaveisDTO(Variaveis v)
        {
            if (v == null) return;
            CargaHoraria = v.CargaHoraria;
            EmpresaCod = v.EmpresaCod;
            ParticipacaoLucros = v.ParticipacaoLucros;
            RemuneracaoVariavel = v.RemuneracaoVariavel;
            PL = v.PL;
            PR = v.PR;
            CargoCod = v.CargoCod;
        }
    
        public int CargaHoraria { get; set; }
        public int EmpresaCod { get; set; }
        public double ParticipacaoLucros { get; set; }
        public double RemuneracaoVariavel { get; set; }
        public Nullable<double> PL { get; set; }
        public Nullable<double> PR { get; set; }
        public int CargoCod { get; set; }
    
    }
}

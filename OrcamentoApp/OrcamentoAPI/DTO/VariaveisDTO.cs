using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoAPI.DTO
{
    
    public partial class VariaveisDTO
    {
        public VariaveisDTO() { }
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
            if (v.Empresa != null) NomeEmpresa = v.Empresa.Nome;
            if (v.Cargo != null) NomeCargo = v.Cargo.NomeCargo;
        }
    
        public int CargaHoraria { get; set; }
        public int EmpresaCod { get; set; }
        public double ParticipacaoLucros { get; set; }
        public double RemuneracaoVariavel { get; set; }
        public float PL { get; set; }
        public double PR { get; set; }
        public int CargoCod { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeCargo { get; set; }
    
    }
}

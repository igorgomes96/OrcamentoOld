using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoAPI.DTO
{
    
    public partial class FilialDTO
    {
        public FilialDTO(Filial f)
        {
            if (f == null) return;
            EmpresaCod = f.EmpresaCod;
            CidadeNome = f.CidadeNome;
            FAP = f.FAP;
            SAT = f.SAT;
            SindicatoCod = f.SindicatoCod;
        }
    
        public int EmpresaCod { get; set; }
        public string CidadeNome { get; set; }
        public double FAP { get; set; }
        public double SAT { get; set; }
        public Nullable<int> SindicatoCod { get; set; }
    
    }
}

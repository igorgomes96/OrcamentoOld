using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class FilialDTO
    {
        public FilialDTO(Filial f)
        {
            if (f == null) return;
            EmpresaCod = f.EmpresaCod;
            CidadeNome = f.CidadeNome;
            AvisoPrevio = f.AvisoPrevio;
            FAP = f.FAP;
            SAT = f.SAT;
            SindicatoCod = f.SindicatoCod;
        }
    
        public int EmpresaCod { get; set; }
        public string CidadeNome { get; set; }
        public Nullable<double> AvisoPrevio { get; set; }
        public Nullable<double> FAP { get; set; }
        public Nullable<double> SAT { get; set; }
        public Nullable<int> SindicatoCod { get; set; }
    
    }
}

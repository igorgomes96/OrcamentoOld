using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class SetorDTO
    {
        public SetorDTO(Setor s)
        {
            if (s == null) return;
            Codigo = s.Codigo;
            NomeSetor = s.NomeSetor;
            Nivel = s.Nivel;
            SetorPai = s.SetorPai;
        }
    
        public int Codigo { get; set; }
        public string NomeSetor { get; set; }
        public int Nivel { get; set; }
        public Nullable<int> SetorPai { get; set; }
    
    }
}
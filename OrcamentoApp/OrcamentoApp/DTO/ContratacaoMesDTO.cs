using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class ContratacaoMesDTO
    {
        public ContratacaoMesDTO(ContratacaoMes c)
        {
            if (c == null) return;
            ContratacaoCod = c.ContratacaoCod;
            MesOrcamentoCod = c.MesOrcamentoCod;
            Qtda = c.Qtda;

        }
        public int ContratacaoCod { get; set; }
        public int MesOrcamentoCod { get; set; }
        public int Qtda { get; set; }
    
    }
}

using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class CargaHorariaDTO
    {
        public CargaHorariaDTO(CargaHoraria c)
        {
            if (c == null) return;
            CargaHorariaCod = c.CargaHorariaCod;
        }
    
        public int CargaHorariaCod { get; set; }
    }
}

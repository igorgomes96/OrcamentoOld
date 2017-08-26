using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class MesOrcamentoDTO
    {
        public MesOrcamentoDTO(MesOrcamento m)
        {
            if (m == null) return;
            Codigo = m.Codigo;
            Mes = m.Mes;
            CicloCod = m.CicloCod;
        }
        public int Codigo { get; set; }
        public System.DateTime Mes { get; set; }
        public Nullable<int> CicloCod { get; set; }
    }
}
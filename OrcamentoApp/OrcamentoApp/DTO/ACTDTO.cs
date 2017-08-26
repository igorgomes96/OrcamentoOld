using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class ACTDTO
    {
        public ACTDTO (ACT a)
        {
            if (a == null) return;
            CodigoACT = a.CodigoACT;
            SolicitacaoCod = a.SolicitacaoCod;
            DataAbertura = a.DataAbertura;
        }

        public string CodigoACT { get; set; }
        public Nullable<int> SolicitacaoCod { get; set; }
        public Nullable<System.DateTime> DataAbertura { get; set; }
    }
}
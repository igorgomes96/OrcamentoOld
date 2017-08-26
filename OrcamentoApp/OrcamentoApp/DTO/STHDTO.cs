using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class STHDTO
    {
        public STHDTO(STH s)
        {
            if (s == null) return;
            CodigoSTH = s.CodigoSTH;
            SolicitacaoCod = s.SolicitacaoCod;
            DataAbertura = s.DataAbertura;
        }
        public string CodigoSTH { get; set; }
        public Nullable<int> SolicitacaoCod { get; set; }
        public Nullable<System.DateTime> DataAbertura { get; set; }
    }
}
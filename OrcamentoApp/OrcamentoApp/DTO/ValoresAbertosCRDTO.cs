using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class ValoresAbertosCRDTO
    {
        public ValoresAbertosCRDTO() { }

        public ValoresAbertosCRDTO(ValoresAbertosCR v)
        {
            if (v == null) return;
            CodEvento = v.CodEvento;
            CodMesOrcamento = v.CodMesOrcamento;
            CodigoCR = v.CodigoCR;
            Valor = v.Valor;
            if (v.MesOrcamento != null) Mes = v.MesOrcamento.Mes;
        }

        public string CodEvento { get; set; }
        public int CodMesOrcamento { get; set; }
        public string CodigoCR { get; set; }
        public float Valor { get; set; }
        public DateTime Mes { get; set; }
    }
}
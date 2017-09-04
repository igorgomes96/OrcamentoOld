using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class ValoresAbertosBaseDTO
    {
        public ValoresAbertosBaseDTO() { }

        public ValoresAbertosBaseDTO(ValoresAbertosBase v)
        {
            if (v == null) return;
            CodEvento = v.CodEvento;
            CodMesOrcamento = v.CodMesOrcamento;
            MatriculaFuncionario = v.MatriculaFuncionario;
            Valor = v.Valor;
            if (v.MesOrcamento != null) Mes = v.MesOrcamento.Mes;
        }
        public string CodEvento { get; set; }
        public int CodMesOrcamento { get; set; }
        public string MatriculaFuncionario { get; set; }
        public float Valor { get; set; }
        public DateTime Mes { get; set; }
    }
}
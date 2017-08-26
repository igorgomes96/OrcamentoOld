using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
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
        }
        public string CodEvento { get; set; }
        public int CodMesOrcamento { get; set; }
        public string MatriculaFuncionario { get; set; }
        public float Valor { get; set; }
    }
}
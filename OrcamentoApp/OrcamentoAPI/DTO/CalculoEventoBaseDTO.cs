using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class CalculoEventoBaseDTO
    {
        public CalculoEventoBaseDTO() { }
        public CalculoEventoBaseDTO(CalculoEventoBase c)
        {
            if (c == null) return;
            CodEvento = c.CodEvento;
            MatriculaFuncionario = c.MatriculaFuncionario;
            CodMesOrcamento = c.CodMesOrcamento;
            Valor = c.Valor;
        }
        public string CodEvento { get; set; }
        public string MatriculaFuncionario { get; set; }
        public int CodMesOrcamento { get; set; }
        public float Valor { get; set; }
    }
}
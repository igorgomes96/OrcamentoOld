using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class ValoresAbertosContratacaoDTO
    {
        public ValoresAbertosContratacaoDTO() { }
        public ValoresAbertosContratacaoDTO(ValoresAbertosContratacao v)
        {
            if (v == null) return;
            CodEvento = v.CodEvento;
            CodMesOrcamento = v.CodMesOrcamento;
            CodContratacao = v.CodContratacao;
            Valor = v.Valor;
        }
        public string CodEvento { get; set; }
        public int CodMesOrcamento { get; set; }
        public int CodContratacao { get; set; }
        public float Valor { get; set; }
    }
}
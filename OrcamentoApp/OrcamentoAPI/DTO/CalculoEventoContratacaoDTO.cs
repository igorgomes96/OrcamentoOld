﻿using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class CalculoEventoContratacaoDTO
    {
        public CalculoEventoContratacaoDTO() { }
        public CalculoEventoContratacaoDTO(CalculoEventoContratacao c)
        {
            if (c == null) return;
            CodContratacao = c.CodContratacao;
            CodEvento = c.CodEvento;
            CodMesOrcamento = c.CodMesOrcamento;
            Valor = c.Valor;
            if (c.MesOrcamento != null) Mes = c.MesOrcamento.Mes;
        }
        public int CodContratacao { get; set; }
        public string CodEvento { get; set; }
        public int CodMesOrcamento { get; set; }
        public float Valor { get; set; }
        public DateTime Mes { get; set; }
    }
}
using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class AdNoturnoContratacaoDTO
    {
        public AdNoturnoContratacaoDTO() { }
        public AdNoturnoContratacaoDTO(AdNoturnoContratacao a)
        {
            if (a == null) return;
            CodContratacao = a.CodContratacao;
            CodMesOrcamento = a.CodMesOrcamento;
            PercentualHoras = a.PercentualHoras;
            QtdaHoras = a.QtdaHoras;
        }
        public int CodContratacao { get; set; }
        public int CodMesOrcamento { get; set; }
        public int PercentualHoras { get; set; }
        public int QtdaHoras { get; set; }
    }
}
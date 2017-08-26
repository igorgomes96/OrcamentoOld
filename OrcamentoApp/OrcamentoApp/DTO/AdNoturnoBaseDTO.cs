using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class AdNoturnoBaseDTO
    {
        public AdNoturnoBaseDTO (AdNoturnoBase a)
        {
            if (a == null) return;
            FuncionarioMatricula = a.FuncionarioMatricula;
            CodMesOrcamento = a.CodMesOrcamento;
            PercentualHoras = a.PercentualHoras;
            QtdaHoras = a.QtdaHoras;
        }

        public string FuncionarioMatricula { get; set; }
        public int CodMesOrcamento { get; set; }
        public int PercentualHoras { get; set; }
        public int QtdaHoras { get; set; }
    }
}
using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class FuncionarioFeriasDTO
    {
        public FuncionarioFeriasDTO() { }
        public FuncionarioFeriasDTO(FuncionarioFerias f)
        {
            if (f == null) return;
            MatriculaFuncionario = f.MatriculaFuncionario;
            CodMesOrcamento = f.CodMesOrcamento;
            QtdaDias = f.QtdaDias;
            if (f.MesOrcamento != null) Mes = f.MesOrcamento.Mes;
        }
        public string MatriculaFuncionario { get; set; }
        public int CodMesOrcamento { get; set; }
        public int QtdaDias { get; set; }
        public DateTime Mes { get; set; }
    }
}
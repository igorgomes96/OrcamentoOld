using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class AfastamentoDTO
    {
        public AfastamentoDTO (Afastamento a)
        {
            if (a == null) return;
            DataFim = a.DataFim;
            MatriculaFuncionario = a.MatriculaFuncionario;
            DataInicio = a.DataInicio;
            Motivo = a.Motivo;
        }

        public System.DateTime DataInicio { get; set; }
        public string MatriculaFuncionario { get; set; }
        public Nullable<System.DateTime> DataFim { get; set; }
        public string Motivo { get; set; }
    
    }
}

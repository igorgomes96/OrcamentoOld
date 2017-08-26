using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class QtdaHorasMesDTO
    {
        public int CodMesOrcamento { get; set; }
        public DateTime Mes { get; set; }
        public int QtdaHoras { get; set; }
        public int PercentualHoras { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class EventoCicloDTO
    {
        public EventoCicloDTO() {
            ValoresMensais = new Dictionary<int, CalculoEventoBaseDTO>();
        }

        public string CodEvento {get; set; }
        public string NomeEvento { get; set; }
        public int CodCiclo { get; set; }
        public IDictionary<int, CalculoEventoBaseDTO> ValoresMensais { get; set; }

    }
}
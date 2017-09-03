using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class EventoCicloContratacaoDTO
    {
        public EventoCicloContratacaoDTO() {
            ValoresMensais = new Dictionary<int, CalculoEventoContratacaoDTO>();
        }

        public string CodEvento {get; set; }
        public string NomeEvento { get; set; }
        public int CodCiclo { get; set; }
        public IDictionary<int, CalculoEventoContratacaoDTO> ValoresMensais { get; set; }

    }
}
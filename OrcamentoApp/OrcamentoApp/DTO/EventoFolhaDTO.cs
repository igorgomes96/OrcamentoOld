using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class EventoFolhaDTO
    {
        public EventoFolhaDTO() { }
        public EventoFolhaDTO(EventoFolha e)
        {
            if (e == null) return;
            Codigo = e.Codigo;
            NomeEvento = e.NomeEvento;
            Descricao = e.Descricao;
            CodConta = e.CodConta;
        }
        public string Codigo { get; set; }
        public string NomeEvento { get; set; }
        public string Descricao { get; set; }
        public string CodConta { get; set; }
    }
}
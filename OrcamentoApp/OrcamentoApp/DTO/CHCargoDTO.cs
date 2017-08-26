using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class CHCargoDTO
    {
        public CHCargoDTO()
        {
            CHs = new List<int>();
        }

        public CHCargoDTO(Cargo c)
        {
            if (c == null) return;
            CHs = new List<int>();
            CargoCod = c.Codigo;
            CargoNome = c.NomeCargo;
            CHs = c.Variaveis.Select(x => x.CargaHoraria).Distinct().ToList();
        }
        public int CargoCod { get; set; }
        public string CargoNome { get; set; }
        public IEnumerable<int> CHs { get; set; }
    }
}
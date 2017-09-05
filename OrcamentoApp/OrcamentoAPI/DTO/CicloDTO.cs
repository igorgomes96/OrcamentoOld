﻿using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class CicloDTO
    {
        public CicloDTO()
        {
            Meses = new List<MesOrcamentoDTO>();
        }
        public CicloDTO (Ciclo c)
        {
            if (c == null) return;
            Codigo = c.Codigo;
            DataInicio = c.DataInicio;
            DataFim = c.DataFim;
            StatusCod = c.StatusCod;
            Descricao = c.Descricao;
            StatusNome = c.StatusCiclo.Nome;
            TipoCod = c.TipoCod;
            TipoNome = c.TipoCiclo.Nome;
            if (c.MesesOrcamento != null)
            {
                Meses = new List<MesOrcamentoDTO>();
                Meses = c.MesesOrcamento.ToList().Select(x => new MesOrcamentoDTO(x));
            }
            
        }

        public int Codigo { get; set; }
        public System.DateTime DataInicio { get; set; }
        public Nullable<System.DateTime> DataFim { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> StatusCod { get; set; }
        public string StatusNome { get; set; }
        public int TipoCod { get; set; }
        public string TipoNome { get; set; }
        public IEnumerable<MesOrcamentoDTO> Meses { get; set; }
    }
}
using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class SolicitacaoContratacaoDTO
    {
        public SolicitacaoContratacaoDTO(SolicitacaoContratacao s)
        {
            if (s == null) return;
            SolicitacaoCod = s.SolicitacaoCod;
            CidadeNome = s.CidadeNome;
            CargoCod = s.CargoCod;
            CargaHoraria = s.CargaHoraria;
            EmpresaCod = s.EmpresaCod;
            CRDestino = s.CRDestino;
            Salario = s.Salario;
            Qtda = s.Qtda;
            DataPrevista = s.DataPrevista;
        }

        public int SolicitacaoCod { get; set; }
        public string CidadeNome { get; set; }
        public int CargoCod { get; set; }
        public int CargaHoraria { get; set; }
        public int EmpresaCod { get; set; }
        public string CRDestino { get; set; }
        public float Salario { get; set; }
        public int Qtda { get; set; }
        public System.DateTime DataPrevista { get; set; }
    }
}
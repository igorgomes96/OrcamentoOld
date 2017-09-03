using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class ContratacaoDTO
    {
        public ContratacaoDTO(Contratacao c)
        {
            if (c == null) return;
            Codigo = c.Codigo;
            Motivo = c.Motivo;
            Periculosidade = c.Periculosidade;
            CentroCustoCod = c.CentroCustoCod;
            ConvenioPlanoCod = c.ConvenioPlanoCod;
            CargaHoraria = c.CargaHoraria;
            EmpresaCod = c.EmpresaCod;
            CidadeNome = c.CidadeNome;
            CargoCod = c.CargoCod;
            Salario = c.Salario;
            CicloCod = c.CicloCod;
            VTFretadoFlag = c.VTFretadoFlag;
            ConvenioOdoCod = c.ConvenioOdoCod;
            CodEscala = c.CodEscala;
            AdCondutor = c.AdCondutor;
            if (c.Variaveis != null) CargoNome = c.Variaveis.Cargo.NomeCargo;
        }

        public int Codigo { get; set; }
        public string Motivo { get; set; }
        public bool Periculosidade { get; set; }
        public string CentroCustoCod { get; set; }
        public int CargaHoraria { get; set; }
        public int EmpresaCod { get; set; }
        public string CidadeNome { get; set; }
        public int CargoCod { get; set; }
        public int CicloCod { get; set; }
        public Nullable<int> ConvenioPlanoCod { get; set; }
        public float Salario { get; set; }
        public bool VTFretadoFlag { get; set; }
        public Nullable<int> ConvenioOdoCod { get; set; }
        public int CodEscala { get; set; }
        public bool AdCondutor { get; set; }
        public string CargoNome { get; set; }

    }
}

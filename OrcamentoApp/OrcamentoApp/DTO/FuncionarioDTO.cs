using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class FuncionarioDTO
    {
        public FuncionarioDTO(Funcionario f)
        {
            if (f == null) return;
            Matricula = f.Matricula;
            AuxCreche = f.AuxCreche;
            AuxEducacao = f.AuxEducacao;
            AuxFilhoExc = f.AuxFilhoExc;
            PrevidenciaPrivada = f.PrevidenciaPrivada;
            AuxSeguro = f.AuxSeguro;
            AvosFerias = f.AvosFerias;
            DataAdmissao = f.DataAdmissao;
            MesDesligamento = f.MesDesligamento;
            MesFerias = f.MesFerias;
            Nome = f.Nome;
            Periculosidade = f.Periculosidade;
            QtdaDependentes = f.QtdaDependentes;
            QtdaDiasVendidosFerias = f.QtdaDiasVendidosFerias;
            Salario = f.Salario;
            TipoAviso = f.TipoAviso;
            VT = f.VT;
            CentroCustoCod = f.CentroCustoCod;
            CidadeNome = f.CidadeNome;
            ConvenioMedCod = f.ConvenioMedCod;
            EmpresaCod = f.EmpresaCod;
            CargaHoraria = f.CargaHoraria;
            Situacao = f.Situacao;
            CargoCod = f.CargoCod;
            SindicatoCod = f.SindicatoCod;
            ValorConvMedico = f.ValorConvMedico;
            ValorConvOdontologico = f.ValorConvOdontologico;
            AdCondutor = f.AdCondutor;
            VTFretadoFlag = f.VTFretadoFlag;
            CodEscalaTrabalho = f.CodEscalaTrabalho;
            CodConvenioOdo = f.CodConvenioOdo;

        }

        public string Matricula { get; set; }
        public float AuxCreche { get; set; }
        public float AuxEducacao { get; set; }
        public float AuxFilhoExc { get; set; }
        public float PrevidenciaPrivada { get; set; }
        public float AuxSeguro { get; set; }
        public int AvosFerias { get; set; }
        public System.DateTime DataAdmissao { get; set; }
        public Nullable<System.DateTime> MesDesligamento { get; set; }
        public Nullable<System.DateTime> MesFerias { get; set; }
        public string Nome { get; set; }
        public bool Periculosidade { get; set; }
        public int QtdaDependentes { get; set; }
        public int QtdaDiasVendidosFerias { get; set; }
        public float Salario { get; set; }
        public string TipoAviso { get; set; }
        public float VT { get; set; }
        public string CentroCustoCod { get; set; }
        public string CidadeNome { get; set; }
        public int EmpresaCod { get; set; }
        public int CargaHoraria { get; set; }
        public string Situacao { get; set; }
        public int CargoCod { get; set; }
        public int SindicatoCod { get; set; }
        public Nullable<int> ConvenioMedCod { get; set; }
        public float ValorConvMedico { get; set; }
        public float ValorConvOdontologico { get; set; }
        public bool AdCondutor { get; set; }
        public bool VTFretadoFlag { get; set; }
        public int CodEscalaTrabalho { get; set; }
        public Nullable<int> CodConvenioOdo { get; set; }

    }
}

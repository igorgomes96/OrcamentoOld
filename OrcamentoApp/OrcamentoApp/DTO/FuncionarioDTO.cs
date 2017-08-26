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
        }
    
        public string Matricula { get; set; }
        public double AuxCreche { get; set; }
        public double AuxEducacao { get; set; }
        public double AuxFilhoExc { get; set; }
        public double PrevidenciaPrivada { get; set; }
        public double AuxSeguro { get; set; }
        public int AvosFerias { get; set; }
        public System.DateTime DataAdmissao { get; set; }
        public Nullable<System.DateTime> MesDesligamento { get; set; }
        public Nullable<System.DateTime> MesFerias { get; set; }
        public string Nome { get; set; }
        public bool Periculosidade { get; set; }
        public Nullable<int> QtdaDependentes { get; set; }
        public Nullable<int> QtdaDiasVendidosFerias { get; set; }
        public double Salario { get; set; }
        public string TipoAviso { get; set; }
        public double VT { get; set; }
        public string CentroCustoCod { get; set; }
        public string CidadeNome { get; set; }
        public Nullable<int> ConvenioMedCod { get; set; }
        public int EmpresaCod { get; set; }
        public int CargaHoraria { get; set; }
        public string Situacao { get; set; }
        public Nullable<int> CargoCod { get; set; }
        public int SindicatoCod { get; set; }
    
    }
}

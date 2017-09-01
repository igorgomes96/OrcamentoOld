﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrcamentoApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Afastamento> Afastamento { get; set; }
        public virtual DbSet<CargaHoraria> CargaHoraria { get; set; }
        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Cidade> Cidade { get; set; }
        public virtual DbSet<ContaContabil> ContaContabil { get; set; }
        public virtual DbSet<Contratacao> Contratacao { get; set; }
        public virtual DbSet<DiasIndenizados> DiasIndenizados { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Encargos> Encargos { get; set; }
        public virtual DbSet<Nivel> Nivel { get; set; }
        public virtual DbSet<PAT> PAT { get; set; }
        public virtual DbSet<ReajConvenioMed> ReajConvenioMed { get; set; }
        public virtual DbSet<Receita> Receita { get; set; }
        public virtual DbSet<SalarioMinimo> SalarioMinimo { get; set; }
        public virtual DbSet<Sindicato> Sindicato { get; set; }
        public virtual DbSet<Sessao> Sessao { get; set; }
        public virtual DbSet<Permissao> Permissao { get; set; }
        public virtual DbSet<Setor> Setor { get; set; }
        public virtual DbSet<CentroCusto> CentroCusto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Ciclo> Ciclo { get; set; }
        public virtual DbSet<StatusCiclo> StatusCiclo { get; set; }
        public virtual DbSet<TipoCiclo> TipoCiclo { get; set; }
        public virtual DbSet<ContratacaoMes> ContratacaoMes { get; set; }
        public virtual DbSet<ConvenioMed> ConvenioMed { get; set; }
        public virtual DbSet<Importacao> Importacao { get; set; }
        public virtual DbSet<Salario> Salario { get; set; }
        public virtual DbSet<ACT> ACT { get; set; }
        public virtual DbSet<SolicitacaoAlteracaoSalario> SolicitacaoAlteracaoSalario { get; set; }
        public virtual DbSet<SolicitacaoContratacao> SolicitacaoContratacao { get; set; }
        public virtual DbSet<SolicitacaoDesligamento> SolicitacaoDesligamento { get; set; }
        public virtual DbSet<SolicitacaoTH> SolicitacaoTH { get; set; }
        public virtual DbSet<STH> STH { get; set; }
        public virtual DbSet<SolicitacaoAlteracaoCargo> SolicitacaoAlteracaoCargo { get; set; }
        public virtual DbSet<PerfilAcesso> PerfilAcesso { get; set; }
        public virtual DbSet<CalculoEventoBase> CalculoEventoBase { get; set; }
        public virtual DbSet<EventoFolha> EventoFolha { get; set; }
        public virtual DbSet<ValoresAbertosBase> ValoresAbertosBase { get; set; }
        public virtual DbSet<Reajuste> Reajuste { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }
        public virtual DbSet<AdNoturnoBase> AdNoturnoBase { get; set; }
        public virtual DbSet<AdNoturnoContratacao> AdNoturnoContratacao { get; set; }
        public virtual DbSet<ConvenioOdo> ConvenioOdo { get; set; }
        public virtual DbSet<EscalaTrabalho> EscalaTrabalho { get; set; }
        public virtual DbSet<QtdaDias> QtdaDias { get; set; }
        public virtual DbSet<ReajConvenioOdo> ReajConvenioOdo { get; set; }
        public virtual DbSet<ReajVTFretado> ReajVTFretado { get; set; }
        public virtual DbSet<ReajVTPasse> ReajVTPasse { get; set; }
        public virtual DbSet<HEBase> HEBase { get; set; }
        public virtual DbSet<HEContratacao> HEContratacao { get; set; }
        public virtual DbSet<ValoresAbertosCR> ValoresAbertosCR { get; set; }
        public virtual DbSet<FeriasPorCR> FeriasPorCR { get; set; }
        public virtual DbSet<ValoresAbertosContratacao> ValoresAbertosContratacao { get; set; }
        public virtual DbSet<CalculoEventoContratacao> CalculoEventoContratacao { get; set; }
        public virtual DbSet<Variaveis> Variaveis { get; set; }
        public virtual DbSet<Filial> Filial { get; set; }
        public virtual DbSet<Simulacao> Simulacao { get; set; }
        public virtual DbSet<SimulacaoContratacao> SimulacaoContratacao { get; set; }
        public virtual DbSet<SimulacaoDemissao> SimulacaoDemissao { get; set; }
        public virtual DbSet<TipoSimulacao> TipoSimulacao { get; set; }
        public virtual DbSet<ReajPAT> ReajPAT { get; set; }
        public virtual DbSet<MesOrcamento> MesOrcamento { get; set; }
        public virtual DbSet<FuncionarioFerias> FuncionarioFerias { get; set; }
        public virtual DbSet<Transferencia> Transferencia { get; set; }
    
        public virtual int CalculaCustoPessoa(string matricula, Nullable<int> codCiclo)
        {
            var matriculaParameter = matricula != null ?
                new ObjectParameter("matricula", matricula) :
                new ObjectParameter("matricula", typeof(string));
    
            var codCicloParameter = codCiclo.HasValue ?
                new ObjectParameter("codCiclo", codCiclo) :
                new ObjectParameter("codCiclo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CalculaCustoPessoa", matriculaParameter, codCicloParameter);
        }
    }
}

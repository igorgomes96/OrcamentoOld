angular.module('orcamentoApp').controller('solicitacoesTHHistoricoCtrl', ['$scope', 'solicitacoesTHAPI', 'sharedDataService', 'solicitacoesContratacoesAPI', 'solicitacoesDesligamentosAPI', 'solicitacoesAlteracoesCargosAPI', 'solicitacoesAlteracoesSalariosAPI', 'cargosAPI', 'funcionariosAPI', 'pageResolve', 'usuariosAPI', 'messagesService', function($scope, solicitacoesTHAPI, sharedDataService, solicitacoesContratacoesAPI, solicitacoesDesligamentosAPI, solicitacoesAlteracoesCargosAPI, solicitacoesAlteracoesSalariosAPI, cargosAPI, funcionariosAPI, pageResolve, usuariosAPI, messagesService) {
	
	var self = this;
	self.solicitacoes = [];
	self.solicitacao = null;
	self.qtdaAbertas = 0;
	self.filtroStatus = '';

	var user = sharedDataService.getUsuario();

	$scope.$on('novaSolicitacaoEvent', function(event) {
		loadSolicitacoesPorLogin(user.Login);
	});

	self.saveSolicitacao = function(solicitacao) {
		solicitacao.DataResposta = new Date();
		solicitacao.StatusSolicitacao = 'Respondida';
		solicitacoesTHAPI.putSolicitacaoTH(solicitacao.Codigo, solicitacao)
		.then(function(dado) {
			loadSolicitacoesPorSetor(user.SetorCod);
			messagesService.exibeMensagemSucesso("Solicitação respondida com sucesso!");
		});
	}

	var loadSolicitacoesPorLogin = function(login) {
		solicitacoesTHAPI.getSolicitacoesTH(login)
		.then(function(dado) {
			self.solicitacoes = dado.data;
			formataSolicitacoes(self.solicitacoes);
		});
	}

	var loadSolicitacoesPorSetor = function(codSetor) {
		solicitacoesTHAPI.getSolicitacoesTHPorSetor(codSetor)
		.then(function(dado) {
			self.solicitacoes = dado.data;
			formataSolicitacoes(self.solicitacoes);
		});
	}

	var formataSolicitacoes = function(solicitacoes) {
		self.qtdaAbertas = 0;
		solicitacoes.forEach(function(x) {

			usuariosAPI.getUsuario(x.LoginSolicitante)
			.then(function(retorno) {
				x.Solicitante = retorno.data;
			});

			x.MatriculaFuncionario = x.MatriculaFuncionario ? x.MatriculaFuncionario : 'N/A';
			x.Parecer = x.Parecer ? x.Parecer : 'N/A';
			x.DataResposta = x.DataResposta ? x.DataResposta : 'N/A';

			if (x.TipoSolicitacao == 'Contratação')
				x.Modal = "modal-contratacao";
			else if (x.TipoSolicitacao == 'Desligamento')
				x.Modal = "modal-demissao";
			else if (x.TipoSolicitacao == 'Alteração de Cargo')
				x.Modal = "modal-alteracao-cargo";
			else if (x.TipoSolicitacao == 'Alteração de Salário')
				x.Modal = "modal-alteracao-salario";
			else if (x.TipoSolicitacao == 'Alteração de Cargo e Salário')
				x.Modal = "modal-alteracao-cargo-salario";

			x.ACTsSTHs = (x.ACTs && x.ACTs.length) || (x.STHs && x.STHs.length) ? x.ACTs.map(function(x) { 
					return x.CodigoACT; 
				}).concat(x.STHs.map(function(x) { 
					return x.CodigoSTH; 
				})).reduce(function(x, y) {
					return x + ', ' + y;
				}) : [];

			x.ACTsSTHs = x.ACTsSTHs && x.ACTsSTHs.length ? x.ACTsSTHs : '-';

			if (x.StatusSolicitacao == 'Aguardando Aprovação') self.qtdaAbertas++;
		});
	}

	self.carregaSolicitacao = function(solicitacao) {
		self.solicitacao = solicitacao;

		if (solicitacao.TipoSolicitacao == 'Contratação'){
			carregaContratacao(self.solicitacao);
		}
		else if (solicitacao.TipoSolicitacao == 'Desligamento') {
			carregaDesligamento(self.solicitacao);
		}
		else if (solicitacao.TipoSolicitacao == 'Alteração de Cargo') {
			carregaAlteracaoCargo(self.solicitacao);
		}
		else if (solicitacao.TipoSolicitacao == 'Alteração de Salário') {
			carregaAlteracaoSalario(self.solicitacao);
		}
		else if (solicitacao.TipoSolicitacao == 'Alteração de Cargo e Salário') {
			carregaAlteracaoCargo(self.solicitacao);
			carregaAlteracaoSalario(self.solicitacao);
		}

	}

	var carregaContratacao = function(solicitacao) {
		solicitacoesContratacoesAPI.getSolicitacaoContratacao(solicitacao.Codigo)
		.then(function(dado) {
			solicitacao.Contratacao = dado.data;
			return cargosAPI.getCargo(dado.data.CargoCod);
		}).then(function(dado) {
			solicitacao.Contratacao.Cargo = dado.data;
		});
	}

	var carregaDesligamento = function(solicitacao) {
		solicitacoesDesligamentosAPI.getSolicitacaoDesligamento(solicitacao.Codigo)
		.then(function(dado) {
			solicitacao.Desligamento = dado.data;
			return funcionariosAPI.getFuncionario(dado.data.FuncionarioMatricula);
		}).then(function(dado) {
			solicitacao.Desligamento.Funcionario = dado.data;
			return cargosAPI.getCargo(dado.data.CargoCod);
		}).then(function(dado) {
			solicitacao.Desligamento.Funcionario.Cargo = dado.data;
		});
	}

	var carregaAlteracaoCargo = function(solicitacao) {
		solicitacoesAlteracoesCargosAPI.getSolicitacaoAlteracaoCargo(solicitacao.Codigo)
		.then(function(dado) {
			solicitacao.AlteracaoCargo = dado.data;
			return funcionariosAPI.getFuncionario(dado.data.FuncionarioMatricula);
		}).then(function(dado) {
			solicitacao.AlteracaoCargo.Funcionario = dado.data;
			return cargosAPI.getCargo(solicitacao.AlteracaoCargo.CargoCodAnterior);
		}).then(function(dado) {
			solicitacao.AlteracaoCargo.CargoAnterior = dado.data;
			return cargosAPI.getCargo(solicitacao.AlteracaoCargo.CargoCodNovo);
		}).then(function(dado) {
			solicitacao.AlteracaoCargo.CargoNovo = dado.data;
		});
	}

	var carregaAlteracaoSalario = function(solicitacao) {
		solicitacoesAlteracoesSalariosAPI.getSolicitacaoAlteracaoSalario(solicitacao.Codigo)
		.then(function(dado) {
			solicitacao.AlteracaoSalario = dado.data;
			return funcionariosAPI.getFuncionario(dado.data.FuncionarioMatricula);
		}).then(function(dado) {
			solicitacao.AlteracaoSalario.Percentual = ((solicitacao.AlteracaoSalario.SalarioNovo - solicitacao.AlteracaoSalario.SalarioAnterior) / solicitacao.AlteracaoSalario.SalarioAnterior) * 100;
			solicitacao.AlteracaoSalario.Funcionario = dado.data;
		});
	}

	if (pageResolve == 'Histórico')
		loadSolicitacoesPorLogin(user.Login);
	else if(pageResolve == 'Fila')
		loadSolicitacoesPorSetor(user.SetorCod);

}]);
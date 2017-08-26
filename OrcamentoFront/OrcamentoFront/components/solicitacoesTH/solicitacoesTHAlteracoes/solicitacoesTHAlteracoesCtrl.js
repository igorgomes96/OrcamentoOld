angular.module('orcamentoApp').controller('solicitacoesTHAlteracoesCtrl', ['$rootScope', 'funcionariosAPI', 'sharedDataService', 'cargosAPI', 'solicitacoesAlteracoesCargosAPI', 'empresasAPI', 'solicitacoesTHAPI', 'messagesService', 'solicitacoesAlteracoesSalariosAPI', 'numberFilter', function($rootScope, funcionariosAPI, sharedDataService, cargosAPI, solicitacoesAlteracoesCargosAPI, empresasAPI, solicitacoesTHAPI, messagesService, solicitacoesAlteracoesSalariosAPI, numberFilter) {

	var self = this;
	self.cargosCH = [];
	self.funcionario = null;
	self.descricao = null;
    self.percentual = 0;


	var user = sharedDataService.getUsuario();

	self.buscaFuncionario = function(matricula) {
		funcionariosAPI.getFuncionario(matricula)
		.then(function(dado) {
			self.funcionario = dado.data;
			self.novaSolicitacao.EmpresaCodAnterior = self.novaSolicitacao.EmpresaCodNovo = self.funcionario.EmpresaCod;
			self.novaSolicitacao.CargoCodAnterior = self.funcionario.CargoCod;
			self.novaSolicitacao.CHAnterior = self.funcionario.CargaHoraria;
			loadCargos(self.funcionario.EmpresaCod);
			return cargosAPI.getCargo(self.funcionario.CargoCod);
		}).then(function(dado) {
			self.funcionario.Cargo = dado.data;
			return empresasAPI.getEmpresa(self.funcionario.EmpresaCod);
		}).then(function(dado) {
			self.funcionario.EmpresaNome = dado.data.Nome;
		});
	}

	var loadCargos = function(empresaCod) {
        cargosAPI.getCHsPorCargo(empresaCod)
        .then(function(dado) {
            self.cargosCH = dado.data;
        });
    }

    self.cancelarEdicao = function() {

    	self.novaSolicitacao = null;
    	self.descricao = null;
    	self.funcionario = null;
        self.percentual = 0;
   	
   	}

   	self.saveAlteracaoCargo = function(solicitacao) {
    	
    	var solicitacaoTH = {
    		TipoSolicitacao: "Alteração de Cargo",
    		LoginSolicitante: user.Login,
    		DataSolicitacao: new Date(),
    		DescricaoSolicitacao: self.descricao,
            StatusSolicitacao: "Aguardando Aprovação"
    	}

		solicitacoesTHAPI.postSolicitacaoTH(solicitacaoTH)
    	.then(function(dado) {
    		solicitacao.SolicitacaoCod = dado.data.Codigo;
    		solicitacao.CargoCodNovo = solicitacao.Cargo.CargoCod;
    		return solicitacoesAlteracoesCargosAPI.postSolicitacaoAlteracaoCargo(solicitacao);
    	}).then(function(dado) {
    		self.cancelarEdicao();
    		messagesService.exibeMensagemSucesso("Solicitação enviada com sucesso!");
            $rootScope.$broadcast('novaSolicitacaoEvent');
    	});

    }

    self.saveAlteracaoSalario = function(solicitacao) {
        
        var solicitacaoTH = {
            TipoSolicitacao: "Alteração de Salário",
            LoginSolicitante: user.Login,
            DataSolicitacao: new Date(),
            DescricaoSolicitacao: self.descricao,
            StatusSolicitacao: "Aguardando Aprovação"
        }

        solicitacoesTHAPI.postSolicitacaoTH(solicitacaoTH)
        .then(function(dado) {
            solicitacao.SolicitacaoCod = dado.data.Codigo;
            solicitacao.SalarioAnterior = self.funcionario.Salario;
            return solicitacoesAlteracoesSalariosAPI.postSolicitacaoAlteracaoSalario(solicitacao);
        }).then(function(dado) {
            self.cancelarEdicao();
            messagesService.exibeMensagemSucesso("Solicitação enviada com sucesso!");
            $rootScope.$broadcast('novaSolicitacaoEvent');
        });

        

    }

    self.saveAlteracoes = function(solicitacao) {

        var solicitacaoTH = {
            TipoSolicitacao: "Alteração de Cargo e Salário",
            LoginSolicitante: user.Login,
            DataSolicitacao: new Date(),
            DescricaoSolicitacao: self.descricao,
            StatusSolicitacao: "Aguardando Aprovação"
        }

        solicitacoesTHAPI.postSolicitacaoTH(solicitacaoTH)
        .then(function(dado) {
            solicitacao.SolicitacaoCod = dado.data.Codigo;
            solicitacao.CargoCodNovo = solicitacao.Cargo.CargoCod;
            return solicitacoesAlteracoesCargosAPI.postSolicitacaoAlteracaoCargo(solicitacao);
        }).then(function(dado) {
            solicitacao.SalarioAnterior = self.funcionario.Salario;
            return solicitacoesAlteracoesSalariosAPI.postSolicitacaoAlteracaoSalario(solicitacao);
        }).then(function(dado) {
            self.cancelarEdicao();
            messagesService.exibeMensagemSucesso("Solicitação enviada com sucesso!");
            $rootScope.$broadcast('novaSolicitacaoEvent');
        });

    }

    self.calculaPercentual = function(antigo, novo) {
        self.percentual = numberFilter(((novo - antigo) / antigo) * 100, 2);
    }


}]);
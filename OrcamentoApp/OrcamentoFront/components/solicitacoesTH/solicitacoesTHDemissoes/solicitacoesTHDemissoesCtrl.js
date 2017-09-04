angular.module('orcamentoApp').controller('solicitacoesTHDemissoesCtrl', ['$rootScope', 'sharedDataService', 'solicitacoesTHAPI', 'solicitacoesDesligamentosAPI', 'messagesService', 'funcionariosAPI', function($rootScope, sharedDataService, solicitacoesTHAPI, solicitacoesDesligamentosAPI, messagesService, funcionariosAPI) {

	var self = this;
	self.novaSolicitacao = null;
	self.NomeFuncionario = null;

	var user = sharedDataService.getUsuario();

	self.buscaFuncionario = function(matricula) {
		funcionariosAPI.getFuncionario(matricula)
		.then(function(dado) {
			self.NomeFuncionario = dado.data.Nome;
		});
	}

	self.saveSolicitacao = function(solicitacao) {
    	
    	var solicitacaoTH = {
    		TipoSolicitacao: "Desligamento",
    		LoginSolicitante: user.Login,
    		DataSolicitacao: new Date(),
    		DescricaoSolicitacao: self.descricao,
            StatusSolicitacao: "Aguardando Aprovação"
    	}

		solicitacoesTHAPI.postSolicitacaoTH(solicitacaoTH)
    	.then(function(dado) {
    		solicitacao.SolicitacaoCod = dado.data.Codigo;
    		return solicitacoesDesligamentosAPI.postSolicitacaoDesligamento(solicitacao);
    	}).then(function(dado) {
    		self.cancelarEdicao();
    		messagesService.exibeMensagemSucesso("Solicitação enviada com sucesso!");
            $rootScope.$broadcast('novaSolicitacaoEvent');
    	});

    }

    self.cancelarEdicao = function() {

    	self.novaSolicitacao = null;
    	self.descricao = null;
    	self.NomeFuncionario = null;
   	
   	}

}]);
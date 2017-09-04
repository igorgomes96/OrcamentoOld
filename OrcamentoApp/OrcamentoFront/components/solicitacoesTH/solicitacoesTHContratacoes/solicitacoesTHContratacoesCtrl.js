angular.module('orcamentoApp').controller('solicitacoesTHContratacoesCtrl', ['$rootScope', 'solicitacoesTHAPI', 'solicitacoesContratacoesAPI', 'cargosAPI', 'filiaisAPI', 'sharedDataService', 'messagesService', 'centrosCustosAPI', 'empresasAPI', function($rootScope, solicitacoesTHAPI, solicitacoesContratacoesAPI, cargosAPI, filiaisAPI, sharedDataService, messagesService, centrosCustosAPI, empresasAPI) {
	
	var self = this;
	self.cidades = [];
	self.cargosCH =[];
	self.empresas = [];
	self.crs = [];
	self.novaSolicitacao = null;

	var user = sharedDataService.getUsuario();

	self.alteraEmpresa = function(empresaCod) {
		loadCRs(empresaCod);
		loadCidades(empresaCod);
		loadCargos(empresaCod);
	}

	var loadEmpresas = function() {
		empresasAPI.getEmpresas()
		.then(function(dado) {
			self.empresas = dado.data;
		});
	}

	var loadCRs = function(empresaCod) {
		self.crs = [];
		if (!empresaCod) return;
		centrosCustosAPI.getCentrosCustos(null, empresaCod)
		.then(function(dado) {
			self.crs = dado.data;
		});
	}

	var loadCidades = function(empresaCod) {
		filiaisAPI.getFiliais(empresaCod)
		.then(function(dado) {
			self.cidades = [];
			dado.data.forEach(function(x) {
				self.cidades.push(x.CidadeNome);
			});
		});
	}

	var loadCargos = function(empresaCod) {
        cargosAPI.getCHsPorCargo(empresaCod)
        .then(function(dado) {
            self.cargosCH = dado.data;
        });
    }

    self.saveSolicitacao = function(solicitacao) {
    	
    	var solicitacaoTH = {
    		TipoSolicitacao: "Contratação",
    		LoginSolicitante: user.Login,
    		DataSolicitacao: new Date(),
    		DescricaoSolicitacao: self.descricao,
            StatusSolicitacao: "Aguardando Aprovação"
    	}

		solicitacoesTHAPI.postSolicitacaoTH(solicitacaoTH)
    	.then(function(dado) {
    		solicitacao.SolicitacaoCod = dado.data.Codigo;
    		solicitacao.CargoCod = solicitacao.Cargo.CargoCod;
    		return solicitacoesContratacoesAPI.postSolicitacaoContratacao(solicitacao);
    	}).then(function(dado) {
    		self.novaSolicitacao = null;
    		self.descricao = null;
    		messagesService.exibeMensagemSucesso("Solicitação enviada com sucesso!");
    		$rootScope.$broadcast('novaSolicitacaoEvent');
    	});

    	
    }

    loadEmpresas();


}]);
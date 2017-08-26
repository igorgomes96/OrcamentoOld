angular.module('orcamentoApp').controller('selecaoCicloCtrl', ['ciclosAPI', 'sharedDataService', '$state', function(ciclosAPI, sharedDataService, $state) {

	var self = this;
	self.ciclos = [];
	self.ciclosAbertos = null;
	self.ciclosFechados = [];
	self.qtdaFechadosVisivel = 0;

	var load = function() {
		ciclosAPI.getCiclos(1)   //Status 2 = Fechado
		.then(function(dado) {
			self.ciclosAbertos = dado.data;
			return ciclosAPI.getCiclos(2);
		}).then(function(dado) {
			self.ciclosFechados = dado.data;
			self.qtdaFechadosVisivel = self.ciclosAbertos && self.ciclosAbertos.length < 4 ?  4 - self.ciclosAbertos.length : 0;
		});
	}

	self.irParaOrcamento = function(ciclo) {
		sharedDataService.setCicloAtual(ciclo);
		$state.go('containerHome.selecaoModulo');
	}

	load();
}]);
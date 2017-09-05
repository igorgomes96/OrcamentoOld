angular.module('orcamentoApp').controller('selecaoCicloCtrl', ['ciclosAPI', 'sharedDataService', '$state', 'tiposCicloAPI', 'messagesService', function(ciclosAPI, sharedDataService, $state, tiposCicloAPI, messagesService) {

	var self = this;
	self.ciclos = [];
	self.ciclosAbertos = null;
	self.ciclosFechados = [];
	self.qtdaFechadosVisivel = 0;
	self.anos = [];
	self.ano = null;
	self.tipos = [];
	self.novoCiclo = {StatusCod: 1, DataInicio: new Date()};

	var loadTiposCiclo = function() {
		tiposCicloAPI.getTiposCiclo()
		.then(function(dado) {
			self.tipos = dado.data;
		});
	}

	var load = function() {
		self.anos = [];
		ciclosAPI.getCiclos(1)   
		.then(function(dado) {
			self.ciclosAbertos = dado.data;
			return ciclosAPI.getCiclos(2); //Status 2 = Fechado
		}).then(function(dado) {
			self.ciclosFechados = dado.data;
			self.qtdaFechadosVisivel = self.ciclosAbertos && self.ciclosAbertos.length < 4 ?  4 - self.ciclosAbertos.length : 0;
			self.ciclosFechados.forEach(function(x) {
				x.Ano = new Date(x.DataInicio).getYear() + 1900;
				if (self.anos.indexOf(x.Ano) == -1) {
					self.anos.push(x.Ano);
				}
			});
		});
	}

	self.salvarCiclo = function(ciclo) {
		ciclosAPI.postCiclo(ciclo)
		.then(function(dado) {
			load();
			self.novoCiclo = {StatusCod: 1};
			messagesService.exibeMensagemSucesso('Ciclo criado com sucesso!');
		});
	}

	self.irParaOrcamento = function(ciclo) {
		sharedDataService.setCicloAtual(ciclo);
		$state.go('containerHome.selecaoModulo');
	}

	load();
	loadTiposCiclo();
}]);
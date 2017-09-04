angular.module('orcamentoApp').controller('encargosPorFilialCtrl', ['messagesService', '$scope', 'filiaisAPI', 'empresasAPI', 'sindicatosAPI', 'numberFilter', function(messagesService, $scope, filiaisAPI, empresasAPI, sindicatosAPI, numberFilter) {

	var self = this;

	self.encargos = [];
	
	var loadEncargos = function() {	
		filiaisAPI.getFiliais()
		.then(function(dado) {
			self.encargos = dado.data;

			self.encargos.forEach(function(x) {

				empresasAPI.getEmpresa(x.EmpresaCod)
				.then(function(retorno) {
					x.NomeEmpresa = retorno.data.Nome;
				});

				sindicatosAPI.getSindicato(x.SindicatoCod)
				.then(function(retorno) {
					x.NomeSindicato = retorno.data.NomeSindicato;
				});

				x.FAP = numberFilter(x.FAP * 100, 3);
				x.SAT = numberFilter(x.SAT * 100, 3);

			});

		});
	}

	self.saveEncargos = function(encargos) {
		var total = encargos.length, cont = 0;
		angular.copy(encargos).forEach(function(x) {
			
			x.FAP = x.FAP / 100;
			x.SAT = x.SAT / 100;

			filiaisAPI.putFilial(x.EmpresaCod, x.CidadeNome, x)
			.then(function() {
				cont++;
				if (cont >= total) 
					messagesService.exibeMensagemSucesso("Encargos Salvos com sucesso!");
			});
		});

	}

	$scope.$on('newBaseEvent', function(event) {
		loadEncargos();
	});

	loadEncargos();
}]);
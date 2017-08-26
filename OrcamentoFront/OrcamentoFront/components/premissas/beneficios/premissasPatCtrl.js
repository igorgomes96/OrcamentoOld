angular.module('orcamentoApp').controller('premissasPatCtrl', ['patAPI', 'sindicatosAPI', 'numberFilter', function(patAPI, sindicatosAPI, numberFilter) {

	var self = this;
	self.pats = [];

	var loadPats = function() {
		patAPI.getPATs()
		.then(function(dado) {
			dado.data.forEach(function(x) {

				x.Valor = numberFilter(x.Valor, 2);
				sindicatosAPI.getSindicato(x.SindicatoCod)
				.then(function(retorno) {
					x.Sindicato = retorno.data;
				});

			});

			self.pats = dado.data;
		});
	}

	loadPats();
}]);
angular.module('orcamentoApp').controller('premissasVTCtrl', ['cidadesAPI', 'numberFilter', function(cidadesAPI, numberFilter) {

	var self = this;

	self.cidades = [];

	var loadVT = function() {
		cidadesAPI.getCidades()
		.then(function(dado) {

			dado.data.forEach(function(x) {
				x.VT = numberFilter(x.VT, 2);
			});

			self.cidades = dado.data;
		});
	}

	loadVT();


}]);
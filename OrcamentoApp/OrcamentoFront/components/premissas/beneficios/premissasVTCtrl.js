angular.module('orcamentoApp').controller('premissasVTCtrl', ['cidadesAPI', 'numberFilter', 'messagesService', function(cidadesAPI, numberFilter, messagesService) {

	var self = this;

	self.cidades = [];

	var loadVT = function() {
		cidadesAPI.getCidades()
		.then(function(dado) {

			dado.data.forEach(function(x) {
				x.VTPasse = numberFilter(x.VTPasse, 2);
				x.VTFretadoValor = numberFilter(x.VTFretadoValor, 2);
			});

			self.cidades = dado.data;
		});
	}

	self.saveAll = function(cidades) {
		cidadesAPI.postCidadeSaveAll(cidades)
		.then(function() {
			messagesService.exibeMensagemSucesso("Informações salvas com sucesso!");
		});
	}

	loadVT();


}]);
angular.module('orcamentoApp').controller('premissasPatCtrl', ['patAPI', 'sindicatosAPI', 'numberFilter', 'messagesService', function(patAPI, sindicatosAPI, numberFilter, messagesService) {

	var self = this;
	self.pats = [];

	var loadPats = function() {
		patAPI.getPATs()
		.then(function(dado) {
			dado.data.forEach(function(x) {

				x.Valor = numberFilter(x.Valor, 2);
				/*sindicatosAPI.getSindicato(x.SindicatoCod)
				.then(function(retorno) {
					x.Sindicato = retorno.data;
				});*/

			});

			self.pats = dado.data;
		});
	}

	self.saveAll = function(pats) {
		patAPI.postPATSaveAll(pats)
		.then(function() {
			messagesService.exibeMensagemSucesso("Informações salvas com sucesso!");
		});
	}

	loadPats();
}]);
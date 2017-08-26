angular.module('orcamentoApp').controller('premissasConvenioMedicoCtrl', ['conveniosMedAPI', 'numberFilter', 'messagesService', function(conveniosMedAPI, numberFilter, messagesService) {

	var self = this;
	self.convenios = [];

	var loadConvenios = function() {
		conveniosMedAPI.getConveniosMeds()
		.then(function(dado) {
			dado.data.forEach(function(x) {
				x.Valor = numberFilter(x.Valor, 2);
				x.ValorDependentes = numberFilter(x.ValorDependentes, 2);
			});
			self.convenios = dado.data;
		});
	}

	self.salvarConvenios = function(convenios) {
		var f = null;
		convenios.forEach(function(x) {
			if (x.Codigo) f = conveniosMedAPI.putConveniosMed(x.Codigo, x);
			else f = conveniosMedAPI.postConveniosMed(x);
		});
		messagesService.exibeMensagemSucesso('Planos salvos com sucesso!');
	}

	loadConvenios();

}]);
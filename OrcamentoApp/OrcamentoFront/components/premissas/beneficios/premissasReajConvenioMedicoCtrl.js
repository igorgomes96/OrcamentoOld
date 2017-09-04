angular.module('orcamentoApp').controller('premissasReajConvenioMedicoCtrl', ['reajConvenioMedicoAPI', 'conveniosMedAPI', 'numberFilter', 'messagesService', function(reajConvenioMedicosAPI, conveniosMedAPI, numberFilter, messagesService) {

	var self = this;

	self.reajustes = [];
	self.anos = [];
	self.meses = [
		{Valor: 1, Mes: 'Janeiro'},
		{Valor: 2, Mes: 'Fevereiro'},
		{Valor: 3, Mes: 'Março'},
		{Valor: 4, Mes: 'Abril'},
		{Valor: 5, Mes: 'Maio'},
		{Valor: 6, Mes: 'Junho'},
		{Valor: 7, Mes: 'Julho'},
		{Valor: 8, Mes: 'Agosto'},
		{Valor: 9, Mes: 'Setembro'},
		{Valor: 10, Mes: 'Outubro'},
		{Valor: 11, Mes: 'Novembro'},
		{Valor: 12, Mes: 'Dezembro'}
	];
	self.ano = new Date().getYear() + 1900;

	self.loadReajustes = function(ano) {
		reajConvenioMedicosAPI.getReajConvenioMedicos(ano)
		.then(function(dado) {
			dado.data.forEach(function(x) {

				x.PercentualReajuste = numberFilter(x.PercentualReajuste * 100, 2);

			});
			self.reajustes = dado.data;
		});
	}

	self.saveAll = function(reajustes) {
		var reaj = angular.copy(reajustes);
		reaj.forEach(function(x) {
			x.PercentualReajuste = x.PercentualReajuste / 100;
		});

		reajConvenioMedicosAPI.postReajConvenioMedicoSaveAll(reaj)
		.then(function() {
			messagesService.exibeMensagemSucesso("Informações salvas com sucesso!");
		});

	}

	self.excluir = function(ano, convenio, mes) {
		reajConvenioMedicosAPI.deleteReajConvenioMedico(ano, convenio, mes)
		.then(function() {
			messagesService.exibeMensagemSucesso("Reajuste excluído com sucesso!");
			self.loadReajustes(self.ano);
		});	
	}

	for(var i = -1; i < 5; i++) {
		self.anos.push(self.ano + i);
	}

	self.loadReajustes(self.ano);

}]);
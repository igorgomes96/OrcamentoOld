angular.module('orcamentoApp').controller('premissasSindicatosCtrl', ['reajustesAPI', 'messagesService', 'sindicatosAPI', 'numberFilter', function(reajustesAPI, messagesService, sindicatosAPI, numberFilter) {

	var self = this;

	self.sindicatos = [];
	self.filtroAno = new Date().getYear() + 1900;
	self.anos = [];

	self.loadSindicatos = function(ano) {
		reajustesAPI.getReajustes(null, ano)
		.then(function(dado){

			dado.data.forEach(function(x) {
				x.PisoSalarial = numberFilter(x.PisoSalarial, 2);
				x.PercentualReajuste = numberFilter(x.PercentualReajuste * 100, 4);
				x.MesFechamento = new Date(x.Ano, x.MesFechamento - 1, 1);
				sindicatosAPI.getSindicato(x.SindicatoCod)
				.then(function(retorno) {
					x.SindicatoNome = retorno.data.NomeSindicato;
				});
			});

			self.sindicatos = dado.data;
		})
	}

	self.salvarSindicatos = function(sindicatos) {
		var f = null;
		sindicatos = angular.copy(sindicatos);
		sindicatos.forEach(function(x) {
			x.PercentualReajuste = x.PercentualReajuste / 100;
			x.MesFechamento = x.MesFechamento.getMonth() + 1;
			reajustesAPI.putReajuste(x.SindicatoCod, x.Ano, x);
		});
		messagesService.exibeMensagemSucesso('Sindicatos salvos com sucesso!');

	}

	function carregaAnos() {
		var x = self.filtroAno - 1;

		for (var i = 0; i < 5; i++) {
			self.anos.push(x + i);
		}
	};
	
	carregaAnos();

	self.loadSindicatos(self.filtroAno);

}]);
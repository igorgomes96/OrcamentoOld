angular.module('orcamentoApp').controller('encargosPorEmpresaCtrl', ['empresasAPI', 'numberFilter', 'encargosAPI', 'messagesService', '$scope', function(empresasAPI, numberFilter, encargosAPI, messagesService, $scope) {

	var self = this;

	self.encargos = [];

	var loadEncargos = function() {
		encargosAPI.getEncargos()
		.then(function(dado) {
			self.encargos = dado.data;

			self.encargos.forEach(function(x) {

				empresasAPI.getEmpresa(x.EmpresaCod)
				.then(function(retorno) {
					x.NomeEmpresa = retorno.data.Nome;
				}); 

				x.Enc13 = numberFilter(x.Enc13 * 100, 3);
				x.AvisoPrevio = numberFilter(x.AvisoPrevio * 100, 3);
				x.Ferias = numberFilter(x.Ferias * 100, 3);
				x.FGTS = numberFilter(x.FGTS * 100, 3);
				x.INSS = numberFilter(x.INSS * 100, 3);
				x.SistemaS = numberFilter(x.SistemaS * 100, 3);

			});

		});
	}

	self.saveEncargos = function(encargos) {
		var total = encargos.length, cont = 0;
		angular.copy(encargos).forEach(function(x) {
			x.Enc13 = x.Enc13 / 100;
			x.AvisoPrevio = x.AvisoPrevio / 100;
			x.Ferias = x.Ferias / 100;
			x.FGTS = x.FGTS / 100;
			x.INSS = x.INSS / 100;
			x.SistemaS = x.SistemaS / 100;
			encargosAPI.putEncargo(x.EmpresaCod, x)
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
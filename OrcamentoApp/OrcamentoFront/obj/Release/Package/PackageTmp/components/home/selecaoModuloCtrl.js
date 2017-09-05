angular.module('orcamentoApp').controller('selecaoModuloCtrl', ['sharedDataService', 'ciclosAPI', 'messagesService', '$state', function(sharedDataService, ciclosAPI, messagesService, $state) {

	var self = this;

	self.ciclo = sharedDataService.getCicloAtual();

	self.fecharCiclo = function(ciclo) {
		ciclo.DataFim = new Date();
		ciclo.StatusCod = 2;
		ciclosAPI.putCiclo(ciclo.Codigo, ciclo)
		.then(function(dado) {
			messagesService.exibeMensagemSucesso('Ciclo excluído com sucesso!');
			$state.go('containerHome.selecaoCiclo');
		});	
	}
}]);
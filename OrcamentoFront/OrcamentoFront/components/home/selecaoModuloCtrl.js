angular.module('orcamentoApp').controller('selecaoModuloCtrl', ['sharedDataService', function(sharedDataService) {

	var self = this;

	self.ciclo = sharedDataService.getCicloAtual();
}]);
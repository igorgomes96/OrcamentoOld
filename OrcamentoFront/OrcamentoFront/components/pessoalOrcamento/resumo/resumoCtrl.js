angular.module('orcamentoApp').controller('resumoCtrl', ['calculosEventosBaseAPI', 'sharedDataService', '$scope', 'numberFilter', function(calculosEventosBaseAPI, sharedDataService, $scope, numberFilter) {

	var self = this;

	self.cr = null;
	self.ciclo = null;
	self.eventosBaseFuncionario = [];

	var loadEventosBasePorFuncionario = function(cr, ciclo) {
		calculosEventosBaseAPI.getValoresPorCicloPorCR(cr, ciclo)
		.then(function(dado) {
			dado.data.forEach(function(x) {
				x.Eventos.forEach(function(y) {
					for (var mes in y.ValoresMensais) {
						y.ValoresMensais[mes].Valor = numberFilter(y.ValoresMensais[mes].Valor, 2);
					}
				});
			})
			self.eventosBaseFuncionario = dado.data;
		});
	}


	//Adiciona um listener para capturar as mudanças de seleção de CR
    var listenerCR = $scope.$on('crChanged', function($event, cr) {
        if (cr) {
            self.cr = cr;
            loadEventosBasePorFuncionario(cr.Codigo, self.ciclo.Codigo);
        }
        else{
            self.eventosBaseFuncionario = [];
        }
    });


    //Remove o Listener
    $scope.$on('$destroy', function () {
        listenerCR();
    });


	//loadFuncionarios();
    self.ciclo = sharedDataService.getCicloAtual();

}]);
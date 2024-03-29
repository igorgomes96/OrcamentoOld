angular.module('orcamentoApp').controller('resumoFolhaContratacoesCtrl', ['sharedDataService', '$scope', 'numberFilter', 'calculosEventosContratacoesAPI', function(sharedDataService, $scope, numberFilter, calculosEventosContratacoesAPI) {

	var self = this;

	self.ciclo = null;
	self.cr = null;
	self.ciclo = null;
	self.eventosContratacoes = [];


	var loadEventosContratacoes = function(cr, ciclo) {
		calculosEventosContratacoesAPI.getValoresPorCicloPorCR(cr, ciclo)
		.then(function(dado) {
			dado.data.forEach(function(x) {
				x.Eventos.forEach(function(y) {
					for (var mes in y.ValoresMensais) {
						y.ValoresMensais[mes].Valor = numberFilter(y.ValoresMensais[mes].Valor, 2);
					}
				});
			});
			self.eventosContratacoes = dado.data;
		});
	}

	//Adiciona um listener para capturar as mudanças de seleção de CR
    var listenerCR = $scope.$on('crChanged', function($event, cr) {
        if (cr && self.ciclo) {
            self.cr = cr;
            loadEventosContratacoes(self.cr.Codigo, self.ciclo.Codigo);
        }
    });

	var listenerCalculoRealizado = $scope.$on('calculoRealizado', function($event) {
		if (self.cr && self.ciclo){
			loadEventosContratacoes(self.cr.Codigo, self.ciclo.Codigo);
		}

	});


    //Remove o Listener
    $scope.$on('$destroy', function () {
        listenerCR();
        listenerCalculoRealizado();
    });

    self.ciclo = sharedDataService.getCicloAtual();
	self.cr = sharedDataService.getUltimoCR();

    if (self.ciclo && self.cr) {
    	loadEventosContratacoes(self.cr.Codigo, self.ciclo.Codigo);
    }

}]);
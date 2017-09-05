angular.module('orcamentoApp').controller('resumoFolhaVerbasGeraisCtrl', ['valoresAbertosCRsAPI', 'sharedDataService', '$scope', 'numberFilter', function(valoresAbertosCRsAPI, sharedDataService, $scope, numberFilter) {

	var self = this;
	self.valoresAbertos = [];

	var insumosAbertos = sharedDataService.getInsumosAbertosFolha();
	
	var loadValoresAbertosCR = function(cr, ciclo) {
		self.valoresAbertos = [];
        insumosAbertos.forEach(function(z) {
            valoresAbertosCRsAPI.getValoresAbertosCRsPorCiclo(cr, z, ciclo)
            .then(function(dado) {
				self.valoresAbertos.push(dado.data);
            });
        });
    }

	//Adiciona um listener para capturar as mudanças de seleção de CR
    var listenerCR = $scope.$on('crChanged', function($event, cr) {
        if (cr && self.ciclo) {
            self.cr = cr;
            loadValoresAbertosCR(self.cr.Codigo, self.ciclo.Codigo);
        }
        else
            self.valoresAbertos = [];
    });

	var listenerCalculoRealizado = $scope.$on('calculoRealizado', function($event) {
		if (self.cr && self.ciclo){
			loadValoresAbertosCR(self.cr.Codigo, self.ciclo.Codigo);
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
    	loadValoresAbertosCR(self.cr.Codigo, self.ciclo.Codigo);
    }

}]);
angular.module('orcamentoApp').controller('resumoFolhaCtrl', ['calculosEventosBaseAPI', 'sharedDataService', '$scope', 'numberFilter', '$scope', 'valoresAbertosCRsAPI', function(calculosEventosBaseAPI, sharedDataService, $scope, numberFilter, $scope, valoresAbertosCRsAPI) {

	var self = this;

	self.ciclo = null;
	self.cr = null;
	self.ciclo = null;
	self.eventosBaseFuncionario = [];
	self.valoresAbertos = [];

	var insumosAbertos = sharedDataService.getInsumosAbertosFolha();

	var loadEventosBasePorFuncionario = function(cr, ciclo) {
		calculosEventosBaseAPI.getValoresPorCicloPorCR(cr, ciclo)
		.then(function(dado) {
			dado.data.forEach(function(x) {
				x.Eventos.forEach(function(y) {
					for (var mes in y.ValoresMensais) {
						y.ValoresMensais[mes].Valor = numberFilter(y.ValoresMensais[mes].Valor, 2);
					}
				});
			});
			self.eventosBaseFuncionario = dado.data;
		});
	}

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
            loadEventosBasePorFuncionario(self.cr.Codigo, self.ciclo.Codigo);
            loadValoresAbertosCR(self.cr.Codigo, self.ciclo.Codigo);
        }
        else
            self.funcionarios = [];
    });

	var listenerCalculoRealizado = $scope.$on('calculoRealizado', function($event) {
		if (self.cr && self.ciclo){
			loadEventosBasePorFuncionario(self.cr.Codigo, self.ciclo.Codigo);
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
    	loadEventosBasePorFuncionario(self.cr.Codigo, self.ciclo.Codigo);
    	loadValoresAbertosCR(self.cr.Codigo, self.ciclo.Codigo);
    }


}]);
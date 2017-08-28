angular.module('orcamentoApp').controller('valoresPorCRCtrl', ['valoresAbertosCRsAPI', '$scope', 'sharedDataService', 'feriasPorCRsAPI', 'eventosFolhaAPI', 'numberFilter', function(valoresAbertosCRsAPI, $scope, sharedDataService, feriasPorCRsAPI, eventosFolhaAPI, numberFilter) {

	var self = this;

	self.valores = [];
	self.ferias = [];
    self.aba = 'Férias';

    var insumosAbertos = sharedDataService.getInsumosAbertosFolha();


    var loadFerias = function(cr, ciclo) {
        var temp = [];
        self.ciclo.Meses.forEach(function(y) {
            temp.push({
                CodigoCR: cr,
                CodMesOrcamento: y.Codigo,
                Percentual: 0
            });
        });

        feriasPorCRsAPI.getFeriasPorCRs(cr, ciclo)
        .then(function(dado) {

            dado.data.forEach(function(x) {
                var f = temp.filter(function(y) {
                    return y.CodMesOrcamento == x.CodMesOrcamento;
                });
                if (f && f.length > 0) {
                    f[0].Percentual = numberFilter(x.Percentual * 100, 2);
                }
            });

        });

        self.ferias = temp;
    }

    var loadValores = function(cr, ciclo) {

        self.valores = [];
                
        insumosAbertos.forEach(function(z) {


            eventosFolhaAPI.getEventoFolha(z)
            .then(function(dado) {
                var atual = dado.data;
                atual.Valores = [];
                self.valores.push(atual);
                self.ciclo.Meses.forEach(function(y) {
                    atual.Valores.push({
                        CodigoCR: cr,
                        CodEvento: z,
                        CodMesOrcamento: y.Codigo,
                        Valor: 0
                    });
                });

                valoresAbertosCRsAPI.getValoresAbertosCRs(cr, z, ciclo)
                .then(function(dado) {

                    dado.data.forEach(function(x) {
                        var v = atual.Valores.filter(function(y) {
                            return y.CodMesOrcamento == x.CodMesOrcamento;
                        });

                        if (v && v.length > 0) {
                            v[0].Valor = x.Valor;
                        }
                    });
                    

                });

            });


        });



    }


    //Adiciona um listener para capturar as mudanças de seleção de CR
    var listenerCR = $scope.$on('crChanged', function($event, cr) {
        if (cr && self.ciclo) {
            self.cr = cr;
            loadValores(self.cr.Codigo, self.ciclo.Codigo);
            loadFerias(self.cr.Codigo, self.ciclo.Codigo);
        }
        else {
            self.valores = [];
            self.ferias = [];
        }

    });

    self.ciclo = sharedDataService.getCicloAtual();

    //Remove o Listener
    $scope.$on('$destroy', function () {
        listenerCR();
    });

}]);
angular.module('orcamentoApp').controller('valoresPorCRCtrl', ['valoresAbertosCRsAPI', '$scope', 'sharedDataService', 'feriasPorCRsAPI', 'eventosFolhaAPI', 'numberFilter', 'messagesService', function(valoresAbertosCRsAPI, $scope, sharedDataService, feriasPorCRsAPI, eventosFolhaAPI, numberFilter, messagesService) {

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
            valoresAbertosCRsAPI.getValoresAbertosCRsPorCiclo(cr, z, ciclo)
            .then(function(dado) {
                self.valores.push(dado.data);
            });
        });
    }

    self.saveAll = function() {
        var valores = [];
        self.valores.forEach(function(x) {
            valores = valores.concat(x.Valores.map(function(y) { return y; }));
        });

        var ferias = angular.copy(self.ferias);
        ferias.forEach(function(x) {
            x.Percentual = x.Percentual / 100;
        })

        valoresAbertosCRsAPI.postValorAbertoCRSaveAll(valores)
        .then(function() {
            return feriasPorCRsAPI.postFeriasPorCRSaveAll(self.ciclo.Codigo, ferias);
        }).then(function() {
            messagesService.exibeMensagemSucesso("Valores salvos com Sucesso!");
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
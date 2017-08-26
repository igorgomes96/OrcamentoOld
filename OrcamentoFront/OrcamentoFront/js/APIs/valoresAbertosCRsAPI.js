angular.module('orcamentoApp').service('valoresAbertosCRsAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'ValoresAbertosCRs';

    self.getValoresAbertosCRs = function(cr, codEvento, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{cr:cr, codEvento:codEvento, codCiclo:codCiclo}});
    }

    /*self.getValorAbertoCR = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }*/

    self.postValorAbertoCR = function(valorAbertoCR) {
        return $http.post(config.baseUrl + resource, valorAbertoCR);
    }

    self.putValorAbertoCR = function(codEvento, codMes, cr, valorAbertoCR) {
        return $http.put(config.baseUrl + resource + '/' + codEvento + '/' + codMes + '/' + cr, valorAbertoCR);
    }

    self.deleteValorAbertoCR = function(codEvento, codMes, cr) {
        return $http.delete(config.baseUrl + resource + '/' + codEvento + '/' + codMes + '/' + cr);
    }
}]);
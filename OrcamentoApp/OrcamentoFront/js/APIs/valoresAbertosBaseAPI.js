angular.module('orcamentoApp').service('valoresAbertosBaseAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'ValoresAbertosBase';

    self.getValoresAbertosBase = function(codEvento, matricula, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{codEvento:codEvento, matricula:matricula, codCiclo:codCiclo}});
    }

    self.postValorAbertoBaseSaveAll = function(valoresAbertos) {
        return $http.post(config.baseUrl + resource + '/SaveAll', valoresAbertos);
    }

    self.postValorAbertoBase = function(valorAbertoBase) {
        return $http.post(config.baseUrl + resource, valorAbertoBase);
    }

    self.putValorAbertoBase = function(codEvento, codMes, matricula, valorAbertoBase) {
        return $http.put(config.baseUrl + resource + '/' + codEvento + '/' + codMes + '/' + matricula, valorAbertoBase);
    }

    self.deleteValorAbertoBase = function(codEvento, codMes, matricula) {
        return $http.delete(config.baseUrl + resource + '/' + codEvento + '/' + codMes + '/' + matricula);
    }
}]);
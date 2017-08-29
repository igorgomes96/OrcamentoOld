angular.module('orcamentoApp').service('calculosEventosBaseAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'CalculosEventosBase';

    self.getCalculosEventosBase = function(codCiclo, matricula, codEvento) {
        return $http.get(config.baseUrl + resource, {params:{codCiclo:codCiclo, matricula:matricula, codEvento:codEvento}});
    }

    self.getValoresPorCiclo = function(matricula, codCiclo) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/' + matricula + '/' + codCiclo);
    }

    self.getValoresPorCicloPorCR = function(cr, codCiclo) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/PorCR/' + cr + '/' + codCiclo);
    }

    self.postCalculoEventoBase = function(calculoEventoBase) {
        return $http.post(config.baseUrl + resource, calculoEventoBase);
    }

    self.putCalculoEventoBase = function(evento, matricula, mes, calculoEventoBase) {
        return $http.put(config.baseUrl + resource + '/' + evento + '/' + matricula + '/' + mes, calculoEventoBase);
    }

    self.deleteCalculoEventoBase = function(evento, matricula, mes) {
        return $http.delete(config.baseUrl + resource + '/' + evento + '/' + matricula + '/' + mes);
    }
}]);
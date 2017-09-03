angular.module('orcamentoApp').service('calculosEventosContratacoesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'CalculosEventosContratacoes';

    self.getCalculosEventosContratacoes = function(codCiclo, contratacao, codEvento) {
        return $http.get(config.baseUrl + resource, {params:{codCiclo:codCiclo, contratacao:contratacao, codEvento:codEvento}});
    }

    self.getValoresPorCiclo = function(contratacao, codCiclo) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/' + contratacao + '/' + codCiclo);
    }

    self.getValoresPorCicloPorCR = function(cr, codCiclo) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/PorCR/' + cr + '/' + codCiclo);
    }

    self.postCalculaContratacaoPorCicloPorCR = function(cr, codCiclo) {
        return $http.post(config.baseUrl + resource + '/Calcula/PorCiclo/PorCR/' + cr + '/' + codCiclo);
    }

    self.postCalculoEventoContratacao = function(calculoEventoContratacao) {
        return $http.post(config.baseUrl + resource, calculoEventoContratacao);
    }

    self.putCalculoEventoContratacao = function(contratacao, evento, mes, calculoEventoContratacao) {
        return $http.put(config.baseUrl + resource + '/' + contratacao + '/' + evento + '/' + mes, calculoEventoContratacao);
    }

    self.deleteCalculoEventoContratacao = function(contratacao, evento, mes) {
        return $http.delete(config.baseUrl + resource + '/' + contratacao + '/' + evento + '/' + mes);
    }
}]);
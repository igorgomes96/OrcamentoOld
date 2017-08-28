angular.module('orcamentoApp').service('valoresAbertosContratacoesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'ValoresAbertosContratacoes';

    self.getValoresAbertosContratacoes = function(codEvento, codContratacao, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{codEvento:codEvento, codContratacao:codContratacao, codCiclo:codCiclo}});
    }

    self.postValorAbertoContratacao = function(valorAbertoContratacao) {
        return $http.post(config.baseUrl + resource, valorAbertoContratacao);
    }

    self.putValorAbertoContratacao = function(codEvento, codMes, codContratacao, valorAbertoContratacao) {
        return $http.put(config.baseUrl + resource + '/' + codEvento + '/' + codMes + '/' + codContratacao, valorAbertoContratacao);
    }

    self.deleteValorAbertoContratacao = function(codEvento, codMes, codContratacao) {
        return $http.delete(config.baseUrl + resource + '/' + codEvento + '/' + codMes + '/' + codContratacao);
    }
}]);
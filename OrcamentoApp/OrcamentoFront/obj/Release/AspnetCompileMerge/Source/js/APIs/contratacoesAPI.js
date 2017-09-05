angular.module('orcamentoApp').service('contratacoesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Contratacoes';

    self.getContratacoes = function(codCiclo, codCentroCusto) {
        if (codCiclo && codCentroCusto)
            return $http({method: 'GET', url:config.baseUrl + resource, params:{codCiclo: codCiclo, codCentroCusto: codCentroCusto}});
        else if (codCiclo)
            return $http.get(config.baseUrl + resource + '?codCiclo=' + codCiclo);
        else if (codCentroCusto)
            return $http.get(config.baseUrl + resource + '?codCentroCusto=' & codCentroCusto);
        else
            return $http.get(config.baseUrl + resource);
    }

    self.getContratacao = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postContratacao = function(contratacao) {
        return $http.post(config.baseUrl + resource, contratacao);
    }

    self.putContratacao = function(id, contratacao) {
        return $http.put(config.baseUrl + resource + '/' + id, contratacao);
    }

    self.deleteContratacao = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
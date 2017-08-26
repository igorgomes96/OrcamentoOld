angular.module('orcamentoApp').service('contratacoesMesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'ContratacoesMes';

    self.getContratacoesMes = function(codContratacao) {
        if (codContratacao)
            return $http.get(config.baseUrl + resource + '?codContratacao=' + codContratacao);
        else
            return $http.get(config.baseUrl + resource);
    }

    self.getContratacaoMes = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postContratacaoMes = function(contratacaoMes) {
        return $http.post(config.baseUrl + resource, contratacaoMes);
    }

    self.putContratacaoMes = function(id, contratacaoMes) {
        return $http.put(config.baseUrl + resource + '/' + id, contratacaoMes);
    }

    self.deleteContratacaoMes = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
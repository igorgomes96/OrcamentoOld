angular.module('orcamentoApp').service('sindicatosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Sindicatos';

    self.getSindicatos = function(codEmpresa) {
        return $http.get(config.baseUrl + resource);
    }

    self.getSindicato = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postSindicato = function(sindicato) {
        return $http.post(config.baseUrl + resource, sindicato);
    }

    self.putSindicato = function(id, sindicato) {
        return $http.put(config.baseUrl + resource + '/' + id, sindicato);
    }

    self.deleteSindicato = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
angular.module('orcamentoApp').service('cidadesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Cidades';

    self.getCidades = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getCidade = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postCidade = function(cidade) {
        return $http.post(config.baseUrl + resource, cidade);
    }

    self.putCidade = function(id, cidade) {
        return $http.put(config.baseUrl + resource + '/' + id, cidade);
    }

    self.deleteCidade = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
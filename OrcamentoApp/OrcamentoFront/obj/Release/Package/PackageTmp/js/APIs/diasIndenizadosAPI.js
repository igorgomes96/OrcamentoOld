angular.module('orcamentoApp').service('diasIndenizadosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'DiasIndenizados';

    self.getDiasIndenizadosAll = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getDiasIndenizados = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postDiasIndenizados = function(diasIndenizados) {
        return $http.post(config.baseUrl + resource, diasIndenizados);
    }

    self.putDiasIndenizados = function(id, diasIndenizados) {
        return $http.put(config.baseUrl + resource + '/' + id, diasIndenizados);
    }

    self.deleteDiasIndenizados = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
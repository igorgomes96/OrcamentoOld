angular.module('orcamentoApp').service('setoresAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Setores';

    self.getSetores = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getSetor = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postSetor = function(setor) {
        return $http.post(config.baseUrl + resource, setor);
    }

    self.putSetor = function(id, setor) {
        return $http.put(config.baseUrl + resource + '/' + id, setor);
    }

    self.deleteSetor = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
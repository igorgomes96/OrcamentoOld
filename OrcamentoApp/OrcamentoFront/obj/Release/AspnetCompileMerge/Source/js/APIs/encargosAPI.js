angular.module('orcamentoApp').service('encargosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Encargos';

    self.getEncargos = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getEncargo = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postEncargo = function(encargo) {
        return $http.post(config.baseUrl + resource, encargo);
    }

    self.putEncargo = function(id, encargo) {
        return $http.put(config.baseUrl + resource + '/' + id, encargo);
    }

    self.deleteEncargo = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
angular.module('orcamentoApp').service('actsAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'ACTs';

    self.getACTs = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getACT = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postACT = function(ACT) {
        return $http.post(config.baseUrl + resource, ACT);
    }

    self.putACT = function(id, ACT) {
        return $http.put(config.baseUrl + resource + '/' + id, ACT);
    }

    self.deleteACT = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
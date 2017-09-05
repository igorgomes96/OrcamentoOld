angular.module('orcamentoApp').service('sthsAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'STHs';

    self.getSTHs = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getSTH = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postSTH = function(STH) {
        return $http.post(config.baseUrl + resource, STH);
    }

    self.putSTH = function(id, STH) {
        return $http.put(config.baseUrl + resource + '/' + id, STH);
    }

    self.deleteSTH = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
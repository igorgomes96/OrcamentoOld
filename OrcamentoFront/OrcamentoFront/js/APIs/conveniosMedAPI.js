angular.module('orcamentoApp').service('conveniosMedAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'ConveniosMedicos';

    self.getConveniosMeds = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getConveniosMed = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postConveniosMed = function(conveniosMed) {
        return $http.post(config.baseUrl + resource, conveniosMed);
    }

    self.putConveniosMed = function(id, conveniosMed) {
        return $http.put(config.baseUrl + resource + '/' + id, conveniosMed);
    }

    self.deleteConveniosMed = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
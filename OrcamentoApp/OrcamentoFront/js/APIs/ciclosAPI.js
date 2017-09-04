angular.module('orcamentoApp').service('ciclosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Ciclos';

    self.getCiclos = function(statusCod) {
        if (statusCod)
            return $http.get(config.baseUrl + resource + "?statusCod=" + statusCod);
        else
            return $http.get(config.baseUrl + resource);
    }

    self.getCiclo = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postCiclo = function(ciclo) {
        return $http.post(config.baseUrl + resource, ciclo);
    }

    self.putCiclo = function(id, ciclo) {
        return $http.put(config.baseUrl + resource + '/' + id, ciclo);
    }

    self.deleteCiclo = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
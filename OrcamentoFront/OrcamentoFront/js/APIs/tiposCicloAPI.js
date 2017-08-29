angular.module('orcamentoApp').service('tiposCicloAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'TiposCiclo';

    self.getTiposCiclo = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getTipoCiclo = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postTipoCiclo = function(tipoCiclo) {
        return $http.post(config.baseUrl + resource, tipoCiclo);
    }

    self.putTipoCiclo = function(id, tipoCiclo) {
        return $http.put(config.baseUrl + resource + '/' + id, tipoCiclo);
    }

    self.deleteTipoCiclo = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
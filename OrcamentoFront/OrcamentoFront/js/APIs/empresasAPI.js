angular.module('orcamentoApp').service('empresasAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Empresas';

    self.getEmpresas = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getEmpresa = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postEmpresa = function(empresa) {
        return $http.post(config.baseUrl + resource, empresa);
    }

    self.putEmpresa = function(id, empresa) {
        return $http.put(config.baseUrl + resource + '/' + id, empresa);
    }

    self.deleteEmpresa = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
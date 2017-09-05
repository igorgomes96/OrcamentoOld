angular.module('orcamentoApp').service('centrosCustosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'CentrosCustos';

    self.getCentrosCustos = function(codSetor, codEmpresa) {
        return $http({method: 'GET', url:config.baseUrl + resource, params:{codSetor: codSetor, codEmpresa: codEmpresa}});
    }

    self.getCentroCusto = function(matricula) {
        return $http.get(config.baseUrl + resource + '/' + matricula);
    }

    self.postCentroCusto = function(centroCusto) {
        return $http.post(config.baseUrl + resource, centroCusto);
    }

    self.putCentroCusto = function(matricula, centroCusto) {
        return $http.put(config.baseUrl + resource + '/' + matricula, centroCusto);
    }

    self.deleteCentroCusto = function(matricula) {
        return $http.delete(config.baseUrl + resource + '/' + matricula);
    }
}]);
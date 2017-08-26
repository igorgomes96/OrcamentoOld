angular.module('orcamentoApp').service('patAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'PATs';

    self.getPATs = function(sindicatoCod) {
        return $http.get(config.baseUrl + resource, {params:{sindicatoCod:sindicatoCod}});
    }

    self.getPAT = function(sindicatoCod, cargaHoraria) {
        return $http.get(config.baseUrl + resource + '/' + sindicatoCod + '/' + cargaHoraria);
    }

    self.postPAT = function(pat) {
        return $http.post(config.baseUrl + resource, pat);
    }

    self.putPAT = function(sindicatoCod, cargaHoraria, pat) {
        return $http.put(config.baseUrl + resource + '/' + sindicatoCod + '/' + cargaHoraria, pat);
    }

    self.deletePAT = function(sindicatoCod, cargaHoraria) {
        return $http.delete(config.baseUrl + resource + '/' + sindicatoCod + '/' + cargaHoraria);
    }
}]);
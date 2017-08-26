angular.module('orcamentoApp').service('reajustesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Reajustes';

    self.getReajustes = function(codSindicato, ano) {
        return $http.get(config.baseUrl + resource, {params:{codSindicato:codSindicato, ano:ano}});
    }

    self.getReajuste = function(codSindicato, ano) {
        return $http.get(config.baseUrl + resource + '/' + codSindicato + '/' + ano);
    }

    self.postReajuste = function(reajuste) {
        return $http.post(config.baseUrl + resource, reajuste);
    }

    self.putReajuste = function(codSindicato, ano, reajuste) {
        return $http.put(config.baseUrl + resource + '/' + codSindicato + '/' + ano, reajuste);
    }

    self.deleteReajuste = function(codSindicato, ano) {
        return $http.delete(config.baseUrl + resource + '/' + codSindicato + '/' + ano);
    }
}]);
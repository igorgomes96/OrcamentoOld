angular.module('orcamentoApp').service('feriasPorCRsAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'FeriasPorCRs';

    self.getFeriasPorCRs = function(cr, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{cr:cr, codCiclo:codCiclo}});
    }

    self.getFeriasPorCR = function(cr, mes) {
        return $http.get(config.baseUrl + resource + '/' + cr + '/' + mes);
    }

    self.postFeriasPorCR = function(FeriasPorCR) {
        return $http.post(config.baseUrl + resource, FeriasPorCR);
    }

    self.putFeriasPorCR = function(cr, mes, FeriasPorCR) {
        return $http.put(config.baseUrl + resource + '/' + cr + '/' + mes, FeriasPorCR);
    }

    self.deleteFeriasPorCR = function(cr, mes) {
        return $http.delete(config.baseUrl + resource + '/' + cr + '/' + mes);
    }
}]);
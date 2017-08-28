angular.module('orcamentoApp').service('feriasPorCRsAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'FeriasPorCRs';

    self.getFeriasPorCRs = function(cr) {
        return $http.get(config.baseUrl + resource, {params:{cr:cr}});
    }

    self.getFeriasPorCR = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postFeriasPorCR = function(FeriasPorCR) {
        return $http.post(config.baseUrl + resource, FeriasPorCR);
    }

    self.putFeriasPorCR = function(id, FeriasPorCR) {
        return $http.put(config.baseUrl + resource + '/' + id, FeriasPorCR);
    }

    self.deleteFeriasPorCR = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
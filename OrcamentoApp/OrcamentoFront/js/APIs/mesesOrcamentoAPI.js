angular.module('orcamentoApp').service('mesesOrcamentoAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'MesesOrcamento';

    self.getMesesOrcamento = function(idCiclo) {
        if (idCiclo)
            return $http({method: 'GET', url:config.baseUrl + resource, params:{idCiclo:idCiclo}});
        else
            return $http.get(config.baseUrl + resource);
    }

    self.getMesOrcamento = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postMesOrcamento = function(mesOrcamento) {
        return $http.post(config.baseUrl + resource, mesOrcamento);
    }

    self.putMesOrcamento = function(id, mesOrcamento) {
        return $http.put(config.baseUrl + resource + '/' + id, mesOrcamento);
    }

    self.deleteMesOrcamento = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
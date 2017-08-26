angular.module('orcamentoApp').service('transferenciasAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Transferencias';

    self.getTransferencias = function(centroCustoOrigem, centroCustoDestino, idCiclo) {
        /*if (centroCustoOrigem && centroCustoDestino)
            return $http({method: 'GET', url:config.baseUrl + resource, params:{centroCustoOrigem:centroCustoOrigem, centroCustoDestino:centroCustoDestino}});
        else if (centroCustoOrigem)
            return $http({method: 'GET', url:config.baseUrl + resource, params:{centroCustoOrigem:centroCustoOrigem}});
        else if (centroCustoDestino)
            return $http({method: 'GET', url:config.baseUrl + resource, params:{centroCustoDestino:centroCustoDestino}});          
        else
            return $http.get(config.baseUrl + resource);*/
        return $http({method: 'GET', url:config.baseUrl + resource, params:{centroCustoOrigem:centroCustoOrigem, centroCustoDestino:centroCustoDestino, idCiclo:idCiclo}});
    }

    self.getTransferencia = function(crDestino, funcMatricula) {
        return $http.get(config.baseUrl + resource + '/' + crDestino + '/' + funcMatricula);
    }

    self.postTransferencia = function(transferencia) {
        return $http.post(config.baseUrl + resource, transferencia);
    }

    self.putTransferencia = function(crDestino, funcMatricula, transferencia) {
        return $http.put(config.baseUrl + resource + '/' + crDestino + '/' + funcMatricula, transferencia);
    }

    self.deleteTransferencia = function(crDestino, funcMatricula) {
        return $http.delete(config.baseUrl + resource + '/' + crDestino + '/' + funcMatricula);
    }
}]);
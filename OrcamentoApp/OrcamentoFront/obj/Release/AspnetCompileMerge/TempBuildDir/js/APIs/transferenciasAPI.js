angular.module('orcamentoApp').service('transferenciasAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Transferencias';

    self.getTransferencias = function(centroCustoOrigem, centroCustoDestino, idCiclo, pendente) {
        return $http({method: 'GET', url:config.baseUrl + resource, params:{centroCustoOrigem:centroCustoOrigem, centroCustoDestino:centroCustoDestino, idCiclo:idCiclo, pendente:pendente}});
    }

    self.getTransferencia = function(crDestino, funcMatricula, mesTrans) {
        return $http.get(config.baseUrl + resource + '/' + crDestino + '/' + funcMatricula + '/' + mesTrans);
    }

    self.postTransferencia = function(transferencia) {
        return $http.post(config.baseUrl + resource, transferencia);
    }

    self.putTransferencia = function(crDestino, funcMatricula, mesTrans, transferencia) {
        return $http.put(config.baseUrl + resource + '/' + crDestino + '/' + funcMatricula + '/' + mesTrans, transferencia);
    }

    self.deleteTransferencia = function(crDestino, funcMatricula, mesTrans) {
        return $http.delete(config.baseUrl + resource + '/' + crDestino + '/' + funcMatricula + '/' + mesTrans);
    }
}]);
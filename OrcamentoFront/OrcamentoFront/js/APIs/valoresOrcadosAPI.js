angular.module('orcamentoApp').service('valoresOrcadosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'ValoresOrcados';

    self.getValoresOrcados = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getValoresOrcadosFolha = function(codCiclo, cr) {
    	return $http.get(config.baseUrl + resource + '/Folha', {params:{codCiclo:codCiclo, cr:cr}});
    }

    self.getValorOrcado = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postValorOrcado = function(ValorOrcado) {
        return $http.post(config.baseUrl + resource, ValorOrcado);
    }

    self.putValorOrcado = function(id, ValorOrcado) {
        return $http.put(config.baseUrl + resource + '/' + id, ValorOrcado);
    }

    self.deleteValorOrcado = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
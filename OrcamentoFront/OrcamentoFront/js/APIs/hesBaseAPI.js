angular.module('orcamentoApp').service('hesBaseAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'HEsBase';

    self.getHEsBase = function(percHoras, matricula, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{percHoras:percHoras, matricula:matricula, codCiclo:codCiclo}});
    }

    self.getFuncionarioHEs = function(matricula, codCiclo) {
        return $http.get(config.baseUrl + resource + '/FuncionarioHE/' + matricula + '/' + codCiclo);
    }

    self.postHEBase = function(HEBase) {
        return $http.post(config.baseUrl + resource, HEBase);
    }

    self.putHEBase = function(matricula, percentual, mesOrcamento, HEBase) {
        return $http.put(config.baseUrl + resource + '/' + matricula + '/' + percentual + '/' + mesOrcamento, HEBase);
    }

    self.deleteHEBase = function(matricula, percentual, mesOrcamento) {
        return $http.delete(config.baseUrl + resource + '/' + matricula + '/' + percentual + '/' + mesOrcamento);
    }
}]);
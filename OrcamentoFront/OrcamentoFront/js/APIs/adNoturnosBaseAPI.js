angular.module('orcamentoApp').service('adNoturnosBaseAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'AdNoturnosBase';

    self.getAdNoturnosBase = function(percHoras, matricula, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{percHoras:percHoras, matricula:matricula, codCiclo:codCiclo}});
    }

    self.getFuncionarioHNs = function(matricula, codCiclo) {
        return $http.get(config.baseUrl + resource + '/FuncionarioHN/' + matricula + '/' + codCiclo);
    }

    self.postAdNoturnoBase = function(AdNoturnoBase) {
        return $http.post(config.baseUrl + resource, AdNoturnoBase);
    }

    self.putAdNoturnoBase = function(matricula, percentual, mesOrcamento, AdNoturnoBase) {
        return $http.put(config.baseUrl + resource + '/' + matricula + '/' + percentual + '/' + mesOrcamento, AdNoturnoBase);
    }

    self.deleteAdNoturnoBase = function(matricula, percentual, mesOrcamento) {
        return $http.delete(config.baseUrl + resource + '/' + matricula + '/' + percentual + '/' + mesOrcamento);
    }
}]);
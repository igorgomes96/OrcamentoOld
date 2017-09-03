angular.module('orcamentoApp').service('adNoturnosContratacoesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'AdNoturnosContratacoes';

    self.getAdNoturnosContratacoes = function(percHoras, contratacao, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{percHoras:percHoras, contratacao:contratacao, codCiclo:codCiclo}});
    }

    self.getContratacaoHNs = function(contratacao, codCiclo) {
        return $http.get(config.baseUrl + resource + '/ContratacaoHN/' + contratacao + '/' + codCiclo);
    }

    self.postAdNoturnoContratacaoSaveAll = function(adNoturnos) {
        return $http.post(config.baseUrl + resource + '/SaveAll', adNoturnos);
    }

    self.postAdNoturnoContratacao = function(AdNoturnoContratacao) {
        return $http.post(config.baseUrl + resource, AdNoturnoContratacao);
    }

    self.putAdNoturnoContratacao = function(codContratacao, percentual, mesOrcamento, AdNoturnoContratacao) {
        return $http.put(config.baseUrl + resource + '/' + codContratacao + '/' + percentual + '/' + mesOrcamento, AdNoturnoContratacao);
    }

    self.deleteAdNoturnoContratacao = function(codContratacao, percentual, mesOrcamento) {
        return $http.delete(config.baseUrl + resource + '/' + codContratacao + '/' + percentual + '/' + mesOrcamento);
    }
}]);
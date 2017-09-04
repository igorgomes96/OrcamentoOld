angular.module('orcamentoApp').service('hesContratacoesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'HEsContratacoes';

    self.getHEsContratacoes = function(percHoras, codContratacao, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{percHoras:percHoras, codContratacao:codContratacao, codCiclo:codCiclo}});
    }

    self.getContratacaoHEs = function(codContratacao, codCiclo) {
        return $http.get(config.baseUrl + resource + '/ContratacaoHE/' + codContratacao + '/' + codCiclo);
    }

    self.postHEContratacaoSaveAll = function(hes) {
        return $http.post(config.baseUrl + resource + '/SaveAll', hes);
    }

    self.postHEContratacao = function(HEContratacao) {
        return $http.post(config.baseUrl + resource, HEContratacao);
    }

    self.putHEContratacao = function(codContratacao, percentual, mesOrcamento, HEContratacao) {
        return $http.put(config.baseUrl + resource + '/' + codContratacao + '/' + percentual + '/' + mesOrcamento, HEContratacao);
    }

    self.deleteHEContratacao = function(codContratacao, percentual, mesOrcamento) {
        return $http.delete(config.baseUrl + resource + '/' + codContratacao + '/' + percentual + '/' + mesOrcamento);
    }
}]);
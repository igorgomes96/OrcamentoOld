angular.module('orcamentoApp').service('solicitacoesContratacoesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'SolicitacoesContratacoes';

    self.getSolicitacoesContratacoes = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getSolicitacaoContratacao = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postSolicitacaoContratacao = function(solicitacaoContratacao) {
        return $http.post(config.baseUrl + resource, solicitacaoContratacao);
    }

    self.putSolicitacaoContratacao = function(id, solicitacaoContratacao) {
        return $http.put(config.baseUrl + resource + '/' + id, solicitacaoContratacao);
    }

    self.deleteSolicitacaoContratacao = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
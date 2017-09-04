angular.module('orcamentoApp').service('solicitacoesDesligamentosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'SolicitacoesDesligamentos';

    self.getSolicitacoesDesligamentos = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getSolicitacaoDesligamento = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postSolicitacaoDesligamento = function(solicitacaoDesligamento) {
        return $http.post(config.baseUrl + resource, solicitacaoDesligamento);
    }

    self.putSolicitacaoDesligamento = function(id, solicitacaoDesligamento) {
        return $http.put(config.baseUrl + resource + '/' + id, solicitacaoDesligamento);
    }

    self.deleteSolicitacaoDesligamento = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
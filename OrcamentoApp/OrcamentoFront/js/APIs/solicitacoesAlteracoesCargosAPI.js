angular.module('orcamentoApp').service('solicitacoesAlteracoesCargosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'SolicitacoesAlteracoesCargos';

    self.getSolicitacoesAlteracoesCargos = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getSolicitacaoAlteracaoCargo = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postSolicitacaoAlteracaoCargo = function(solicitacaoAlteracaoCargo) {
        return $http.post(config.baseUrl + resource, solicitacaoAlteracaoCargo);
    }

    self.putSolicitacaoAlteracaoCargo = function(id, solicitacaoAlteracaoCargo) {
        return $http.put(config.baseUrl + resource + '/' + id, solicitacaoAlteracaoCargo);
    }

    self.deleteSolicitacaoAlteracaoCargo = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
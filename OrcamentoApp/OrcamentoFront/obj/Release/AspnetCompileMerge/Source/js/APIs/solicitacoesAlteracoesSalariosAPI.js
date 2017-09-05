angular.module('orcamentoApp').service('solicitacoesAlteracoesSalariosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'SolicitacoesAlteracoesSalarios';

    self.getSolicitacoesAlteracoesSalarios = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getSolicitacaoAlteracaoSalario = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postSolicitacaoAlteracaoSalario = function(solicitacaoAlteracaoSalario) {
        return $http.post(config.baseUrl + resource, solicitacaoAlteracaoSalario);
    }

    self.putSolicitacaoAlteracaoSalario = function(id, solicitacaoAlteracaoSalario) {
        return $http.put(config.baseUrl + resource + '/' + id, solicitacaoAlteracaoSalario);
    }

    self.deleteSolicitacaoAlteracaoSalario = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
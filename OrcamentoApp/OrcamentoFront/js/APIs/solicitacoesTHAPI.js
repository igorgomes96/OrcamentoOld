angular.module('orcamentoApp').service('solicitacoesTHAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'SolicitacoesTH';

    self.getSolicitacoesTH = function(login) {
        return $http({method: 'GET', url:config.baseUrl + resource, params:{login: login}});
    }

    self.getSolicitacoesTHPorSetor = function(codSetor) {
        return $http.get(config.baseUrl + resource + '/PorSetor/' + codSetor);
    }

    self.getSolicitacaoTH = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postSolicitacaoTH = function(solicitacaoTH) {
        return $http.post(config.baseUrl + resource, solicitacaoTH);
    }

    self.putSolicitacaoTH = function(id, solicitacaoTH) {
        return $http.put(config.baseUrl + resource + '/' + id, solicitacaoTH);
    }

    self.deleteSolicitacaoTH = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
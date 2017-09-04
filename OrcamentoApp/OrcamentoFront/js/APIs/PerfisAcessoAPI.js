angular.module('orcamentoApp').service('perfisAcessoAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'PerfisAcesso';

    self.getPerfisAcesso = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getPerfilAcesso = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.putPerfilAcesso = function(id, perfilAcesso) {
        return $http.put(config.baseUrl + resource + '/' + id, perfilAcesso);
    }

}]);
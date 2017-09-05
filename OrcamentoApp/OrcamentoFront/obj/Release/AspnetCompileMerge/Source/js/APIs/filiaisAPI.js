angular.module('orcamentoApp').service('filiaisAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Filiais';

    self.getFiliais = function(empresaCod, cidadeNome) {
        if (empresaCod && cidadeNome)
            return $http({method: 'GET', url:config.baseUrl + resource, params:{empresaCod: empresaCod, cidadeNome: cidadeNome}});
        else if (empresaCod)
            return $http.get(config.baseUrl + resource + '?empresaCod=' + empresaCod);
        else if (cidadeNome)
            return $http.get(config.baseUrl + resource + '?cidadeNome=' & cidadeNome);
        else
            return $http.get(config.baseUrl + resource);
    }

    self.getFilial = function(empresaCod, cidadeNome) {
        return $http.get(config.baseUrl + resource + '/' + empresaCod + '/' + cidadeNome);
    }

    self.postFilial = function(filial) {
        return $http.post(config.baseUrl + resource, filial);
    }

    self.putFilial = function(empresaCod, cidadeNome, filial) {
        return $http.put(config.baseUrl + resource + '/' + empresaCod + '/' + cidadeNome, filial);
    }

    self.deleteFilial = function(empresaCod, cidadeNome) {
        return $http.delete(config.baseUrl + resource + '/' + empresaCod + '/' + cidadeNome);
    }
}]);
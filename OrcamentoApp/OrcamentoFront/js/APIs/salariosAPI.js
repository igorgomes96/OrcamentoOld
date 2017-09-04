angular.module('orcamentoApp').service('salariosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Salarios';

    self.getSalarios = function(cargoCod, cidadeNome, empresaCod) {
        return $http({method: 'GET', url:config.baseUrl + resource, params:{cargoCod:cargoCod, cidadeNome:cidadeNome, empresaCod:empresaCod}});
    }

    self.getSalariosPaged = function(pageNumber, pageSize, cargoCod, cidadeNome, empresaCod, consultaCargo) {
        return $http({method: 'GET', url:config.baseUrl + resource + '/Paged/' + pageNumber + '/' + pageSize, 
            params:{cargoCod:cargoCod, cidadeNome:cidadeNome, empresaCod:empresaCod, consultaCargo:consultaCargo}});
    }


    self.getSalario = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postSalario = function(salario) {
        return $http.post(config.baseUrl + resource, salario);
    }

    self.putSalario = function(id, salario) {
        return $http.put(config.baseUrl + resource + '/' + id, salario);
    }

    self.deleteSalario = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
angular.module('orcamentoApp').service('variaveisAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Variaveis';

    self.getVariaveisAll = function(empresaCod, cargoCod) {
        return $http.get(config.baseUrl + resource, {params:{empresaCod:empresaCod, cargoCod:cargoCod}});
    }

    self.getVariaveis = function(empresaCod, cargoCod, cargaHoraria) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postVariaveis = function(variaveis) {
        return $http.post(config.baseUrl + resource, variaveis);
    }

    self.putVariaveis = function(empresaCod, cargoCod, cargaHoraria, variaveis) {
        return $http.put(config.baseUrl + resource + '/' + id, variaveis);
    }

    self.deleteVariaveis = function(empresaCod, cargoCod, cargaHoraria) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
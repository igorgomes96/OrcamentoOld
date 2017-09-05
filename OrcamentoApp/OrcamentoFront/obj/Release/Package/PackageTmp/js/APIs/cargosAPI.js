angular.module('orcamentoApp').service('cargosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Cargos';

    self.getCargos = function(consulta) {
        if (consulta)
            return $http({method: 'GET', url:config.baseUrl + resource, params:{consulta:consulta}});
        else
            return $http.get(config.baseUrl + resource);
    }

    self.getCargosPaged = function(pageNumber, pageSize, consulta) {
        if (consulta)
            return $http({method: 'GET', url:config.baseUrl + resource + '/Paged/' + pageNumber + '/' + pageSize, params:{consulta:consulta}});
        else
            return $http.get(config.baseUrl + resource + '/Paged/' + pageNumber + '/' + pageSize);
    }

    self.getNumberOfPages = function(pageSize) {
        return $http.get(config.baseUrl + resource + '/NumberOfPages/' + pageSize);

    }

    self.getCHsPorCargo = function(empresaCod) {
        return $http({method: 'GET', url:config.baseUrl + resource + '/CHs', params:{empresaCod:empresaCod}});
    }

    self.getCargo = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postCargo = function(cargo) {
        return $http.post(config.baseUrl + resource, cargo);
    }

    self.putCargo = function(id, cargo) {
        return $http.put(config.baseUrl + resource + '/' + id, cargo);
    }

    self.deleteCargo = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
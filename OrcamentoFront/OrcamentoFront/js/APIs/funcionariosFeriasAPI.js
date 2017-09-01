angular.module('orcamentoApp').service('funcionariosFeriasAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'FuncionariosFerias';

    self.getFuncionariosFerias = function(matricula, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{matricula:matricula, codCiclo:codCiclo}});
    }

    self.getFuncionarioFerias = function(matricula, codMes) {
        return $http.get(config.baseUrl + resource + '/' + matricula + '/' + codMes);
    }

    self.saveAllFuncionariosFerias = function(ciclo, ferias) {
        return $http.post(config.baseUrl + resource + '/SaveAll/' + ciclo, ferias);
    }

    self.postFuncionarioFerias = function(funcionarioFerias) {
        return $http.post(config.baseUrl + resource, funcionarioFerias);
    }

    self.putFuncionarioFerias = function(matricula, codMes, funcionarioFerias) {
        return $http.put(config.baseUrl + resource + '/' + matricula + '/' + codMes, funcionarioFerias);
    }

    self.deleteFuncionarioFerias = function(matricula, codMes) {
        return $http.delete(config.baseUrl + resource + '/' + matricula + '/' + codMes);
    }
}]);
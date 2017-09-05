angular.module('orcamentoApp').service('funcionariosAPI', ['config', '$http', function(config, $http){

    var self = this;
    var resource = 'Funcionarios';

    self.getFuncionarios = function(cr, codCiclo) {
        return $http.get(config.baseUrl + resource, {params:{cr:cr, codCiclo:codCiclo}});
    }

    self.getFuncionarioHistorico = function(matricula, cr, ciclo) {
        return $http.get(config.baseUrl + resource + '/' + matricula + '/' + cr + '/' + ciclo);
    }

    self.getFuncionario = function(matricula) {
        return $http.get(config.baseUrl + resource + '/' + matricula);
    }

    self.saveAllFuncionarios = function(funcionarios) {
        return $http.post(config.baseUrl + resource + '/SaveAll', funcionarios);
    }

    self.postFuncionario = function(funcionario) {
        return $http.post(config.baseUrl + resource, funcionario);
    }

    self.putFuncionario = function(matricula, funcionario) {
        return $http.put(config.baseUrl + resource + '/' + matricula, funcionario);
    }

    self.deleteFuncionario = function(matricula) {
        return $http.delete(config.baseUrl + resource + '/' + matricula);
    }

}]);
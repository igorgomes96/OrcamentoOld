angular.module('orcamentoApp').service('funcionariosAPI', ['config', '$http', function(config, $http){

    var self = this;
    var resource = 'Funcionarios';

    self.getFuncionarios = function(cr) {
        if (cr)
            return $http.get(config.baseUrl + resource + "?cr=" + cr);
        else
            return $http.get(config.baseUrl + resource);
    }

    self.getFuncionario = function(matricula) {
        return $http.get(config.baseUrl + resource + '/' + matricula);
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
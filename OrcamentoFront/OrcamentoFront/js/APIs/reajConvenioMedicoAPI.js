angular.module('orcamentoApp').service('reajConvenioMedicoAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'ReajConvenioMedicos';

    self.getReajConvenioMedicos = function(ano, plano) {
        return $http.get(config.baseUrl + resource, {params:{ano:ano, plano:plano}});
    }

    self.getReajConvenioMedico = function(ano, plano) {
        return $http.get(config.baseUrl + resource + '/' + ano + '/' + plano);
    }

    self.postReajConvenioMedico = function(reajConvenioMedico) {
        return $http.post(config.baseUrl + resource, reajConvenioMedico);
    }

    self.putReajConvenioMedico = function(ano, plano, reajConvenioMedico) {
        return $http.put(config.baseUrl + resource + '/' + ano + '/' + plano, reajConvenioMedico);
    }

    self.deleteReajConvenioMedico = function(ano, plano) {
        return $http.delete(config.baseUrl + resource + '/' + ano + '/' + plano);
    }
}]);
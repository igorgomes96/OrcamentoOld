angular.module('orcamentoApp').service('reajConvenioMedicoAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'ReajConvenioMedicos';

    self.getReajConvenioMedicos = function(ano, plano) {
        return $http.get(config.baseUrl + resource, {params:{ano:ano, plano:plano}});
    }

    self.getReajConvenioMedico = function(ano, plano, mes) {
        return $http.get(config.baseUrl + resource + '/' + ano + '/' + plano + '/' + mes);
    }

    self.postReajConvenioMedicoSaveAll = function(reajustes) {
        return $http.post(config.baseUrl + resource + '/SaveAll', reajustes);
    }

    self.postReajConvenioMedico = function(reajConvenioMedico) {
        return $http.post(config.baseUrl + resource, reajConvenioMedico);
    }

    self.putReajConvenioMedico = function(ano, plano, mes, reajConvenioMedico) {
        return $http.put(config.baseUrl + resource + '/' + ano + '/' + plano + '/' + mes, reajConvenioMedico);
    }

    self.deleteReajConvenioMedico = function(ano, plano, mes) {
        return $http.delete(config.baseUrl + resource + '/' + ano + '/' + plano + '/' + mes);
    }
}]);
angular.module('orcamentoApp').service('eventosFolhaAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'EventosFolha';

    self.getEventosFolha = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getEventoFolha = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postEventoFolha = function(eventoFolha) {
        return $http.post(config.baseUrl + resource, eventoFolha);
    }

    self.putEventoFolha = function(id, eventoFolha) {
        return $http.put(config.baseUrl + resource + '/' + id, eventoFolha);
    }

    self.deleteEventoFolha = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
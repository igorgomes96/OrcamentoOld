angular.module('orcamentoApp').service('autenticacaoAPI', ['$http', 'config', function($http, config) {

	this.postLogin = function(user) {
		return $http.post(config.baseUrl + "Autentica", user);
	}

	this.putLogout = function(token) {
		return $http.put(config.baseUrl + "Logout", token);
	}

}]);
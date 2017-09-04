angular.module('orcamentoApp').service('localStorageService', ['$window', function($window) {
	var self = this;

	/*self.saveToken = function(token) {
		$window.localStorage.setItem('token', token);
	}

	self.getToken = function() {
		return $window.localStorage.getItem('token');
	}*/

	self.saveUser = function(user) {
		//if ($window.localStorage.getItem('user')) self.deleteUser();	
		$window.localStorage.setItem('user', JSON.stringify(user));
	}

	self.getUser = function() {
		var temp = $window.localStorage.getItem('user');
		if (temp) temp = JSON.parse(temp);
		return temp; 
	}

	self.deleteUser = function() {
		$window.localStorage.removeItem('user');
	}


}]);
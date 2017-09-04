angular.module("orcamentoApp").factory("anexaTokenInterceptor", ['localStorageService', function(localStorageService) {
	return {
		request: function(config) {
			var user = localStorageService.getUser();
			if (user && user.Token) 
				config.headers['Authorization'] = 'Basic ' + user.Token;
			return config;
		}
	}
}]);
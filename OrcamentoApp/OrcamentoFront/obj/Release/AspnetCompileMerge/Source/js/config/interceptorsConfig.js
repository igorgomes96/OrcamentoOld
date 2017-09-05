angular.module('orcamentoApp').config(['$httpProvider', function($httpProvider) {
	$httpProvider.interceptors.push('anexaTokenInterceptor');
	$httpProvider.interceptors.push('errorInterceptor');
}]);
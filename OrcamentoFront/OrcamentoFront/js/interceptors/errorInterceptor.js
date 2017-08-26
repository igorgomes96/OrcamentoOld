angular.module("orcamentoApp").factory("errorInterceptor", ['$q', '$location', 'messagesService', function($q, $location, messagesService) {		//$q implementação do angular para promisse
	return {
		responseError: function(rejection) {
			if (rejection.status === 401) {
				$location.path("/unauthenticated");
			} else {

				var mensagem = rejection.statusText + '. ';
				if (rejection.data && rejection.data.Message)
					mensagem = mensagem + rejection.data.Message;

				messagesService.exibeMensagemErro(rejection.status, mensagem);

			}
			return $q.reject(rejection);
		}
	}
}]);
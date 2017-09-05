angular.module("orcamentoApp").factory("errorInterceptor", ['$q', '$location', 'messagesService', 'sharedDataWithoutInjectionService', function($q, $location, messagesService, sharedDataWithoutInjectionService) {		//$q implementação do angular para promisse
	return {
		responseError: function(rejection) {
			if (rejection.status === 401) {
				$location.path("/unauthenticated");
			} else {
				if (sharedDataWithoutInjectionService.mensagensAutomaticas) {
					var mensagem = rejection.statusText + '. ';
					if (rejection.data && rejection.data.Message)
						mensagem = mensagem + rejection.data.Message;

					messagesService.exibeMensagemErro(rejection.status, mensagem);
				}

			}
			return $q.reject(rejection);
		}
	}
}]);
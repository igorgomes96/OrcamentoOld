angular.module('orcamentoApp').controller('autenticacaoCtrl', ['sharedDataService', 'localStorageService', 'autenticacaoAPI', '$state', function (sharedDataService, localStorageService, autenticacaoAPI, $state) {
	var self = this;


	self.erro = null;

	self.autentica = function(user) {
		autenticacaoAPI.postLogin(user)
		.then(function(dado) {
			//$state.go("menuContainer.dashboard");
			$state.go("containerHome.home");
			localStorageService.saveUser(dado.data);
			sharedDataService.setUsuario(dado.data);
		}, function(error) {

			console.log(error);
			if (error.data && error.status) {

				if (error.status == 400) {
					self.erro = {
						Title: "Informações Inválidas!",
						Message: error.data.Message
					}
				} else {
					self.erro = {
						Title: "Erro " + error.status + "!",
						Message: error.data.Message
					}
				}
				
			} else {

				self.erro = {
					Title: "Erro!",
					Message: "Ocorreu um erro inesperado!"
				}
				
			}

		});

	}

	self.fechaErro = function() {
		self.erro = null;
	}

	self.limpar = function(user) {
		if (user) {
			user.Login = "";
			user.Senha = "";
		}
	}


}]);
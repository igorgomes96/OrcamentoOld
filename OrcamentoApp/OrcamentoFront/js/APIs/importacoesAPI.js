angular.module('orcamentoApp').service('importacoesAPI', ['localStorageService', 'config', '$http', function(localStorageService, config, $http) {
	
	var self = this;
    var resource = 'Funcionarios';

    self.uploadFile = function(form) {

    	var user = localStorageService.getUser();
		var token = 'Basic ';

		if (user && user.Token) 
			token = token + user.Token;

    	var settings = {
		  "async": true,
		  "crossDomain": true,
		  "url": config.baseUrl + 'Importacoes/Upload',
		  "method": "POST",
		  "headers": {
		  	"authorization": token,
		    "cache-control": "no-cache",
		  },
		  "processData": false,
		  "contentType": false,
		  "mimeType": "multipart/form-data",
		  "data": form
		}

		return $.ajax(settings);
    }

}]);
angular.module('orcamentoApp').service('uploadFileService', ['messagesService', 'importacoesAPI', function(messagesService, importacoesAPI) {

	var self = this;

	self.sendFile = function() {

		var form = new FormData();
		form.append("arquivo", $('input[type=file]')[0].files[0]);
		//form.append("mes", self.mesAtual);

		return importacoesAPI.uploadFile(form);

	}

}]);
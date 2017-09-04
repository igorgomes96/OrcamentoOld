angular.module('orcamentoApp').controller('pessoalTransferenciasCtrl', ['mesesOrcamentoAPI', 'transferenciasAPI','funcionariosAPI', 'cargosAPI', '$scope', '$rootScope', 'sharedDataService', 'messagesService', function(mesesOrcamentoAPI, transferenciasAPI, funcionariosAPI, cargosAPI, $scope, $rootScope, sharedDataService, messagesService) {

	var self = this;
	self.ciclo = null;
	self.cr = null;
	self.transfRecebidas = [];
	self.transfEnviadas = [];

	var loadTransferenciasRecebidas = function(crDestino, idCiclo, pendente) {
		transferenciasAPI.getTransferencias(null, crDestino, idCiclo, pendente)
		.then(function(dado) {
			self.transfRecebidas = dado.data;
		});
	}

	var loadTransferenciasEnviadas = function(crOrigem, idCiclo) {
		transferenciasAPI.getTransferencias(crOrigem, null, idCiclo)
		.then(function(dado) {
			self.transfEnviadas = dado.data;
		});
	}

	self.aprovarTrans = function(transf) {
		transf.Aprovado = true;
		transferenciasAPI.putTransferencia(transf.CRDestino, transf.FuncionarioMatricula, transf.MesTransferencia, transf)
		.then(function(dado) {
			messagesService.exibeMensagemSucesso('Transferência aprovada!');
			loadTransferenciasRecebidas(self.cr.Codigo, self.ciclo.Codigo, true);
			$rootScope.$broadcast('transAprovada', transf.FuncionarioMatricula);
		});
	}

	self.rejeitarTrans = function(transf) {
		self.funcionarioTransf = transf;
	}

	self.salvarTransferenciaRejeitada = function(transf) {
		transf.Aprovado = false;
		transferenciasAPI.putTransferencia(transf.CRDestino, transf.FuncionarioMatricula, transf.MesTransferencia, transf)
		.then(function(dado) {
			$('#modal-rejeitar-transf').fadeOut();
            $('.modal-backdrop').fadeOut();
            $('body').removeClass('modal-open');
			messagesService.exibeMensagemSucesso('Transferência Rejeitada!');
			loadTransferenciasRecebidas(self.cr.Codigo, self.ciclo.Codigo, true);
		});
	}

	var listenerNovaTransf = $scope.$on('transCREvent', function() {
		loadTransferenciasEnviadas(self.cr.Codigo, self.ciclo.Codigo);
	});


	//Adiciona um listener para capturar as mudanças de seleção de CR
    var listenerCRChanged = $scope.$on('crChanged', function($event, cr) {
        if (self.ciclo && cr) {
        	self.cr = cr;
            loadTransferenciasRecebidas(cr.Codigo, self.ciclo.Codigo, true);
            loadTransferenciasEnviadas(cr.Codigo, self.ciclo.Codigo);
        } else {
            self.transfRecebidas = [];
			self.transfEnviadas = [];
        }
    });

    //Remove o Listener
    $scope.$on('$destroy', function () {
        listenerCRChanged();
        listenerNovaTransf();
    });


	self.ciclo = sharedDataService.getCicloAtual();

}]);
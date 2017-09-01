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

			var total = self.transfRecebidas.length, cont = 0;
			self.transfRecebidas.forEach(function(x) {
				funcionariosAPI.getFuncionario(x.FuncionarioMatricula)
				.then(function(retorno) {
					x.Funcionario = retorno.data;
					return mesesOrcamentoAPI.getMesOrcamento(x.MesTransferencia);
				}).then(function(retorno) {
					x.DataInicio = retorno.data.Mes;
					return cargosAPI.getCargo(x.Funcionario.CargoCod);
				}).then(function(retorno) {
					cont++;
					x.Cargo = retorno.data;
				});
			});

		});
	}

	var loadTransferenciasEnviadas = function(crOrigem, idCiclo) {
		transferenciasAPI.getTransferencias(crOrigem, null, idCiclo)
		.then(function(dado) {
			self.transfEnviadas = dado.data;

			self.transfEnviadas.forEach(function(x) {
				funcionariosAPI.getFuncionario(x.FuncionarioMatricula)
				.then(function(retorno) {
					x.Funcionario = retorno.data;
					return mesesOrcamentoAPI.getMesOrcamento(x.MesTransferencia);
				}).then(function(retorno) {
					x.DataInicio = retorno.data.Mes;
					return cargosAPI.getCargo(x.Funcionario.CargoCod);
				}).then(function(retorno) {
					x.Cargo = retorno.data;
				});
			});

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
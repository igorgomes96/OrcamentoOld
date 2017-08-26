angular.module('orcamentoApp').controller('premissasVariaveisCtrl', ['variaveisAPI', 'empresasAPI', 'cargosAPI', 'numberFilter', function(variaveisAPI, empresasAPI, cargosAPI, numberFilter) {

	var self = this;
	self.variaveis = [];
	self.empresas = [];
	self.cargos = [];
	self.filtroEmpresa = null;
	self.filtroCargo = null;

	self.loadVariaveis = function(empresaCod, cargoCod) {
		variaveisAPI.getVariaveisAll(empresaCod, cargoCod)
		.then(function(dado) {
			dado.data.forEach(function(x) {

				x.ParticipacaoLucros = numberFilter(x.ParticipacaoLucros * 100, 2);
				x.RemuneracaoVariavel = numberFilter(x.RemuneracaoVariavel * 100, 2);
				
				empresasAPI.getEmpresa(x.EmpresaCod)
				.then(function(retorno) {
					x.Empresa = retorno.data;
				});

				cargosAPI.getCargo(x.CargoCod)
				.then(function(retorno) {
					x.Cargo = retorno.data;
				});

			});

			self.variaveis = dado.data;

		});
	}

	var loadCargos = function() {
		cargosAPI.getCargos()
		.then(function(dado) {
			self.cargos = dado.data;
		});
	}

	var loadEmpresas = function() {
		empresasAPI.getEmpresas()
		.then(function(dado) {
			self.empresas = dado.data;
			self.filtroEmpresa = self.empresas && self.empresas.length > 0 ? self.empresas[0].Codigo : null;
			self.loadVariaveis(self.filtroEmpresa);
		});
	}

	loadEmpresas();
	loadCargos();


}]);
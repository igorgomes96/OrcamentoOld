angular.module('orcamentoApp').controller('premissasCargosCtrl', ['$scope', 'cargosAPI', 'cidadesAPI', 'empresasAPI', 'salariosAPI', 'conveniosMedAPI', 'numberFilter', function($scope, cargosAPI, cidadesAPI, empresasAPI, salariosAPI, conveniosMedAPI, numberFilter) {

	var self = this;
	self.planos = [];
	self.salarios = [];
	self.pageSize = 50;
	self.pageAtual = 1;
	self.filtroCidade = null;
	self.filtroEmpresa = null;
	self.filtroNomeCargo = null;
	self.empresas = [];
	self.cidades = [];
	self.pages = [];

	var loadConvenios = function() {
		conveniosMedAPI.getConveniosMeds()
		.then(function(dado) {
			self.planos = dado.data;
		});
	}

	$scope.$on('newBaseEvent', function(event) {
		self.loadSalarios(self.pageAtual, self.pageSize, null, self.filtroCidade, self.filtroEmpresa, self.filtroNomeCargo);

	});

	var loadEmpresas = function() {
		empresasAPI.getEmpresas()
		.then(function(dado) {
			self.empresas = dado.data;
		});
	}

	var loadCidades = function() {
		cidadesAPI.getCidades()
		.then(function(dado) {
			self.cidades = [];
			dado.data.forEach(function(x) {
				self.cidades.push(x.NomeCidade);
				self.filtroCidade = self.cidades[0];
				self.loadSalarios(self.pageAtual, self.pageSize, null, self.filtroCidade);
			});
		});
	}

	self.loadSalarios = function(pageAtual, pageSize, cargoCod, cidadeNome, empresaCod, filtroCargo) {
		salariosAPI.getSalariosPaged(pageAtual, pageSize, cargoCod, cidadeNome, empresaCod, filtroCargo)
		.then(function(dado) {

			//Obtem os Salarios
			self.salarios = dado.data.Salarios;

			self.salarios.forEach(function(salario) {
				empresasAPI.getEmpresa(salario.EmpresaCod)
				.then(function(retorno) {
					salario.EmpresaNome = retorno.data.Nome;
					return cargosAPI.getCargo(salario.CargoCod);
				}).then(function(retorno) {
					salario.Cargo = retorno.data;
				});
				salario.Faixa1 = numberFilter(salario.Faixa1, 2);
				salario.Faixa2 = numberFilter(salario.Faixa2, 2);
				salario.Faixa3 = numberFilter(salario.Faixa3, 2);
				salario.Faixa4 = numberFilter(salario.Faixa4, 2);
			});

			//Atualiza o vetor de páginas
			self.pages = [];
			for (var i = 1; i <= dado.data.NumberOfPages; i++) 
				self.pages.push(i);

			if (self.pageAtual > dado.data.NumberOfPages)
				self.pageAtual = dado.data.NumberOfPages;

			self.pageAtual = dado.data.Page;

		});
	}

	self.changeSizePage = function(newValue) {
		self.pageSize = newValue;
		self.loadSalarios(self.pageAtual, self.pageSize, null, self.filtroCidade, self.filtroEmpresa, self.filtroNomeCargo);
	}

	self.changePage = function(page) {
		if (page > self.pages.length || page < 1) return;
		self.pageAtual = page;
		self.loadSalarios(self.pageAtual, self.pageSize, null, self.filtroCidade, self.filtroEmpresa, self.filtroNomeCargo);
	}


	//loadConvenios();
	loadEmpresas();
	loadCidades();

}]);
angular.module("orcamentoApp").controller('navigationCtrl', ['$scope', '$state', 'sharedDataService', function($scope, $state, sharedDataService) {
	
	var self = this;

	self.getUser = function() {
		return sharedDataService.getUsuario();
	}


	self.stateAtual = function() {
		return $state.$current.name;
	}

	self.toggleExpand = function(menu, indice) {
		if (!$('#itemMenu' + indice).prev()[0]) return;
		$('#itemMenu' + indice).slideToggle('fast');
		var elem = $($('#itemMenu' + indice).prev()[0].children[1]);
		if (elem.hasClass('fa-angle-down')) {
			elem.removeClass('fa-angle-down');
			elem.addClass('fa-angle-left');
		} else if (elem.hasClass('fa-angle-left')) {
			elem.addClass('fa-angle-down');
			elem.removeClass('fa-angle-left');
		}
	}

	var setMenu = function() {
		var user = self.getUser();


		self.menu = [
			{
				state: 'menuContainer.dashboard',
				icone: 'fa fa-dashboard fa-fw',
				texto: 'Dashboard',
				subMenu: [],
				visible: true
			},
			{
				state: 'menuContainer.pessoalOrcamento({codCiclo:1})',
				icone: 'fa fa-money fa-fw',
				texto: 'Orçamento',
				subMenu: [],
				visible: user.Perfil == 'Administrador' || user.Perfil == 'Gestor de CR'
			},
			{
				state: 'menuContainer.simulacoes',
				icone: 'fa fa-object-group fa-fw',
				texto: 'Simulações',
				subMenu: [],
				visible: user.Perfil == 'Administrador' || user.Perfil == 'BP'
			},
			{
				state: '#',
				icone: 'fa fa-pencil fa-fw',
				texto: 'Premissas',
				visible: user.Perfil == 'Administrador',
				subMenu: [
					{
						state: 'menuContainer.premissasEncargos',
						texto: 'Encargos',
						visible: true
					},
					{
						state: 'menuContainer.premissasCargos',
						texto: 'Cargos e Salários',
						visible: true
					},
					{
						state: 'menuContainer.premissasBeneficios',
						texto: 'Benefícios',
						visible: true
					},
					/*{
						state: 'menuContainer.premissasConvenioMedico',
						texto: 'Convênios Médicos',
						visible: true
					},*/
					{
						state: 'menuContainer.premissasSindicatos',
						texto: 'Sindicatos',
						visible: true
					}
				]
			},
			{
				state: '#',
				icone: 'fa fa-users fa-fw',
				texto: 'Gestão de Funcionários',
				visible: user.Perfil == 'Administrador' || user.Perfil == 'Gestor de CR' || user.Perfil == 'BP',
				subMenu: [
					{
						state: 'menuContainer.filaSolicitacoes',
						texto: 'Fila de Solicitações',
						visible: user.Perfil == 'Administrador' || user.Perfil == 'BP'
					},
					{
						state: 'menuContainer.solicitacoesTH',
						texto: 'Solicitação ao TH',
						visible: user.Perfil == 'Administrador' || user.Perfil == 'Gestor de CR'
					},
					{
						state: '#',
						texto: 'Relatório',
						visible: true
					}
				]
			},
			{
				state: 'menuContainer.gestaoUsuarios',
				icone: 'fa fa-user fa-fw',
				visible: user.Perfil == 'Administrador',
				texto: 'Gestão de Usuários'
			}
		];

	}


	//Adiciona um listener para capturar as mudanças de seleção de CR
    var listener = $scope.$on('transferenciasNotify', function($event, transferencias) {
        self.transferencias = transferencias;
    });

    //Remove o Listener
    $scope.$on('$destroy', function () {
        listener();
    });

    setMenu();


}]);

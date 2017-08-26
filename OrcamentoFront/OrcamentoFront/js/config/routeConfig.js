angular.module('orcamentoApp').config(['$stateProvider', '$urlRouterProvider', '$locationProvider', function($stateProvider, $urlRouterProvider, $locationProvider) {

    $locationProvider.hashPrefix('');
    $urlRouterProvider.otherwise('/autenticacao');
    
    $stateProvider

    .state('autenticacao', {
        url: '/autenticacao',
        templateUrl: 'components/autenticacao/autenticacao.html',
        controller: 'autenticacaoCtrl as ct'
    })

    .state('containerHome', {
        templateUrl: 'components/home/containerHome.html'
    })

    .state('containerHome.home', {
        url: '/home',
        templateUrl: 'components/home/homeAdministrador.html'/*,
        controller: 'homeAdministradorCtrl as ct'*/
    })

    .state('containerHome.selecaoCiclo', {
        url: '/selecaoCiclo',
        templateUrl: 'components/home/selecaoCiclo.html',
        controller: 'selecaoCicloCtrl as ct'
    })

    .state('containerHome.selecaoModulo', {
        url: '/selecaoModulo',
        templateUrl: 'components/home/selecaoModulo.html',
        controller: 'selecaoModuloCtrl as ct'
    })

    .state('menuContainer', {
        templateUrl: 'components/menuContainer/menuContainer.html'
    })

    .state('menuContainer.dashboard', {
        url: '/dashboard',
        templateUrl: 'components/dashboard/dashboard.html',
        controller: 'dashboardCtrl as ct'
    })

    .state('menuContainer.unauthenticated', {
        url: '/unauthenticated',
        templateUrl: 'components/autenticacao/unauthenticated.html'
    })

    .state('menuContainer.pessoalOrcamento', {
        url: '/pessoalOrcamento',
        views: {
            '': {
                templateUrl: 'components/pessoalOrcamento/pessoalOrcamento.html', 
                controller: 'pessoalOrcamentoCtrl as ctMain'
            },
            'pessoalBase@menuContainer.pessoalOrcamento': {
                templateUrl: 'components/pessoalOrcamento/pessoalBase/pessoalBase.html', 
                controller: 'pessoalBaseCtrl as ct'
            },
            'pessoalContratacoes@menuContainer.pessoalOrcamento': {
                templateUrl: 'components/pessoalOrcamento/pessoalContratacoes/pessoalContratacoes.html', 
                controller: 'pessoalContratacoesCtrl as ct'
            },
            'pessoalTransferenciasEnviadas@menuContainer.pessoalOrcamento': {
                templateUrl: 'components/pessoalOrcamento/pessoalTransferencias/pessoalTransferenciasEnviadas.html', 
                controller: 'pessoalTransferenciasCtrl as ct'
            },
            'pessoalTransferenciasRecebidas@menuContainer.pessoalOrcamento': {
                templateUrl: 'components/pessoalOrcamento/pessoalTransferencias/pessoalTransferenciasRecebidas.html', 
                controller: 'pessoalTransferenciasCtrl as ct'
            }
        }/*,
        resolve: {
            cicloResolve: function($stateParams, ciclosAPI) {
                if (!$stateParams.codCiclo) return null;
                return ciclosAPI.getCiclo($stateParams.codCiclo);
            }
        }*/
    })

    .state('menuContainer.solicitacoesTH', {
        url: '/solicitacoesTH',
        views: {
            '': {
                templateUrl: 'components/solicitacoesTH/solicitacoesTH.html', 
                controller: 'solicitacoesTHCtrl as ctMain'
            },
            'solicitacoes@menuContainer.solicitacoesTH': {
                templateUrl: 'components/solicitacoesTH/solicitacoesTHSolicitacoes/solicitacoesTHSolicitacoes.html', 
            },
            'historico@menuContainer.solicitacoesTH': {
                templateUrl: 'components/solicitacoesTH/solicitacoesTHHistorico/solicitacoesTHHistorico.html', 
                controller: 'solicitacoesTHHistoricoCtrl as ct'
            }
        },
        resolve: {
            pageResolve: function() {
                return 'Histórico';
            }
        }
    })

    .state('menuContainer.simulacoes', {
        url: '/simulacoes',
        views: {
            '': {
                templateUrl: 'components/temp/simulacoes.html'
            }
        }
    })

    .state('menuContainer.filaSolicitacoes', {
        url: '/filaSolicitacoes',
        templateUrl: 'components/solicitacoesTH/solicitacoesTHFila/solicitacoesTHFila.html',
        controller: 'solicitacoesTHHistoricoCtrl as ct',
        resolve: {
            pageResolve: function() {
                return 'Fila';
            }
        }       
    })

    .state('menuContainer.premissasEncargos', {
        url: '/premissasEncargos',
        views: {
            '': {
                templateUrl: 'components/premissas/encargos/encargos.html', 
                controller: 'premissasCtrl as ctMain'
            },
            'porEmpresa@menuContainer.premissasEncargos': {
                templateUrl: 'components/premissas/encargos/encargosPorEmpresa.html', 
                controller: 'encargosPorEmpresaCtrl as ct'
            },
            'porFilial@menuContainer.premissasEncargos': {
                templateUrl: 'components/premissas/encargos/encargosPorFilial.html', 
                controller: 'encargosPorFilialCtrl as ct'
            }
        }
    })

    .state('menuContainer.premissasCargos', {
        url: '/premissasCargos',
        views: {
            '': {
                templateUrl: 'components/premissas/cargosSalarios/premissasCargosSalarios.html', 
                controller: 'premissasCtrl as ctMain'
            },
            'cargos@menuContainer.premissasCargos': {
                templateUrl: 'components/premissas/cargosSalarios/premissasCargos.html', 
                controller: 'premissasCargosCtrl as ct'
            }
        }
    })

    .state('menuContainer.premissasSindicatos', {
        url: '/premissasReajustes',
        views: {
            '': {
                templateUrl: 'components/premissas/sindicatos/premissasSindicatosContainer.html', 
                controller: 'premissasCtrl as ctMain'
            },
            'sindicatos@menuContainer.premissasSindicatos': {
                templateUrl: 'components/premissas/sindicatos/premissasSindicatos.html', 
                controller: 'premissasSindicatosCtrl as ct'
            }
        }
    })

    .state('menuContainer.premissasBeneficios', {
        url: '/premissasBeneficios',
        views: {
            '': {
                templateUrl: 'components/premissas/beneficios/premissasBeneficios.html', 
                controller: 'premissasCtrl as ctMain'
            },
            'variaveis@menuContainer.premissasBeneficios': {
                templateUrl: 'components/premissas/beneficios/premissasVariaveis.html', 
                controller: 'premissasVariaveisCtrl as ct'
            },
            'pat@menuContainer.premissasBeneficios': {
                templateUrl: 'components/premissas/beneficios/premissasPat.html', 
                controller: 'premissasPatCtrl as ct'
            },
            'vt@menuContainer.premissasBeneficios': {
                templateUrl: 'components/premissas/beneficios/premissasVT.html', 
                controller: 'premissasVTCtrl as ct'
            },
            'planosConvenio@menuContainer.premissasBeneficios': {
                templateUrl: 'components/premissas/beneficios/premissasConvenioMedico.html', 
                controller: 'premissasConvenioMedicoCtrl as ct'
            },
            'reajustesConvenio@menuContainer.premissasBeneficios': {
                templateUrl: 'components/premissas/beneficios/premissasReajConvenioMedico.html', 
                controller: 'premissasReajConvenioMedicoCtrl as ct'
            }
        }
    })

    .state('menuContainer.gestaoUsuarios', {
        url: '/gestaoUsuarios',
        views: {
            '': {
                templateUrl: 'components/usuarios/containerUsuarios.html'
            },
            'usuarios@menuContainer.gestaoUsuarios': {
                templateUrl: 'components/usuarios/usuarios.html', 
                controller: 'gestaoUsuariosCtrl as ct'
            }
        }
    });
    
}]);
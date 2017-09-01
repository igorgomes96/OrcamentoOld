angular.module('orcamentoApp').controller('pessoalBaseCtrl', ['messagesService', 'transferenciasAPI', 'funcionariosAPI', '$scope', 'sharedDataService', 'cargosAPI', '$rootScope', 'hesBaseAPI', 'adNoturnosBaseAPI', 'valoresAbertosBaseAPI', 'numberFilter', 'sharedDataWithoutInjectionService', 'calculosEventosBaseAPI', function(messagesService, transferenciasAPI, funcionariosAPI, $scope, sharedDataService, cargosAPI, $rootScope, hesBaseAPI, adNoturnosBaseAPI, valoresAbertosBaseAPI, numberFilter, sharedDataWithoutInjectionService, calculosEventosBaseAPI) {

	var self = this;

    sharedDataWithoutInjectionService.mensagensAutomaticas = false;
    self.cr = null;
    self.funcionarioTransf = null;
    self.funcionarios = [];
    self.outros = [];
    self.adNoturnos = [];
    self.horasNoturnasAbertas = [];
    self.horasExtras = [];
    self.horasExtrasAbertas = [];
    $scope.aba = "Associados";

    var outrosInsumos = sharedDataService.getOutrosInsumosFolha();

	var loadFuncionarios = function(cr) {
        funcionariosAPI.getFuncionarios(cr)
        .then(function(dado) {

            dado.data.forEach(function(x) {
                cargosAPI.getCargo(x.CargoCod)
                .then(function(dado2){
                    x.Cargo = dado2.data;
                });
            });

            self.funcionarios = dado.data;
            loadHorasExtras();
            loadAdicionaisNoturnos();
            loadOutros();

        }, function(error) {
            console.log(error);
        });
    }

    var loadHorasExtras = function() {
        self.horasExtras = [];
        self.horasExtrasAbertas = [];

        self.funcionarios.forEach(function(x) {
            hesBaseAPI.getFuncionarioHEs(x.Matricula, self.ciclo.Codigo)
            .then(function(dado) {
                self.horasExtras.push(dado.data);
            });
        });

    }

    var loadAdicionaisNoturnos = function() {
        self.adNoturnos = [];
        self.horasNoturnasAbertas = [];

        self.funcionarios.forEach(function(x) {
            adNoturnosBaseAPI.getFuncionarioHNs(x.Matricula, self.ciclo.Codigo)
            .then(function(dado) {
                self.adNoturnos.push(dado.data);
            });
        });

    }

    var loadOutros = function() {
        self.outros = [];

        self.funcionarios.forEach(function(x) {
            valoresAbertosBaseAPI.getValoresAbertosBase(null, x.Matricula, self.ciclo.Codigo)
            .then(function(dado) {

                
                var f = {
                    Matricula: x.Matricula,
                    Nome: x.Nome
                };

                cargosAPI.getCargo(x.CargoCod)
                .then(function(retorno){
                    f.Cargo = retorno.data.NomeCargo;
                });

                
                outrosInsumos.forEach(function(z) {
                    f[z] = [];
                    self.ciclo.Meses.forEach(function(y) {
                        f[z].push({
                            CodEvento: z,
                            CodMesOrcamento: y.Codigo,
                            MatriculaFuncionario: x.Matricula,
                            Valor: 0
                        });
                    });
                });
                

                dado.data.forEach(function(y) {
                    if (f.hasOwnProperty(y.CodEvento)) {

                        var fun = f[y.CodEvento].filter(function(z) {
                            return z.CodMesOrcamento == y.CodMesOrcamento;
                        });

                        if (fun && fun.length > 0) {
                            fun[0].Valor = y.Valor;
                        }

                    }
                });

                self.outros.push(f);

            });
        });


    }


    self.mudaAba = function(nova) {
        $scope.aba = nova;
    }


    self.saveAll = function() {

        exibeLoader();
        funcionariosAPI.saveAllFuncionarios(self.funcionarios)
        .then(function(dado) {
            return hesBaseAPI.saveAllFuncionariosHEs(self.horasExtras);
        }).then(function(dado) {
            return adNoturnosBaseAPI.saveAllFuncionariosHNs(self.adNoturnos);
        }).then(function(dado) {
            messagesService.exibeMensagemSucesso("Informações salvas com sucesso! Os cálculos estão sendo realizados em background.");
            ocultaLoader();
            return calculosEventosBaseAPI.postCalculaBasePorCR(self.cr.Codigo, self.ciclo.Codigo);
        }).then(function(dado) {
            $rootScope.$broadcast('calculoRealizado');
        });

        /*self.funcionarios.forEach(function(x) {
            funcionariosAPI.putFuncionario(x.Matricula, x);
        });

        self.horasExtras.forEach(function(x) {
            var listas = ['HEs170', 'HEs100', 'HEs75', 'HEs60', 'HEs50'];

            listas.forEach(function(y) {
                x[y].forEach(function(z) {

                    z.FuncionarioMatricula = x.Matricula;

                    if (z.QtdaHoras == 0) {
                        hesBaseAPI.deleteHEBase(z.FuncionarioMatricula, z.PercentualHoras, z.CodMesOrcamento);
                    } else {
                        hesBaseAPI.postHEBase(z)
                        .then(function(){}, function(error) {
                            if (error.status && error.status == 201) {
                                hesBaseAPI.putHEBase(z.FuncionarioMatricula, z.PercentualHoras, z.CodMesOrcamento, z);
                            }
                        });
                    }
                });
            });
        });

        self.adNoturnos.forEach(function(x) {
            var listas = ['HNs20', 'HNs30', 'HNs40', 'HNs50', 'HNs60'];

            listas.forEach(function(y) {
                x[y].forEach(function(z) {

                    z.FuncionarioMatricula = x.Matricula;

                    if (z.QtdaHoras == 0) {
                        adNoturnosBaseAPI.deleteAdNoturnoBase(z.FuncionarioMatricula, z.PercentualHoras, z.CodMesOrcamento);
                    } else {
                        adNoturnosBaseAPI.postAdNoturnoBase(z)
                        .then(function(){}, function(error) {
                            if (error.status && error.status == 201) {
                                adNoturnosBaseAPI.putAdNoturnoBase(z.FuncionarioMatricula, z.PercentualHoras, z.CodMesOrcamento, z);
                            }
                        });
                    }
                });
            });
        });


        self.outros.forEach(function(x) {
            for (var prop in x) {
                if (Array.isArray(x[prop])) {
                    x[prop].forEach(function(y) {
                        if (y.Valor == 0)
                            valoresAbertosBaseAPI.deleteValorAbertoBase(y.CodEvento, y.CodMesOrcamento, y.MatriculaFuncionario);
                        else {
                            valoresAbertosBaseAPI.postValorAbertoBase(y)
                            .then(function() {}, function(error) {
                                if (error.status && error.status == 201) {
                                    valoresAbertosBaseAPI.putValorAbertoBase(y.CodEvento, y.CodMesOrcamento, y.MatriculaFuncionario, y);
                                }
                            });
                        }
                    });
                }
            }
        });*/

        //messagesService.exibeMensagemSucesso("Informações salvas com sucesso!");

    }

    /*self.saveFuncionarios = function(funcionarios) {
    	funcionarios.forEach(function(x) {
            funcionariosAPI.putFuncionario(x.Matricula, x);
    	});
        messagesService.exibeMensagemSucesso("Alterações salvas com sucesso!");
    }*/
    
    self.salvarTransferencia = function(transf) {
        var copia = angular.copy(transf.Funcionario);
        delete transf.Funcionario;
        transferenciasAPI.postTransferencia(transf)
        .then(function(dado) {
            messagesService.exibeMensagemSucesso("Solicitação de Transferência enviada com sucesso!");
            $('#modal-tranferir-cr').fadeOut();
            $('.modal-backdrop').fadeOut();
            $('body').removeClass('modal-open');
            $rootScope.$broadcast('transCREvent');
        }, function(error) {
            transf.Funcionario = copia;
        });
    }

    self.transferir = function(funcionario) {
        self.funcionarioTransf = {
            CRDestino: "",
            FuncionarioMatricula: funcionario.Matricula,
            Funcionario: funcionario,
            Status: "Aguardando Aprovação",
            DataSolicitacao: new Date(),
            MesTransferencia: self.ciclo.Meses[0].Codigo
        }
    }

    self.verificarFerias = function(funcionario) {
    	if (!funcionario.MesFerias) return;
    	if (funcionario.MesFerias > funcionario.MesDesligamento)
    		funcionario.MesFerias = null;
    }

    self.verificarDesligamento = function(funcionario) {
    	if (!funcionario.MesDesligamento) return;
    	if (funcionario.MesFerias > funcionario.MesDesligamento)
    		funcionario.MesDesligamento = null;
    }


    //Adiciona um listener para capturar as mudanças de seleção de CR
    var listenerCR = $scope.$on('crChanged', function($event, cr) {
        if (cr) {
            self.cr = cr;
            loadFuncionarios(self.cr.Codigo);
            //mudaAba();
        }
        else
            self.funcionarios = [];
    });


    //Remove o Listener
    $scope.$on('$destroy', function () {
        listenerCR();
    });

    self.ciclo = sharedDataService.getCicloAtual();


}]);
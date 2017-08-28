angular.module('orcamentoApp').controller('pessoalBaseCtrl', ['messagesService', 'transferenciasAPI', 'funcionariosAPI', '$scope', 'sharedDataService', 'cargosAPI', '$rootScope', 'hesBaseAPI', 'adNoturnosBaseAPI', 'valoresAbertosCRsAPI', 'valoresAbertosBaseAPI', 'feriasPorCRsAPI', 'numberFilter', function(messagesService, transferenciasAPI, funcionariosAPI, $scope, sharedDataService, cargosAPI, $rootScope, hesBaseAPI, adNoturnosBaseAPI, valoresAbertosCRsAPI, valoresAbertosBaseAPI, feriasPorCRsAPI, numberFilter) {

	var self = this;

    self.cr = null;
    self.funcionarioTransf = null;
    self.funcionarios = [];
    self.outros = [];
    self.adNoturnos = [];
    self.horasNoturnasAbertas = [];
    self.horasExtras = [];
    self.horasExtrasAbertas = [];
    self.ferias = [];
    $scope.aba = "Associados";

    var outrosInsumos = ['GRATIF','PRODUT','COMISSAO','PRO-LAB','PGP-PGI','AJU-CUST'];

	var loadFuncionarios = function(cr) {
        funcionariosAPI.getFuncionarios(cr)
        .then(function(dado) {
            self.funcionarios = dado.data;

            self.funcionarios.forEach(function(x) {
                cargosAPI.getCargo(x.CargoCod)
                .then(function(dado2){
                    x.Cargo = dado2.data;
                });
            });
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

        valoresAbertosCRsAPI.getValoresAbertosCRs(self.cr.Codigo, 'HE-ABERTO', self.ciclo.Codigo)
        .then(function(dado) {
            self.horasExtrasAbertas = dado.data;
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

        valoresAbertosCRsAPI.getValoresAbertosCRs(self.cr.Codigo, 'AD-ABERTO', self.ciclo.Codigo)
        .then(function(dado) {
            self.horasNoturnasAbertas = dado.data;
        });
    }

    var loadOutros = function() {
        self.outros = [];

        self.funcionarios.forEach(function(x) {
            valoresAbertosBaseAPI.getValoresAbertosBase(null, x.Matricula, self.ciclo.Codigo)
            .then(function(dado) {

                
                var f = {
                    Matricula: x.Matricula,
                    Nome: x.Nome,
                    Cargo: x.Cargo.NomeCargo
                };

                
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

    self.loadFerias = function(cr) {
        var temp = [];
        self.ciclo.Meses.forEach(function(y) {
            temp.push({
                CodigoCR: cr,
                CodMesOrcamento: y.Codigo,
                Percentual: 0
            });
        });

        feriasPorCRsAPI.getFeriasPorCRs(cr)
        .then(function(dado) {

            dado.data.forEach(function(x) {
                var f = temp.filter(function(y) {
                    return y.CodMesOrcamento == x.CodMesOrcamento;
                });
                if (f && f.length > 0) {
                    f[0].Percentual = numberFilter(x.Percentual * 100, 2);
                }
            });

        });

        self.ferias = temp;
    }

    self.mudaAba = function(nova) {
        $scope.aba = nova;
    }

    self.saveFuncionarios = function(funcionarios) {
        var cont = 0; total = funcionarios.length;
    	funcionarios.forEach(function(x) {
            funcionariosAPI.putFuncionario(x.Matricula, x);
    	});
        messagesService.exibeMensagemSucesso("Alterações salvas com sucesso!");
    }
    
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
            mudaAba();
        }
        else
            self.funcionarios = [];
    });


    //Remove o Listener
    $scope.$on('$destroy', function () {
        listenerCR();
    });

    mudaAba = function() {
        if ($scope.aba == "Associados") {
            if (self.funcionarios.length == 0 && self.cr){
                loadFuncionarios(self.cr.Codigo);
            }
        } else if ($scope.aba == "Horas Extras") {
            if (self.horasExtras.length == 0 || self.horasExtrasAbertas.length == 0){
                loadHorasExtras();
            }
        } else if ($scope.aba == "Adicional Noturno") {
            if (self.adNoturnos.length == 0){
                loadAdicionaisNoturnos();
            }
        } else if ($scope.aba == "Gratificações" || $scope.aba == "Produtividade" || $scope.aba == "Comissão" || $scope.aba == "Outros") {
            if (self.outros.length == 0){
                loadOutros();
            }
        }
    }


    $scope.$watch('aba', function() {
        mudaAba();
    });

    //loadFuncionarios();
    self.ciclo = sharedDataService.getCicloAtual();


}]);
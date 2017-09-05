angular.module('orcamentoApp').controller('pessoalBaseCtrl', ['messagesService', 'transferenciasAPI', 'funcionariosAPI', '$scope', 'sharedDataService', 'cargosAPI', '$rootScope', 'hesBaseAPI', 'adNoturnosBaseAPI', 'valoresAbertosBaseAPI', 'numberFilter', 'sharedDataWithoutInjectionService', 'calculosEventosBaseAPI', 'funcionariosFeriasAPI', function(messagesService, transferenciasAPI, funcionariosAPI, $scope, sharedDataService, cargosAPI, $rootScope, hesBaseAPI, adNoturnosBaseAPI, valoresAbertosBaseAPI, numberFilter, sharedDataWithoutInjectionService, calculosEventosBaseAPI, funcionariosFeriasAPI) {

	var self = this;

    sharedDataWithoutInjectionService.mensagensAutomaticas = false;
    self.cr = null;
    self.funcionarioTransf = null;
    self.funcionarios = [];
    self.erroTransf = null;
    $scope.aba = "Associados";

    var outrosInsumos = sharedDataService.getOutrosInsumosFolha();

	var loadFuncionarios = function(cr, ciclo) {
        funcionariosAPI.getFuncionarios(cr, ciclo)
        .then(function(dado) {

            dado.data.forEach(function(x) {
                cargosAPI.getCargo(x.CargoCod)
                .then(function(dado2){
                    x.Cargo = dado2.data;
                });
            });

            self.funcionarios = dado.data;
            loadHorasExtras(self.funcionarios);
            loadAdicionaisNoturnos(self.funcionarios);
            loadOutros(self.funcionarios);
            loadFerias(self.funcionarios);

        }, function(error) {
            console.log(error);
        });
    }

    var setMesesVisivel = function(funcionario, meses) {
        meses.forEach(function(y) { //Para cada mês
            y.Visivel = funcionario.Historico.length == 0 || funcionario.Historico.some(function(z) {
                return (z.Inicio == null && z.Fim >= y.Mes) || (z.Fim == null && z.Inicio < y.Mes) || (y.Mes >= z.Inicio && y.Mes < z.Fim);
            });
        });
    }

    var loadHorasExtras = function(funcionarios) {
        self.horasExtras = [];

        funcionarios.forEach(function(x) {
            hesBaseAPI.getFuncionarioHEs(x.Matricula, self.ciclo.Codigo)
            .then(function(dado) {
                for (var prop in dado.data) {
                    if (Array.isArray(dado.data[prop])) {  // Se for um array (HEs170, HEs100...)
                        setMesesVisivel(x, dado.data[prop]);
                    }
                }
                x.horasExtras = dado.data;
            });
        });

    }

    var loadAdicionaisNoturnos = function(funcionarios) {
        self.adNoturnos = [];

        funcionarios.forEach(function(x) {
            adNoturnosBaseAPI.getFuncionarioHNs(x.Matricula, self.ciclo.Codigo)
            .then(function(dado) {
                for (var prop in dado.data) {
                    if (Array.isArray(dado.data[prop])) {  // Se for um array (HNs20, HNs30...)
                        setMesesVisivel(x, dado.data[prop]);
                    }
                }
                x.adNoturnos = dado.data;
            });
        });

    }

    var loadFeriasFuncionario = function(funcionario) {
        funcionario.Ferias = [
        {
            MatriculaFuncionario: funcionario.Matricula,
            CodMesOrcamento: null,
            QtdaDias: null
        },{
            MatriculaFuncionario: funcionario.Matricula,
            CodMesOrcamento: null,
            QtdaDias: null
        },{
            MatriculaFuncionario: funcionario.Matricula,
            CodMesOrcamento: null,
            QtdaDias: null
        }];
        funcionariosFeriasAPI.getFuncionariosFerias(funcionario.Matricula, self.ciclo.Codigo)
        .then(function(dado) {
            dado.data.forEach(function(y, index) {
                funcionario.Ferias[index] = y;
            });
        });
    }

    var loadOutrosFuncionario = function(funcionario) {
        valoresAbertosBaseAPI.getValoresAbertosBase(null, funcionario.Matricula, self.ciclo.Codigo)
        .then(function(dado) {

            //Inicializando algumas propriedades
            var f = {
                Matricula: funcionario.Matricula,
                Nome: funcionario.Nome
            };

            //Carrega o cargo
            cargosAPI.getCargo(funcionario.CargoCod)
            .then(function(retorno){
                f.Cargo = retorno.data.NomeCargo;
            });

            //Cria um propriedade para cada evento no array Outros Insumo, inicializando com valor 0
            outrosInsumos.forEach(function(z) {
                f[z] = [];
                self.ciclo.Meses.forEach(function(y) {
                    f[z].push({
                        CodEvento: z,
                        CodMesOrcamento: y.Codigo,
                        MatriculaFuncionario: funcionario.Matricula,
                        Valor: 0,
                        Mes: y.Mes
                    });
                });
                //Seta meses visíveis e não visíveis
                setMesesVisivel(funcionario, f[z]);
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

            // self.outros.push(f);
            funcionario.Outros = f;
        });
    }

    var loadFerias = function(funcionarios) {
        funcionarios.forEach(function(x) {
            loadFeriasFuncionario(x);
        });
    }

    var loadOutros = function(funcionarios) {
        // self.outros = [];

        funcionarios.forEach(function(x) {
            x.Outros = [];
            loadOutrosFuncionario(x);
        });


    }


    self.mudaAba = function(nova) {
        $scope.aba = nova;
    }


    self.saveAll = function() {

        var allFerias = [];
        self.funcionarios.forEach(function(x) {
            if (x.Ferias) {
                x.Ferias.forEach(function(y) {
                    allFerias.push(y);
                });
            }
        });

        var allHE = [];
        var allHN = [];
        var allOutros = [];
        self.funcionarios.forEach(function(x) {
            allHE.concat(x.horasExtras);
            allHN.concat(x.adNoturnos);
            for (var prop in x.Outros) {
                if (Array.isArray(x.Outros[prop])) {
                    allOutros = allOutros.concat(x.Outros[prop].map(function(y) { return y; }));
                }
            }
            
        });


        exibeLoader();
        funcionariosAPI.saveAllFuncionarios(self.funcionarios)
        .then(function(dado) {
            return hesBaseAPI.saveAllFuncionariosHEs(allHE);
        }).then(function(dado) {
            return adNoturnosBaseAPI.saveAllFuncionariosHNs(allHN);
        }).then(function(dado) {
            return funcionariosFeriasAPI.saveAllFuncionariosFerias(self.ciclo.Codigo, allFerias);
        }).then(function(dado) {
            return valoresAbertosBaseAPI.postValorAbertoBaseSaveAll(allOutros);
        }).then(function(dado) {
            messagesService.exibeMensagemSucesso("Informações salvas com sucesso! Os cálculos estão sendo realizados em background.");
            ocultaLoader();
            return calculosEventosBaseAPI.postCalculaBasePorCR(self.cr.Codigo, self.ciclo.Codigo);
        }).then(function(dado) {
            $rootScope.$broadcast('calculoRealizado');
        });


    }


    
    self.salvarTransferencia = function(transf) {
        var copia = angular.copy(transf.Funcionario);
        delete transf.Funcionario;
        transf.CROrigem = self.cr.Codigo;
        transferenciasAPI.postTransferencia(transf)
        .then(function(dado) {
            messagesService.exibeMensagemSucesso("Solicitação de Transferência enviada com sucesso!");
            $('#modal-tranferir-cr').fadeOut();
            $('.modal-backdrop').fadeOut();
            $('body').removeClass('modal-open');
            $rootScope.$broadcast('transCREvent');
        }, function(error) {
            if (error.status) {
                if (error.status == 404)
                    self.erroTransf = "Código de CR não cadastrado!";
                else if (error.status == 409)
                    self.erroTransf = "Funcionário já transferido!";
            } 
            transf.Funcionario = copia;
        });
    }

    self.transferir = function(funcionario) {
        self.erroTransf = null;
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

    var listenerTransAprovada = $scope.$on('transAprovada', function($event, matricula) {

        if (!self.cr || !self.ciclo) return;
        loadFuncionarios(self.cr.Codigo, self.ciclo.Codigo);

    });

    //Adiciona um listener para capturar as mudanças de seleção de CR
    var listenerCR = $scope.$on('crChanged', function($event, cr) {
        if (cr) {
            self.cr = cr;
            if (self.ciclo) 
                loadFuncionarios(self.cr.Codigo, self.ciclo.Codigo);
            else
                loadFuncionarios(self.cr.Codigo);
        }
        else
            self.funcionarios = [];
    });


    //Remove o Listener
    $scope.$on('$destroy', function () {
        listenerCR();
        listenerTransAprovada();
    });

    self.ciclo = sharedDataService.getCicloAtual();


}]);
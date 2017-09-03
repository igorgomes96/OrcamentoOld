angular.module('orcamentoApp').controller('pessoalContratacoesCtrl', ['messagesService', 'contratacoesAPI', 'contratacoesMesAPI', '$scope', 'sharedDataService', 'cargosAPI', 'filiaisAPI', 'numberFilter', 'sharedDataService', 'adNoturnosContratacoesAPI', 'hesContratacoesAPI', 'valoresAbertosContratacoesAPI', 'calculosEventosContratacoesAPI', '$rootScope', function(messagesService, contratacoesAPI, contratacoesMesAPI, $scope, sharedDataService, cargosAPI, filiaisAPI, numberFilter, sharedDataService, adNoturnosContratacoesAPI, hesContratacoesAPI, valoresAbertosContratacoesAPI, calculosEventosContratacoesAPI, $rootScope) {
	var self = this;

	self.filiais = [];
	self.cargosCH = [];
	self.contratacoes = [];
    self.cargoNovo = null;
    self.cr = null;
    self.ciclo = null;
    self.outros = [];
    self.adNoturnos = [];
    self.horasExtras = [];
    $scope.aba = "Contratações";

    var outrosInsumos = sharedDataService.getOutrosInsumosFolha();

    var loadContratacoes = function(ciclo, cr) {
        contratacoesAPI.getContratacoes(ciclo, cr)
        .then(function(dado) {

            dado.data.forEach(function(x) {

                cargosAPI.getCargo(x.CargoCod)
                .then(function(retorno) {
                    x.Cargo = retorno.data;
                });

                x.Salario = numberFilter(x.Salario, 2);
                loadContratacoesMes(x);
            });

            self.contratacoes = dado.data;
            loadHorasExtras();
            loadAdicionaisNoturnos();
            loadOutros();

        });

    }

    var loadContratacoesMes = function(contratacao) {
        contratacoesMesAPI.getContratacoesMes(contratacao.Codigo)
        .then(function(dado) {

            contratacao.ContratacaoMeses = getContratacoesMes(self.ciclo, contratacao.Codigo); //Seta Meses como contratacoes = 0

            //Atribui os valores do retorno da requisição ao array
            dado.data.forEach(function(y) {
                var mes = contratacao.ContratacaoMeses.filter(function(z) {
                    return z.MesOrcamentoCod == y.MesOrcamentoCod;
                });
                contratacao.ContratacaoMeses[contratacao.ContratacaoMeses.indexOf(mes[0])] = y;
            });

            contratacao.ExcluirVisivel = true;  //Exibe o botão de excluir
            
        }, function(error) { 
            console.log(error); 
        });

    }

    var loadHorasExtras = function() {
        self.horasExtras = [];
        self.horasExtrasAbertas = [];

        self.contratacoes.forEach(function(x) {
            hesContratacoesAPI.getContratacaoHEs(x.Codigo, self.ciclo.Codigo)
            .then(function(dado) {
                self.horasExtras.push(dado.data);
            });
        });

    }

    var loadAdicionaisNoturnos = function() {
        self.adNoturnos = [];
        self.horasNoturnasAbertas = [];

        self.contratacoes.forEach(function(x) {
            adNoturnosContratacoesAPI.getContratacaoHNs(x.Codigo, self.ciclo.Codigo)
            .then(function(dado) {
                self.adNoturnos.push(dado.data);
            });
        });

    }

    var loadOutros = function() {
        self.outros = [];

        self.contratacoes.forEach(function(x) {
            valoresAbertosContratacoesAPI.getValoresAbertosContratacoes(null, x.Codigo, self.ciclo.Codigo)
            .then(function(dado) {

                
                var f = {
                    Codigo: x.Codigo,
                    Cargo: x.Cargo,
                    CargaHoraria: x.CargaHoraria
                };

                
                outrosInsumos.forEach(function(z) {
                    f[z] = [];
                    self.ciclo.Meses.forEach(function(y) {
                        f[z].push({
                            CodEvento: z,
                            CodMesOrcamento: y.Codigo,
                            CodContratacao: x.Codigo,
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


    var resetCargoNovo = function() {
        self.cargoNovo = null;
        self.cargoNovo = {
            Insalubridade: 0,
            Motivo: null,
            Periculosidade: false,
            CentroCustoCod: self.cr.Codigo,
            EmpresaCod: self.cr.EmpresaCod,
            CidadeNome: undefined,
            CargoCod: undefined,
            CicloCod: self.ciclo.Codigo,
            ConvenioPlanoMed: null,
            ContratacaoMeses: getContratacoesMes(self.ciclo, null),
            Salario: null
        }

    }

	self.adicionaContratacao = function(keyEvent) {

		if (keyEvent.which === 13) {
			self.saveContratacao(self.cargoNovo);
        }
	}

	var loadCidades = function(codEmpresa) {
        filiaisAPI.getFiliais(codEmpresa)
        .then(function(dado) {
            self.filiais = dado.data;
        });
    }

    var loadCargos = function(codEmpresa) {
        cargosAPI.getCHsPorCargo(codEmpresa)
        .then(function(dado) {
            self.cargosCH = dado.data;
            self.cargosCH.forEach(function(x) {
                x.FaixasSalarios = [1000, 2000, 1500, 2500];
            });
        });
    }

    var getContratacoesMes = function(ciclo, codContratacao) {
        var retorno = [];
        for (var i = 0; i < ciclo.Meses.length; i++) {
            retorno.push({
                ContratacaoCod: codContratacao,
                MesOrcamentoCod: ciclo.Meses[i].Codigo,
                Outros: 0,
                Qtda: 1,
                RemuneracaoProdutividade: 0
            });
        }
        return retorno;
    }

    self.saveContratacao = function(contratacao) {
        if (contratacao.CodEscala === undefined || contratacao.CodEscala === 0)
            contratacao.CodEscala = 1;

        contratacao.CargoCod = contratacao.Cargo.CargoCod; 
        var contratacaoMeses = contratacao.ContratacaoMeses;
        delete contratacao.ContratacaoMeses; //Se deixar, acontece erro ao salvar
        contratacao.Salario = numberFilter(contratacao.Salario, 2);
        contratacoesAPI.postContratacao(contratacao)
        .then(function(dado) {

            contratacao.Codigo = dado.data.Codigo;

            contratacaoMeses.forEach(function(x) {
                x.ContratacaoCod = dado.data.Codigo;
                //contratacoesMesAPI.postContratacaoMes(x);
            });

            contratacoesMesAPI.postContratacaoMesSaveAll(contratacaoMeses)
            .then(function(){}, function (error){
                console.log(error);
                messagesService.exibeMensagemErro(error.status, 'Erro ao salvar contrações (Meses)!');
            });

            loadContratacoes(self.ciclo.Codigo, self.cr.Codigo);
            contratacao.Codigo = dado.data.Codigo;
            self.contratacoes.push(contratacao);

            messagesService.exibeMensagemSucesso("Contratação salva com sucesso!");
            resetCargoNovo();
        });
    }

    self.saveAll = function() {

        var horasExtras = [];
        self.horasExtras.forEach(function(x) {
            for (var prop in x) {
                if (Array.isArray(x[prop])) {
                    x[prop].forEach(function(y) {
                        y.ContratacaoCod = x.Codigo;
                    });
                    horasExtras = horasExtras.concat(x[prop]);
                }
            }
        });

        var adNoturnos = [];
        self.adNoturnos.forEach(function(x) {
            for (var prop in x) {
                if (Array.isArray(x[prop])) {
                    x[prop].forEach(function(y) {
                        y.CodContratacao = x.Codigo;
                    });
                    adNoturnos = adNoturnos.concat(x[prop]);
                }
            }
        });

        var outros = [];
        self.outros.forEach(function(x) {
            for (var prop in x) {
                if (Array.isArray(x[prop])) {
                    outros = outros.concat(x[prop]);
                }
            }
        });
        

        hesContratacoesAPI.postHEContratacaoSaveAll(horasExtras)
        .then(function(dado) {
            return adNoturnosContratacoesAPI.postAdNoturnoContratacaoSaveAll(adNoturnos);
        }).then(function(dado) {
            return valoresAbertosContratacoesAPI.postValorAbertoContratacaoSaveAll(outros);
        }).then(function(dado) {
            messagesService.exibeMensagemSucesso("Salvo com sucesso!");
            return calculosEventosContratacoesAPI.postCalculaContratacaoPorCicloPorCR(self.cr.Codigo, self.ciclo.Codigo);
        }).then(function(dado) {
            $rootScope.$broadcast('calculoRealizado');
        }, function(error) {
            console.log(error);
        });  
    }

    self.deleteContratacao = function(contratacaoCod) {
    	contratacoesAPI.deleteContratacao(contratacaoCod);
        var c = self.contratacoes.filter(function(x) {
            return x.Codigo == contratacaoCod;
        })[0];
        self.contratacoes.splice(self.contratacoes.indexOf(c), 1);
        messagesService.exibeMensagemSucesso("Contratação excluída com sucesso!");
    }


    //Adiciona um listener para capturar as mudanças de seleção de CR
    var listener = $scope.$on('crChanged', function($event, cr) {
        if (self.ciclo && cr) {
        	self.cr = cr;
            self.contratacoes = [];
            loadContratacoes(self.ciclo.Codigo, cr.Codigo);
            loadCidades(cr.EmpresaCod);
            loadCargos(cr.EmpresaCod);
            resetCargoNovo();
        } else
            self.contratacoes = [];
    });

    //Remove o Listener
    $scope.$on('$destroy', function () {
        listener();
    });


	self.ciclo = sharedDataService.getCicloAtual();
}]);
angular.module('orcamentoApp').controller('pessoalContratacoesCtrl', ['messagesService', 'contratacoesAPI', 'contratacoesMesAPI', '$scope', 'sharedDataService', 'cargosAPI', 'filiaisAPI', 'numberFilter', 'sharedDataService', function(messagesService, contratacoesAPI, contratacoesMesAPI, $scope, sharedDataService, cargosAPI, filiaisAPI, numberFilter, sharedDataService) {
	var self = this;

	self.filiais = [];
	self.cargosCH = [];
	self.contratacoes = [];
    self.cargoNovo = null;
    self.cr = null;
    self.ciclo = null;

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

    var loadContratacoes = function(ciclo, cr) {
        contratacoesAPI.getContratacoes(ciclo, cr)
        .then(function(dado) {

            dado.data.forEach(function(x) {
                x.Salario = numberFilter(x.Salario, 2);
                loadContratacoesMes(x);
            });

            self.contratacoes = dado.data;

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

            return cargosAPI.getCargo(contratacao.CargoCod);
            
        }).then(function(dado3) {

            contratacao.CargoNome = dado3.data.NomeCargo;
            contratacao.ExcluirVisivel = true;  //Exibe o botão de excluir
            
        }, function(error) { 
            console.log(error); 
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
        contratacao.CargoCod = contratacao.Cargo.CargoCod; 
        var contratacaoMeses = contratacao.ContratacaoMeses;
        contratacao.Salario = numberFilter(contratacao.Salario, 2);
        delete contratacao.ContratacaoMeses;
        contratacoesAPI.postContratacao(contratacao)
        .then(function(dado) {

            var cont = 0, total = contratacaoMeses.length;
            contratacaoMeses.forEach(function(x) {
                x.ContratacaoCod = dado.data.Codigo;
                contratacoesMesAPI.postContratacaoMes(x);
            });

            loadContratacoesMes(contratacao);
            contratacao.Codigo = dado.data.Codigo;
            self.contratacoes.push(contratacao);

            messagesService.exibeMensagemSucesso("Contratação salva com sucesso!");
            resetCargoNovo();
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
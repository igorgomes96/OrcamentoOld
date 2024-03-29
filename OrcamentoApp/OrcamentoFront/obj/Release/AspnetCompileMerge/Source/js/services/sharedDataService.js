angular.module('orcamentoApp').service('sharedDataService', ['centrosCustosAPI', 'localStorageService', 'ciclosAPI', function(centrosCustosAPI, localStorageService, ciclosAPI) {

    var self = this;
    
    var ultimoCR = null;
    var usuario = null;
    var cicloAtual = null;
    var outrosInsumosFolha = ['GRATIF','PRODUT','COMISSAO','PRO-LAB','PGP-PGI','AJU-CUST'];
    var insumosAbertosFolha = ['GRATIF','PRODUT','COMISSAO','PRO-LAB','PGP-PGI','AJU-CUST', 'AD-ABERTO', 'HE-ABERTO'];
    self.mensagensAutomaticas = true;  // Se true, exibe mensagens de erro automaticamente ao receber resposta de erro do servidor (errorInterceptor)


    self.setUsuario = function(user) {
        usuario = user;
        centrosCustosAPI.getCentrosCustos(user.SetorCod)
        .then(function(dado) {
            usuario.CRs = dado.data;
            //localStorageService.saveUser(usuario);
        }, function(error) {
            console.log(error);
        });
    }

    self.getUsuario = function() {
        if (!usuario) {
            usuario = localStorageService.getUser();
            self.setUsuario(usuario);
        }
        return usuario;
    }

    self.setCicloAtual = function(ciclo) {
        cicloAtual = ciclo;
    }


    self.getCicloAtual = function() {
        return cicloAtual;
    }

    self.getOutrosInsumosFolha = function() {
        return outrosInsumosFolha;
    }

    self.getInsumosAbertosFolha = function() {
        return insumosAbertosFolha;
    }

    self.setUltimoCR = function(cr) {
        ultimoCR = cr;
    }

    self.getUltimoCR = function() {
        return ultimoCR;
    }


}]);
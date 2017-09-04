angular.module('orcamentoApp').service('sharedDataWithoutInjectionService', [function() {

    var self = this;

    self.mensagensAutomaticas = true;  // Se true, exibe mensagens de erro automaticamente ao receber resposta de erro do servidor (errorInterceptor)
    

}]);
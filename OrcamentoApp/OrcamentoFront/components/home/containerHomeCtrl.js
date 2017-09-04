angular.module('orcamentoApp').controller('containerHomeCtrl', ['config', function (config) {
    
    var self = this;
    self.nomeSistema = config.nomeSistema;
}]);
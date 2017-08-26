angular.module('orcamentoApp').controller('orcamentoFiltrosCtrl', ['sharedDataService', '$scope', '$rootScope', function(sharedDataService, $scope, $rootScope) {
	$scope.crAtual = null;

    $scope.userContainer = sharedDataService.getUsuario();

   	$scope.$watch('crAtual', function() {
        $rootScope.$broadcast('crChanged', $scope.crAtual);
    });
}]);
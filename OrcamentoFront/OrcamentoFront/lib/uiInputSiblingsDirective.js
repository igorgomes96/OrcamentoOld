var app = angular.module('uiInputSiblings',[]);
app.directive('uiInputParent', function() {
    return {
        scope: {},  //Cria um Isolated Scope
        controller: function() {

            var filhos=[];
            this.addFilho = function(f) {
                filhos.push(f);
            }
            this.replicaValorFrente = function(ctrlFilho, valor) {
                var aux = false;
                filhos.forEach(function(i) {
                    if (aux) {
                        i.$setViewValue(valor);
                        i.$render();
                    }
                    if (i == ctrlFilho) {
                        aux = true;
                    }
                });

            }
            this.replicaValorTras = function(ctrlFilho, valor) {
                filhos.some(function(i) {
                    if (i == ctrlFilho) {
                        return true;
                    }
                    i.$setViewValue(valor);
                    i.$render();
                    return false;
                });
            }
        }
    }
}).directive('uiInputChild', function(){
    return {

        require: ['^^uiInputParent', 'ngModel'],  //^^pai = ctrl[0], ngModel = ctrl[1]
        restrict: 'A',      //Restringe a utilização da diretiva somente como Atributo
        scope: {},          //Cria um Isolated Scope
        link: function (scope, elem, attrs, ctrl) {
            
            //Adiciona o ngModel ao Array Filhos em Pai (Só assim o pai conhece os filhos) - Registra o filho
            ctrl[0].addFilho(ctrl[1]);

            //Atribue uma ação quando uma tecla é pressionada.
            elem.bind('keydown', function($event) {

                // '>'
                if ($event.keyCode == 190 && $event.shiftKey)  {
                    ctrl[0].replicaValorFrente(ctrl[1], ctrl[1].$viewValue);
                    $event.preventDefault();    //Interrompe o evento (não atribui o caractere '>' ou '<' à caixa de texto)
                }

                // '<'
                if ($event.keyCode == 188 && $event.shiftKey) {
                    ctrl[0].replicaValorTras(ctrl[1], ctrl[1].$viewValue);
                    $event.preventDefault();    //Interrompe o evento (não atribui o caractere '>' ou '<' à caixa de texto)
                }

            });
        }
    }
});
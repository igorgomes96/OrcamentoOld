var app = angular.module('uiPanelCollapsible',[]);
app.directive("uiPanel", function() {
	return {
		template:
		'<div class="panel panel-default panel-content">' +
    		'<div class="panel-heading">' +
    			'<div class="row">'+
    				'<a href data-toggle="collapse" data-target="#{{nome}}" class="collapsed">' +
                    	'<div class="col-md-12">' +
                        	'<h4 class="panel-title"> {{titulo}}</h4>' +
                    	'</div>' +
                	'</a>' +
        		'</div>' +
        	'</div>' +
    		'<div id="{{nome}}" class="panel-collapse collapse">' +
    			'<div class="panel-body" ng-transclude></div>' +
    		'</div>' +
		'</div>',
		replace: true,
		restrict: "AE",
		scope: {
			nome: "@",
			titulo: "@"
		},
		transclude: true
	};
});
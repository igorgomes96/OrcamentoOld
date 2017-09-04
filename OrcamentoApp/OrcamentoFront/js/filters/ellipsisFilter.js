angular.module('orcamentoApp').filter('ellipsis', [function() {
	return function(text, leftSize=10, rightSize=0) {
		if (!text || text.length < leftSize + rightSize + 4) return text;
		var left = text.substring(0, leftSize);
		var right = text.substring(text.length - rightSize, text.length);
		console.log('Passou');
		return left + '...' + right;
	}
}]);
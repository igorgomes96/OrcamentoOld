angular.module('orcamentoApp').service('usuariosAPI', ['config', '$http', function(config, $http) {
	
	this.getUsuarios = function() {
		return $http.get(config.baseUrl + "Usuarios");
	}

	this.getUsuario = function(id) {
		return $http.get(config.baseUrl + "Usuarios/" + id);
	}

	this.putUsuario = function(id, usuario) {
		return $http.put(config.baseUrl + "Usuarios/" + id, usuario);
	}

    this.postRedefinirSenha = function (usuario) {
        return $http.post(config.baseUrl + "Usuarios/RedefinirSenha", usuario);
    }

	this.postUsuario = function(usuario) {
		return $http.post(config.baseUrl + "Usuarios", usuario);
	}

	this.deleteUsuario = function(id) {
		return $http.delete(config.baseUrl + "Usuarios/" + id);
	}

	this.alteraSenha = function(id, usuario) {
		return $http.put(config.baseUrl + "Usuarios/AlteraSenha/" + id, usuario);
	}

}]);
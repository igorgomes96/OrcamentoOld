angular.module('orcamentoApp').controller('gestaoUsuariosCtrl', ['usuariosAPI', 'messagesService', 'setoresAPI', 'perfisAcessoAPI', function(usuariosAPI, messagesService, setoresAPI, perfisAcessoAPI) {

	var self = this;
	self.novo = false;
	self.usuario = null;
	self.usuarios = [];
	self.editando = false;
	self.perfis = [];
	self.setores = [];

	var loadUsuarios = function() {
		usuariosAPI.getUsuarios()
		.then(function(dado) {
			self.usuarios = dado.data;
		});
	}

	var loadPerfis = function() {
		perfisAcessoAPI.getPerfisAcesso()
		.then(function(dado) {
			self.perfis = dado.data;
		});
	}

	var loadSetores = function() {
		setoresAPI.getSetores()
		.then(function(dado) {
			self.setores = dado.data;
		});
	}

	self.deleteUsuario = function(login) {
		usuariosAPI.deleteUsuario(login)
		.then(function(dado) {
			loadUsuarios();
			self.usuario = null;	
			messagesService.exibeMensagemSucesso("Usuário excluído com sucesso!");
		});
	}

	self.novoUsuario = function() {
		self.usuario = null;
		self.novo = true;
		self.editando = true;
	}

	self.selecionar = function(usuario) {
		if (!self.editando) {
			self.usuario = usuario;
			self.buscaPerfil(usuario.Perfil);
		}
	}

	self.buscaPerfil = function(perfil) {
		perfisAcessoAPI.getPerfilAcesso(perfil)
		.then(function(dado) {
			self.usuario.PerfilObj = dado.data;
		});
	}

	self.editar = function() {
		self.novo = false;
		self.editando = true;
	}

	self.cancelarEdicao = function() {
		loadUsuarios();
		self.editando = false;
		self.novo = false;
		self.usuario = null;
	}

	self.saveUsuario = function(usuario) {
		var func = null;
		if (self.novo) {
			usuario.Senha = 'algar123';
			func = usuariosAPI.postUsuario(usuario);
		}
		else
			func = usuariosAPI.putUsuario(usuario.Login, usuario);

		func.then(function(dado) {
			loadUsuarios();
			self.cancelarEdicao();
			messagesService.exibeMensagemSucesso("Usuário salvo com sucesso!");
		});

	}

	loadUsuarios();
	loadSetores();
	loadPerfis();

}]); 
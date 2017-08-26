using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class UsuarioDTO
    {
        public UsuarioDTO (Usuario u)
        {
            if (u == null) return;
            Login = u.Login;
            Email = u.Email;
            Nome = u.Nome;
            Perfil = u.Perfil;
            Ramal = u.Ramal;
            Senha = u.Senha;
            SetorCod = u.SetorCod;
        }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Perfil { get; set; }
        public string Ramal { get; set; }
        public string Senha { get; set; }
        public Nullable<int> SetorCod { get; set; }
    }
}

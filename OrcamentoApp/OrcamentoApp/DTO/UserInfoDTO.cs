using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class UserInfoDTO
    {
        public UserInfoDTO(Usuario u)
        {
            if (u == null) return;
            Permissoes = new HashSet<string>();
            Login = u.Login;
            Email = u.Email;
            Nome = u.Nome;
            Ramal = u.Ramal;
            SetorCod = u.SetorCod;
            Perfil = u.Perfil;
        }

        public string Token { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Ramal { get; set; }
        public Nullable<int> SetorCod { get; set; }
        public string Perfil { get; set; }
        public IEnumerable<string> Permissoes { get; set; }
    }
}
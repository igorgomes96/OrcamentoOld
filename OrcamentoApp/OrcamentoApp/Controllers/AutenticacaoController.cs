using OrcamentoApp.DTO;
using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace OrcamentoApp.Controllers
{
    public class AutenticacaoController : ApiController
    {
        [HttpPost]
        [Route("api/Autentica")]
        [AllowAnonymous]
        public IHttpActionResult Login(Usuario usuario)
        {
            if (usuario == null || usuario.Login == null || usuario.Senha == null) return BadRequest();

            Contexto db = new Contexto();
            Usuario user = db.Usuario.Find(usuario.Login);

            if (user == null) return BadRequest("Usuário não cadastrado!");
            //if (user.Senha != Convert.ToBase64String(Encoding.UTF8.GetBytes(usuario.Senha))) return BadRequest("Senha incorreta!");
            if (user.Senha != usuario.Senha) return BadRequest("Senha incorreta!");

            string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(usuario.Login + ":" + usuario.Senha));

            //Verifica se já existe a sessão. Se sim, aumenta a data de expiração, caso contrário, cria uma nova sessão
            Sessao s = db.Sessao.Find(token);
            if (s == null)
                db.Sessao.Add(new Sessao { Chave = token, LoginUsuario = user.Login, Inicio = DateTime.Now, Fim = DateTime.Now.AddHours(6.0) });
            else
                s.Fim = DateTime.Now.AddHours(6.0);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            /*var retorno = new {
                Token = token,
                Usuario = user.Nome,
                Login = user.Login,
                Permissoes = user.Permissoes.Select(x => x.PermissaoNome)
            };*/

            UserInfoDTO u = new UserInfoDTO(user)
            {
                Token = token,
                Permissoes = user.Permissoes.Select(x => x.PermissaoNome)
            };
            db.Dispose();

            return Ok(u);
        }
    }
}

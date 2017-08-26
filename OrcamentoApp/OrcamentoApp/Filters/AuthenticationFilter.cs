using OrcamentoApp.Exceptions;
using OrcamentoApp.Filters;
using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace OrcamentoApp.Filters
{
    public class AuthenticationFilter : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return true;
            }
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // 1. Look for credentials in the request.
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            // 2. If there are no credentials, do nothing.
            if (authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Requisição sem credenciais!", request);
                return;
            }

            // 3. If there are credentials but the filter does not recognize the authentication scheme, do nothing.
            if (authorization.Scheme != "Basic")
            {
                context.ErrorResult = new AuthenticationFailureResult("Tipo de autenticação não reconhecida!", request);
                return;
            }

            // 4. If there are credentials that the filter understands, try to validate them.
            // 5. If the credentials are bad, set the error result.
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Credenciais não encontradas", request);
                return;
            }

            string Token = authorization.Parameter;

            IPrincipal principal = await AuthenticateAsync(Token);
            if (principal == null)
                context.ErrorResult = new AuthenticationFailureResult("Usuário não autenticado ou sessão expirada!", request);

            // 6. If the credentials are valid, set principal.
            else
            {
                context.Principal = principal;
                Thread.CurrentPrincipal = principal;
            }

            return;
        }


        private async Task<IPrincipal> AuthenticateAsync(string Token)
        {

            Contexto db = new Contexto();

            //Verifica a Sessão
            Sessao s = await db.Sessao.FindAsync(Token);

            //Se não houver sessão aberta, retorna
            if (s == null) return null;

            //Se a sessão tiver expirado, exclui do banco e retorna
            if (s.Fim < DateTime.Now)
            {
                db.Sessao.Remove(s);
                db.SaveChanges();
                return null;
            }

            s.Fim = DateTime.Now.AddHours(6.0);    //Atualiza prazo para expirar
            IIdentity identity = new CustomIdentity(s.Usuario, "Basic");
            IPrincipal p = new GenericPrincipal(identity, s.Usuario.Permissoes.Select(x => x.PermissaoNome).ToArray());

            db.SaveChanges();
            return p;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
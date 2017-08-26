using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OrcamentoApp.Exceptions
{
    internal class AuthenticationFailureResult : IHttpActionResult
    {
        private HttpRequestMessage request;
        private string reason;

        public AuthenticationFailureResult(string reason, HttpRequestMessage request)
        {
            this.reason = reason;
            this.request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = request;
            response.ReasonPhrase = reason;
            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrcamentoApp.Controllers
{
    public class TesteController : ApiController
    {
        public IEnumerable<string> GetTags(string query = null)
        {
            if (query != null) query = query.ToLower();
            IEnumerable<string> Tags = new List<string>
            {
                "Periculosidade", "Insalubridade", "Convênio Médico", "Participação nos Lucros", "Remuneração Variável",
                "Salário", "Aviso Prévio", "Seguro Acidente de Trabalho", "PAT", "Vale Transporte", "Férias", "13º Salário"
            };
            return Tags.Where(x => query == null || x.ToLower().Contains(query)).OrderBy(x => x);
        }

        [HttpGet]
        [Route("api/Teste/SendError/{id}")]
        public IHttpActionResult GetSendError(int id)
        {
            if (id == 409) return Conflict();
            if (id == 404) return NotFound();
            if (id == 400) return BadRequest("Requisição mal formatada!");
            return InternalServerError(new Exception("Erro Interno!"));
        }

        [HttpGet]
        [Route("api/Teste/SendSuccess")]
        public IHttpActionResult GetSendSucess()
        {
            return Ok();
        }
    }
}

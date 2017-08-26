using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OrcamentoApp.Models;
using OrcamentoApp.DTO;

namespace OrcamentoApp.Controllers
{
    public class SolicitacoesTHController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/SolicitacoesTH
        public IEnumerable<SolicitacaoTHViewDTO> GetSolicitacaoTH(string login = null)
        {
            return db.SolicitacaoTH.ToList().Where(x => login == null || x.LoginSolicitante == login).Select(x => new SolicitacaoTHViewDTO(x));
        }

        [HttpGet]
        [Route("api/SolicitacoesTH/PorSetor/{codSetor}")]
        public IEnumerable<SolicitacaoTHViewDTO> GetSolicitacoesTHPorSetor(int codSetor)
        {
            IEnumerable<SolicitacaoTH> contratacoes = db.SolicitacaoContratacao.ToList()
                .Where(x => x.CentroCusto.SetorCod == codSetor).Select(x => x.SolicitacaoTH);

            IEnumerable<SolicitacaoTH> demissoes = db.SolicitacaoDesligamento.ToList()
                .Where(x => x.Funcionario.CentroCusto.SetorCod == codSetor).Select(x => x.SolicitacaoTH);

            IEnumerable<SolicitacaoTH> alteracoesCargos = db.SolicitacaoAlteracaoCargo.ToList()
                .Where(x => x.Funcionario.CentroCusto.SetorCod == codSetor).Select(x => x.SolicitacaoTH);

            IEnumerable<SolicitacaoTH> alteracoesSalarios = db.SolicitacaoAlteracaoSalario.ToList()
                .Where(x => x.Funcionario.CentroCusto.SetorCod == codSetor).Select(x => x.SolicitacaoTH);

            return contratacoes.Union(demissoes).Union(alteracoesCargos).Union(alteracoesSalarios)
                .Select(x => new SolicitacaoTHViewDTO(x));
        }

        // GET: api/SolicitacoesTH/5
        [ResponseType(typeof(SolicitacaoTHDTO))]
        public IHttpActionResult GetSolicitacaoTH(int id)
        {
            SolicitacaoTH solicitacaoTH = db.SolicitacaoTH.Find(id);
            if (solicitacaoTH == null)
            {
                return NotFound();
            }

            return Ok(new SolicitacaoTHDTO(solicitacaoTH));
        }

        // PUT: api/SolicitacoesTH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSolicitacaoTH(int id, SolicitacaoTH solicitacaoTH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solicitacaoTH.Codigo)
            {
                return BadRequest();
            }

            db.Entry(solicitacaoTH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitacaoTHExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SolicitacoesTH
        [ResponseType(typeof(SolicitacaoTHDTO))]
        public IHttpActionResult PostSolicitacaoTH(SolicitacaoTH solicitacaoTH)
        {
            if (!ModelState.IsValid)
            {   
                return BadRequest(ModelState);
            }

            db.SolicitacaoTH.Add(solicitacaoTH);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return CreatedAtRoute("DefaultApi", new { id = solicitacaoTH.Codigo }, new SolicitacaoTHDTO(solicitacaoTH));
        }

        // DELETE: api/SolicitacoesTH/5
        [ResponseType(typeof(SolicitacaoTHDTO))]
        public IHttpActionResult DeleteSolicitacaoTH(int id)
        {
            SolicitacaoTH solicitacaoTH = db.SolicitacaoTH.Find(id);
            if (solicitacaoTH == null)
            {
                return NotFound();
            }

            SolicitacaoTHDTO s = new SolicitacaoTHDTO(solicitacaoTH);
            db.SolicitacaoTH.Remove(solicitacaoTH);
            db.SaveChanges();

            return Ok(s);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SolicitacaoTHExists(int id)
        {
            return db.SolicitacaoTH.Count(e => e.Codigo == id) > 0;
        }
    }
}
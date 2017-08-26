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
    public class SolicitacoesDesligamentosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/SolicitacoesDesligamentos
        public IEnumerable<SolicitacaoDesligamentoDTO> GetSolicitacaoDesligamento()
        {
            return db.SolicitacaoDesligamento.ToList().Select(x => new SolicitacaoDesligamentoDTO(x));
        }

        // GET: api/SolicitacoesDesligamentos/5
        [ResponseType(typeof(SolicitacaoDesligamentoDTO))]
        public IHttpActionResult GetSolicitacaoDesligamento(int id)
        {
            SolicitacaoDesligamento solicitacaoDesligamento = db.SolicitacaoDesligamento.Find(id);
            if (solicitacaoDesligamento == null)
            {
                return NotFound();
            }

            return Ok(new SolicitacaoDesligamentoDTO(solicitacaoDesligamento));
        }

        // PUT: api/SolicitacoesDesligamentos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSolicitacaoDesligamento(int id, SolicitacaoDesligamento solicitacaoDesligamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solicitacaoDesligamento.SolicitacaoCod)
            {
                return BadRequest();
            }

            db.Entry(solicitacaoDesligamento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitacaoDesligamentoExists(id))
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

        // POST: api/SolicitacoesDesligamentos
        [ResponseType(typeof(SolicitacaoDesligamentoDTO))]
        public IHttpActionResult PostSolicitacaoDesligamento(SolicitacaoDesligamento solicitacaoDesligamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SolicitacaoDesligamento.Add(solicitacaoDesligamento);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SolicitacaoDesligamentoExists(solicitacaoDesligamento.SolicitacaoCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = solicitacaoDesligamento.SolicitacaoCod }, new SolicitacaoDesligamentoDTO(solicitacaoDesligamento));
        }

        // DELETE: api/SolicitacoesDesligamentos/5
        [ResponseType(typeof(SolicitacaoDesligamentoDTO))]
        public IHttpActionResult DeleteSolicitacaoDesligamento(int id)
        {
            SolicitacaoDesligamento solicitacaoDesligamento = db.SolicitacaoDesligamento.Find(id);
            if (solicitacaoDesligamento == null)
            {
                return NotFound();
            }

            SolicitacaoDesligamentoDTO s = new SolicitacaoDesligamentoDTO(solicitacaoDesligamento);
            db.SolicitacaoDesligamento.Remove(solicitacaoDesligamento);
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

        private bool SolicitacaoDesligamentoExists(int id)
        {
            return db.SolicitacaoDesligamento.Count(e => e.SolicitacaoCod == id) > 0;
        }
    }
}
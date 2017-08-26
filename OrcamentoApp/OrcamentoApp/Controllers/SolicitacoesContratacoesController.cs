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
    public class SolicitacoesContratacoesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/SolicitacoesContratacoes
        public IEnumerable<SolicitacaoContratacaoDTO> GetSolicitacaoContratacao()
        {
            return db.SolicitacaoContratacao.Select(x => new SolicitacaoContratacaoDTO(x));   
        }

        // GET: api/SolicitacoesContratacoes/5
        [ResponseType(typeof(SolicitacaoContratacaoDTO))]
        public IHttpActionResult GetSolicitacaoContratacao(int id)
        {
            SolicitacaoContratacao solicitacaoContratacao = db.SolicitacaoContratacao.Find(id);
            if (solicitacaoContratacao == null)
            {
                return NotFound();
            }

            return Ok(new SolicitacaoContratacaoDTO(solicitacaoContratacao));
        }

        // PUT: api/SolicitacoesContratacoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSolicitacaoContratacao(int id, SolicitacaoContratacao solicitacaoContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solicitacaoContratacao.SolicitacaoCod)
            {
                return BadRequest();
            }

            db.Entry(solicitacaoContratacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitacaoContratacaoExists(id))
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

        // POST: api/SolicitacoesContratacoes
        [ResponseType(typeof(SolicitacaoContratacaoDTO))]
        public IHttpActionResult PostSolicitacaoContratacao(SolicitacaoContratacao solicitacaoContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SolicitacaoContratacao.Add(solicitacaoContratacao);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if (SolicitacaoContratacaoExists(solicitacaoContratacao.SolicitacaoCod))
                {
                    return Conflict();
                }
                else
                {
                    return InternalServerError(e);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = solicitacaoContratacao.SolicitacaoCod }, new SolicitacaoContratacaoDTO(solicitacaoContratacao));
        }

        // DELETE: api/SolicitacoesContratacoes/5
        [ResponseType(typeof(SolicitacaoContratacaoDTO))]
        public IHttpActionResult DeleteSolicitacaoContratacao(int id)
        {
            SolicitacaoContratacao solicitacaoContratacao = db.SolicitacaoContratacao.Find(id);
            if (solicitacaoContratacao == null)
            {
                return NotFound();
            }

            SolicitacaoContratacaoDTO s = new SolicitacaoContratacaoDTO(solicitacaoContratacao);
            db.SolicitacaoContratacao.Remove(solicitacaoContratacao);
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

        private bool SolicitacaoContratacaoExists(int id)
        {
            return db.SolicitacaoContratacao.Count(e => e.SolicitacaoCod == id) > 0;
        }
    }
}
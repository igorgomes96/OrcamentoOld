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

namespace OrcamentoApp.Controllers
{
    public class HEsContratacoesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/HEContratacoes
        public IQueryable<HEContratacao> GetHEContratacao()
        {
            return db.HEContratacao;
        }

        // GET: api/HEContratacoes/5
        [ResponseType(typeof(HEContratacao))]
        public IHttpActionResult GetHEContratacao(int id)
        {
            HEContratacao hEContratacao = db.HEContratacao.Find(id);
            if (hEContratacao == null)
            {
                return NotFound();
            }

            return Ok(hEContratacao);
        }

        // PUT: api/HEContratacoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHEContratacao(int id, HEContratacao hEContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hEContratacao.ContratacaoCod)
            {
                return BadRequest();
            }

            db.Entry(hEContratacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HEContratacaoExists(id))
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

        // POST: api/HEContratacoes
        [ResponseType(typeof(HEContratacao))]
        public IHttpActionResult PostHEContratacao(HEContratacao hEContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HEContratacao.Add(hEContratacao);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HEContratacaoExists(hEContratacao.ContratacaoCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hEContratacao.ContratacaoCod }, hEContratacao);
        }

        // DELETE: api/HEContratacoes/5
        [ResponseType(typeof(HEContratacao))]
        public IHttpActionResult DeleteHEContratacao(int id)
        {
            HEContratacao hEContratacao = db.HEContratacao.Find(id);
            if (hEContratacao == null)
            {
                return NotFound();
            }

            db.HEContratacao.Remove(hEContratacao);
            db.SaveChanges();

            return Ok(hEContratacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HEContratacaoExists(int id)
        {
            return db.HEContratacao.Count(e => e.ContratacaoCod == id) > 0;
        }
    }
}
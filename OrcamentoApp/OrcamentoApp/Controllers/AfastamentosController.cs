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
    public class AfastamentosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Afastamentos
        public IQueryable<Afastamento> GetAfastamento()
        {
            return db.Afastamento;
        }

        // GET: api/Afastamentos/5
        [ResponseType(typeof(Afastamento))]
        public IHttpActionResult GetAfastamento(int id)
        {
            Afastamento afastamento = db.Afastamento.Find(id);
            if (afastamento == null)
            {
                return NotFound();
            }

            return Ok(afastamento);
        }

        // PUT: api/Afastamentos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAfastamento(int id, Afastamento afastamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != afastamento.Codigo)
            {
                return BadRequest();
            }

            db.Entry(afastamento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AfastamentoExists(id))
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

        // POST: api/Afastamentos
        [ResponseType(typeof(Afastamento))]
        public IHttpActionResult PostAfastamento(Afastamento afastamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Afastamento.Add(afastamento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = afastamento.Codigo }, afastamento);
        }

        // DELETE: api/Afastamentos/5
        [ResponseType(typeof(Afastamento))]
        public IHttpActionResult DeleteAfastamento(int id)
        {
            Afastamento afastamento = db.Afastamento.Find(id);
            if (afastamento == null)
            {
                return NotFound();
            }

            db.Afastamento.Remove(afastamento);
            db.SaveChanges();

            return Ok(afastamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AfastamentoExists(int id)
        {
            return db.Afastamento.Count(e => e.Codigo == id) > 0;
        }
    }
}
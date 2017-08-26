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
    public class DiasIndenizadosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/DiasIndenizados
        public IQueryable<DiasIndenizados> GetDiasIndenizados()
        {
            return db.DiasIndenizados;
        }

        // GET: api/DiasIndenizados/5
        [ResponseType(typeof(DiasIndenizados))]
        public IHttpActionResult GetDiasIndenizados(int id)
        {
            DiasIndenizados diasIndenizados = db.DiasIndenizados.Find(id);
            if (diasIndenizados == null)
            {
                return NotFound();
            }

            return Ok(diasIndenizados);
        }

        // PUT: api/DiasIndenizados/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiasIndenizados(int id, DiasIndenizados diasIndenizados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diasIndenizados.Anos)
            {
                return BadRequest();
            }

            db.Entry(diasIndenizados).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiasIndenizadosExists(id))
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

        // POST: api/DiasIndenizados
        [ResponseType(typeof(DiasIndenizados))]
        public IHttpActionResult PostDiasIndenizados(DiasIndenizados diasIndenizados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DiasIndenizados.Add(diasIndenizados);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DiasIndenizadosExists(diasIndenizados.Anos))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = diasIndenizados.Anos }, diasIndenizados);
        }

        // DELETE: api/DiasIndenizados/5
        [ResponseType(typeof(DiasIndenizados))]
        public IHttpActionResult DeleteDiasIndenizados(int id)
        {
            DiasIndenizados diasIndenizados = db.DiasIndenizados.Find(id);
            if (diasIndenizados == null)
            {
                return NotFound();
            }

            db.DiasIndenizados.Remove(diasIndenizados);
            db.SaveChanges();

            return Ok(diasIndenizados);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiasIndenizadosExists(int id)
        {
            return db.DiasIndenizados.Count(e => e.Anos == id) > 0;
        }
    }
}
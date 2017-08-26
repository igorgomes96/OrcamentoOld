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
    public class SalariosMinimosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/SalariosMinimos
        public IQueryable<SalarioMinimo> GetSalarioMinimo()
        {
            return db.SalarioMinimo;
        }

        // GET: api/SalariosMinimos/5
        [ResponseType(typeof(SalarioMinimo))]
        public IHttpActionResult GetSalarioMinimo(DateTime id)
        {
            SalarioMinimo salarioMinimo = db.SalarioMinimo.Find(id);
            if (salarioMinimo == null)
            {
                return NotFound();
            }

            return Ok(salarioMinimo);
        }

        // PUT: api/SalariosMinimos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalarioMinimo(DateTime id, SalarioMinimo salarioMinimo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salarioMinimo.Data)
            {
                return BadRequest();
            }

            db.Entry(salarioMinimo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalarioMinimoExists(id))
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

        // POST: api/SalariosMinimos
        [ResponseType(typeof(SalarioMinimo))]
        public IHttpActionResult PostSalarioMinimo(SalarioMinimo salarioMinimo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalarioMinimo.Add(salarioMinimo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalarioMinimoExists(salarioMinimo.Data))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salarioMinimo.Data }, salarioMinimo);
        }

        // DELETE: api/SalariosMinimos/5
        [ResponseType(typeof(SalarioMinimo))]
        public IHttpActionResult DeleteSalarioMinimo(DateTime id)
        {
            SalarioMinimo salarioMinimo = db.SalarioMinimo.Find(id);
            if (salarioMinimo == null)
            {
                return NotFound();
            }

            db.SalarioMinimo.Remove(salarioMinimo);
            db.SaveChanges();

            return Ok(salarioMinimo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalarioMinimoExists(DateTime id)
        {
            return db.SalarioMinimo.Count(e => e.Data == id) > 0;
        }
    }
}
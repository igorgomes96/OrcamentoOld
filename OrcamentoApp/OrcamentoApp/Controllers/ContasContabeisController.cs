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
    public class ContasContabeisController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/ContasContabeis
        public IQueryable<ContaContabil> GetContaContabil()
        {
            return db.ContaContabil;
        }

        // GET: api/ContasContabeis/5
        [ResponseType(typeof(ContaContabil))]
        public IHttpActionResult GetContaContabil(string id)
        {
            ContaContabil contaContabil = db.ContaContabil.Find(id);
            if (contaContabil == null)
            {
                return NotFound();
            }

            return Ok(contaContabil);
        }

        // PUT: api/ContasContabeis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContaContabil(string id, ContaContabil contaContabil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contaContabil.Codigo)
            {
                return BadRequest();
            }

            db.Entry(contaContabil).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContaContabilExists(id))
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

        // POST: api/ContasContabeis
        [ResponseType(typeof(ContaContabil))]
        public IHttpActionResult PostContaContabil(ContaContabil contaContabil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContaContabil.Add(contaContabil);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ContaContabilExists(contaContabil.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = contaContabil.Codigo }, contaContabil);
        }

        // DELETE: api/ContasContabeis/5
        [ResponseType(typeof(ContaContabil))]
        public IHttpActionResult DeleteContaContabil(string id)
        {
            ContaContabil contaContabil = db.ContaContabil.Find(id);
            if (contaContabil == null)
            {
                return NotFound();
            }

            db.ContaContabil.Remove(contaContabil);
            db.SaveChanges();

            return Ok(contaContabil);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContaContabilExists(string id)
        {
            return db.ContaContabil.Count(e => e.Codigo == id) > 0;
        }
    }
}
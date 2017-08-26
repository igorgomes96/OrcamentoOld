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
    public class STHsController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/STHs
        public IEnumerable<STHDTO> GetSTH()
        {
            return db.STH.ToList().Select(x => new STHDTO(x));
        }

        // GET: api/STHs/5
        [ResponseType(typeof(STHDTO))]
        public IHttpActionResult GetSTH(string id)
        {
            STH sTH = db.STH.Find(id);
            if (sTH == null)
            {
                return NotFound();
            }

            return Ok(new STHDTO(sTH));
        }

        // PUT: api/STHs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSTH(string id, STH sTH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sTH.CodigoSTH)
            {
                return BadRequest();
            }

            db.Entry(sTH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!STHExists(id))
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

        // POST: api/STHs
        [ResponseType(typeof(STHDTO))]
        public IHttpActionResult PostSTH(STH sTH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.STH.Add(sTH);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (STHExists(sTH.CodigoSTH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sTH.CodigoSTH }, new STHDTO(sTH));
        }

        // DELETE: api/STHs/5
        [ResponseType(typeof(STHDTO))]
        public IHttpActionResult DeleteSTH(string id)
        {
            STH sTH = db.STH.Find(id);
            if (sTH == null)
            {
                return NotFound();
            }

            STHDTO s = new STHDTO(sTH);
            db.STH.Remove(sTH);
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

        private bool STHExists(string id)
        {
            return db.STH.Count(e => e.CodigoSTH == id) > 0;
        }
    }
}
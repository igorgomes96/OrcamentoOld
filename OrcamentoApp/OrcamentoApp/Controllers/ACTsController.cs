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
    public class ACTsController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/ACTs
        public IEnumerable<ACTDTO> GetACT()
        {
            return db.ACT.ToList().Select(x => new ACTDTO(x));
        }

        // GET: api/ACTs/5
        [ResponseType(typeof(ACTDTO))]
        public IHttpActionResult GetACT(string id)
        {
            ACT aCT = db.ACT.Find(id);
            if (aCT == null)
            {
                return NotFound();
            }

            return Ok(new ACTDTO(aCT));
        }

        // PUT: api/ACTs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutACT(string id, ACT aCT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aCT.CodigoACT)
            {
                return BadRequest();
            }

            db.Entry(aCT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ACTExists(id))
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

        // POST: api/ACTs
        [ResponseType(typeof(ACTDTO))]
        public IHttpActionResult PostACT(ACT aCT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ACT.Add(aCT);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ACTExists(aCT.CodigoACT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aCT.CodigoACT }, new ACTDTO(aCT));
        }

        // DELETE: api/ACTs/5
        [ResponseType(typeof(ACTDTO))]
        public IHttpActionResult DeleteACT(string id)
        {
            ACT aCT = db.ACT.Find(id);
            if (aCT == null)
            {
                return NotFound();
            }

            ACTDTO a = new ACTDTO(aCT);
            db.ACT.Remove(aCT);
            db.SaveChanges();

            return Ok(a);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ACTExists(string id)
        {
            return db.ACT.Count(e => e.CodigoACT == id) > 0;
        }
    }
}
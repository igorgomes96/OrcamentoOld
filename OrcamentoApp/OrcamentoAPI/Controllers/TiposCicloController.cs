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
using OrcamentoAPI.Models;
using OrcamentoAPI.DTO;

namespace OrcamentoAPI.Controllers
{
    public class TiposCicloController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/TiposCiclo
        public IEnumerable<TipoCicloDTO> GetTipoCiclo()
        {
            return db.TipoCiclo.ToList().Select(x => new TipoCicloDTO(x));
        }

        // GET: api/TiposCiclo/5
        [ResponseType(typeof(TipoCicloDTO))]
        public IHttpActionResult GetTipoCiclo(int id)
        {
            TipoCiclo tipoCiclo = db.TipoCiclo.Find(id);
            if (tipoCiclo == null)
            {
                return NotFound();
            }

            return Ok(new TipoCicloDTO(tipoCiclo));
        }

        // PUT: api/TiposCiclo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoCiclo(int id, TipoCiclo tipoCiclo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoCiclo.Codigo)
            {
                return BadRequest();
            }

            db.Entry(tipoCiclo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCicloExists(id))
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

        // POST: api/TiposCiclo
        [ResponseType(typeof(TipoCicloDTO))]
        public IHttpActionResult PostTipoCiclo(TipoCiclo tipoCiclo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoCiclo.Add(tipoCiclo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoCiclo.Codigo },  new TipoCicloDTO(tipoCiclo));
        }

        // DELETE: api/TiposCiclo/5
        [ResponseType(typeof(TipoCicloDTO))]
        public IHttpActionResult DeleteTipoCiclo(int id)
        {
            TipoCiclo tipoCiclo = db.TipoCiclo.Find(id);
            if (tipoCiclo == null)
            {
                return NotFound();
            }
            TipoCicloDTO t = new TipoCicloDTO(tipoCiclo);
            db.TipoCiclo.Remove(tipoCiclo);
            db.SaveChanges();

            return Ok(t);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoCicloExists(int id)
        {
            return db.TipoCiclo.Count(e => e.Codigo == id) > 0;
        }
    }
}
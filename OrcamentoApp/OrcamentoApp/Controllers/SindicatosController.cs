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
    public class SindicatosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Sindicatos
        public IEnumerable<SindicatoDTO> GetSindicato()
        {
            return db.Sindicato.ToList().Select(x => new SindicatoDTO(x));
        }

        // GET: api/Sindicatos/5
        [ResponseType(typeof(SindicatoDTO))]
        public IHttpActionResult GetSindicato(int id)
        {
            Sindicato sindicato = db.Sindicato.Find(id);
            if (sindicato == null)
            {
                return NotFound();
            }

            return Ok(new SindicatoDTO(sindicato));
        }

        // PUT: api/Sindicatos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSindicato(int id, Sindicato sindicato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sindicato.Codigo)
            {
                return BadRequest();
            }

            db.Entry(sindicato).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SindicatoExists(id))
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

        // POST: api/Sindicatos
        [ResponseType(typeof(SindicatoDTO))]
        public IHttpActionResult PostSindicato(Sindicato sindicato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sindicato.Add(sindicato);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sindicato.Codigo }, new SindicatoDTO(sindicato));
        }

        // DELETE: api/Sindicatos/5
        [ResponseType(typeof(SindicatoDTO))]
        public IHttpActionResult DeleteSindicato(int id)
        {
            Sindicato sindicato = db.Sindicato.Find(id);
            if (sindicato == null)
            {
                return NotFound();
            }

            SindicatoDTO s = new SindicatoDTO(sindicato);
            db.Sindicato.Remove(sindicato);
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

        private bool SindicatoExists(int id)
        {
            return db.Sindicato.Count(e => e.Codigo == id) > 0;
        }
    }
}
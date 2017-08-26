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
    public class ConveniosMedicosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/ConveniosMedicos
        public IEnumerable<ConvenioMedDTO> GetConvenioMed()
        {
            return db.ConvenioMed.ToList().Select(x => new ConvenioMedDTO(x));
        }

        // GET: api/ConveniosMedicos/5
        [ResponseType(typeof(ConvenioMedDTO))]
        public IHttpActionResult GetConvenioMed(int id)
        {
            ConvenioMed convenioMed = db.ConvenioMed.Find(id);
            if (convenioMed == null)
            {
                return NotFound();
            }

            return Ok(new ConvenioMedDTO(convenioMed));
        }

        // PUT: api/ConveniosMedicos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConvenioMed(int id, ConvenioMed convenioMed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != convenioMed.Codigo)
            {
                return BadRequest();
            }

            db.Entry(convenioMed).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConvenioMedExists(id))
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

        // POST: api/ConveniosMedicos
        [ResponseType(typeof(ConvenioMedDTO))]
        public IHttpActionResult PostConvenioMed(ConvenioMed convenioMed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ConvenioMed.Add(convenioMed);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ConvenioMedExists(convenioMed.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = convenioMed.Plano }, new ConvenioMedDTO(convenioMed));
        }

        // DELETE: api/ConveniosMedicos/5
        [ResponseType(typeof(ConvenioMedDTO))]
        public IHttpActionResult DeleteConvenioMed(int id)
        {
            ConvenioMed convenioMed = db.ConvenioMed.Find(id);
            if (convenioMed == null)
            {
                return NotFound();
            }

            ConvenioMedDTO c = new ConvenioMedDTO(convenioMed);
            db.ConvenioMed.Remove(convenioMed);
            db.SaveChanges();

            return Ok(c);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConvenioMedExists(int id)
        {
            return db.ConvenioMed.Count(e => e.Codigo == id) > 0;
        }
    }
}
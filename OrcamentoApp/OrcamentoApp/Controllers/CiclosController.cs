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
    public class CiclosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Ciclos
        public IEnumerable<CicloDTO> GetCiclo(Nullable<int> statusCod = null)
        {
            return db.Ciclo.ToList().Where(x => statusCod == null || x.StatusCod == statusCod).Select(x => new CicloDTO(x));
        }

        // GET: api/Ciclos/5
        [ResponseType(typeof(CicloDTO))]
        public IHttpActionResult GetCiclo(int id)
        {
            Ciclo ciclo = db.Ciclo.Find(id);
            if (ciclo == null)
            {
                return NotFound();
            }

            return Ok(new CicloDTO(ciclo));
        }

        // PUT: api/Ciclos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCiclo(int id, Ciclo ciclo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ciclo.Codigo)
            {
                return BadRequest();
            }

            db.Entry(ciclo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CicloExists(id))
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

        // POST: api/Ciclos
        [ResponseType(typeof(CicloDTO))]
        public IHttpActionResult PostCiclo(Ciclo ciclo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ciclo.Add(ciclo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ciclo.Codigo }, new CicloDTO(ciclo));
        }

        // DELETE: api/Ciclos/5
        [ResponseType(typeof(Ciclo))]
        public IHttpActionResult DeleteCiclo(int id)
        {
            Ciclo ciclo = db.Ciclo.Find(id);
            if (ciclo == null)
            {
                return NotFound();
            }

            CicloDTO c = new CicloDTO(ciclo);
            db.Ciclo.Remove(ciclo);
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

        private bool CicloExists(int id)
        {
            return db.Ciclo.Count(e => e.Codigo == id) > 0;
        }
    }
}
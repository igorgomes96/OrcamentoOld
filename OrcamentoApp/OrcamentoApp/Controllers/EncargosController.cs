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
    public class EncargosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Encargos
        public IEnumerable<EncargosDTO> GetEncargos()
        {
            return db.Encargos.ToList().Select(x => new EncargosDTO(x));
        }

        // GET: api/Encargos/5
        [ResponseType(typeof(EncargosDTO))]
        public IHttpActionResult GetEncargos(int id)
        {
            Encargos encargos = db.Encargos.Find(id);
            if (encargos == null)
            {
                return NotFound();
            }

            return Ok(new EncargosDTO(encargos));
        }

        // PUT: api/Encargos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEncargos(int id, Encargos encargos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != encargos.EmpresaCod)
            {
                return BadRequest();
            }

            db.Entry(encargos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncargosExists(id))
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

        // POST: api/Encargos
        [ResponseType(typeof(EncargosDTO))]
        public IHttpActionResult PostEncargos(Encargos encargos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Encargos.Add(encargos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EncargosExists(encargos.EmpresaCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = encargos.EmpresaCod }, new EncargosDTO(encargos));
        }

        // DELETE: api/Encargos/5
        [ResponseType(typeof(EncargosDTO))]
        public IHttpActionResult DeleteEncargos(int id)
        {
            Encargos encargos = db.Encargos.Find(id);
            if (encargos == null)
            {
                return NotFound();
            }

            EncargosDTO e = new EncargosDTO(encargos);
            db.Encargos.Remove(encargos);
            db.SaveChanges();

            return Ok(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EncargosExists(int id)
        {
            return db.Encargos.Count(e => e.EmpresaCod == id) > 0;
        }
    }
}
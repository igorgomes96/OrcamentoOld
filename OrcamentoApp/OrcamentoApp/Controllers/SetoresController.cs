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
    public class SetoresController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Setores
        public IEnumerable<SetorDTO> GetSetor()
        {
            return db.Setor.ToList().Select(x => new SetorDTO(x));
        }

        // GET: api/Setores/5
        [ResponseType(typeof(SetorDTO))]
        public IHttpActionResult GetSetor(string id)
        {
            Setor setor = db.Setor.Find(id);
            if (setor == null)
            {
                return NotFound();
            }

            return Ok(new SetorDTO(setor));
        }

        // PUT: api/Setores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSetor(int id, Setor setor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setor.Codigo)
            {
                return BadRequest();
            }

            db.Entry(setor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetorExists(id))
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

        // POST: api/Setores
        [ResponseType(typeof(SetorDTO))]
        public IHttpActionResult PostSetor(Setor setor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Setor.Add(setor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SetorExists(setor.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = setor.Codigo }, new SetorDTO(setor));
        }

        // DELETE: api/Setores/5
        [ResponseType(typeof(SetorDTO))]
        public IHttpActionResult DeleteSetor(string id)
        {
            Setor setor = db.Setor.Find(id);
            if (setor == null)
            {
                return NotFound();
            }

            SetorDTO s = new SetorDTO(setor);
            db.Setor.Remove(setor);
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

        private bool SetorExists(int id)
        {
            return db.Setor.Count(e => e.Codigo == id) > 0;
        }
    }
}
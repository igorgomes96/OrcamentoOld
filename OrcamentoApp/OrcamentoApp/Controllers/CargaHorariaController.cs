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
    public class CargaHorariaController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/CargaHoraria
        public IQueryable<CargaHoraria> GetCargaHoraria()
        {
            return db.CargaHoraria;
        }

        // GET: api/CargaHoraria/5
        [ResponseType(typeof(CargaHoraria))]
        public IHttpActionResult GetCargaHoraria(int id)
        {
            CargaHoraria cargaHoraria = db.CargaHoraria.Find(id);
            if (cargaHoraria == null)
            {
                return NotFound();
            }

            return Ok(cargaHoraria);
        }

        // PUT: api/CargaHoraria/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCargaHoraria(int id, CargaHoraria cargaHoraria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cargaHoraria.CargaHorariaCod)
            {
                return BadRequest();
            }

            db.Entry(cargaHoraria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargaHorariaExists(id))
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

        // POST: api/CargaHoraria
        [ResponseType(typeof(CargaHoraria))]
        public IHttpActionResult PostCargaHoraria(CargaHoraria cargaHoraria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CargaHoraria.Add(cargaHoraria);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CargaHorariaExists(cargaHoraria.CargaHorariaCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cargaHoraria.CargaHorariaCod }, cargaHoraria);
        }

        // DELETE: api/CargaHoraria/5
        [ResponseType(typeof(CargaHoraria))]
        public IHttpActionResult DeleteCargaHoraria(int id)
        {
            CargaHoraria cargaHoraria = db.CargaHoraria.Find(id);
            if (cargaHoraria == null)
            {
                return NotFound();
            }

            db.CargaHoraria.Remove(cargaHoraria);
            db.SaveChanges();

            return Ok(cargaHoraria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CargaHorariaExists(int id)
        {
            return db.CargaHoraria.Count(e => e.CargaHorariaCod == id) > 0;
        }
    }
}
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
    public class ReceitasController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Receitas
        public IQueryable<Receita> GetReceita()
        {
            return db.Receita;
        }

        // GET: api/Receitas/5
        [ResponseType(typeof(Receita))]
        public IHttpActionResult GetReceita(string id)
        {
            Receita receita = db.Receita.Find(id);
            if (receita == null)
            {
                return NotFound();
            }

            return Ok(receita);
        }

        // PUT: api/Receitas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReceita(string id, Receita receita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != receita.Atividade)
            {
                return BadRequest();
            }

            db.Entry(receita).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceitaExists(id))
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

        // POST: api/Receitas
        [ResponseType(typeof(Receita))]
        public IHttpActionResult PostReceita(Receita receita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Receita.Add(receita);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReceitaExists(receita.Atividade))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = receita.Atividade }, receita);
        }

        // DELETE: api/Receitas/5
        [ResponseType(typeof(Receita))]
        public IHttpActionResult DeleteReceita(string id)
        {
            Receita receita = db.Receita.Find(id);
            if (receita == null)
            {
                return NotFound();
            }

            db.Receita.Remove(receita);
            db.SaveChanges();

            return Ok(receita);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReceitaExists(string id)
        {
            return db.Receita.Count(e => e.Atividade == id) > 0;
        }
    }
}
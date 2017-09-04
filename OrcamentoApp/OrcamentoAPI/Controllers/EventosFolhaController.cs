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
    public class EventosFolhaController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/EventosFolha
        public IEnumerable<EventoFolhaDTO> GetEventoFolha()
        {
            return db.EventoFolha.ToList()
                .Select(x => new EventoFolhaDTO(x));
        }

        // GET: api/EventosFolha/5
        [ResponseType(typeof(EventoFolhaDTO))]
        public IHttpActionResult GetEventoFolha(string id)
        {
            EventoFolha eventoFolha = db.EventoFolha.Find(id);
            if (eventoFolha == null)
            {
                return NotFound();
            }

            return Ok(new EventoFolhaDTO(eventoFolha));
        }

        // PUT: api/EventosFolha/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventoFolha(string id, EventoFolha eventoFolha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventoFolha.Codigo)
            {
                return BadRequest();
            }

            db.Entry(eventoFolha).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoFolhaExists(id))
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

        // POST: api/EventosFolha
        [ResponseType(typeof(EventoFolhaDTO))]
        public IHttpActionResult PostEventoFolha(EventoFolha eventoFolha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventoFolha.Add(eventoFolha);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EventoFolhaExists(eventoFolha.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eventoFolha.Codigo }, new EventoFolhaDTO(eventoFolha));
        }

        // DELETE: api/EventosFolha/5
        [ResponseType(typeof(EventoFolhaDTO))]
        public IHttpActionResult DeleteEventoFolha(string id)
        {
            EventoFolha eventoFolha = db.EventoFolha.Find(id);
            if (eventoFolha == null)
            {
                return NotFound();
            }

            EventoFolhaDTO e = new EventoFolhaDTO(eventoFolha);
            db.EventoFolha.Remove(eventoFolha);
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

        private bool EventoFolhaExists(string id)
        {
            return db.EventoFolha.Count(e => e.Codigo == id) > 0;
        }
    }
}
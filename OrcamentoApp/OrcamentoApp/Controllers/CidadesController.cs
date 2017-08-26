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
    public class CidadesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Cidades
        public IEnumerable<CidadeDTO> GetCidade()
        {
            return db.Cidade.ToList().Select(x => new CidadeDTO(x));
        }

        // GET: api/Cidades/5
        [ResponseType(typeof(CidadeDTO))]
        public IHttpActionResult GetCidade(string id)
        {
            Cidade cidade = db.Cidade.Find(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return Ok(new CidadeDTO(cidade));
        }

        // PUT: api/Cidades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCidade(string id, Cidade cidade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cidade.NomeCidade)
            {
                return BadRequest();
            }

            db.Entry(cidade).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CidadeExists(id))
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

        // POST: api/Cidades
        [ResponseType(typeof(CidadeDTO))]
        public IHttpActionResult PostCidade(Cidade cidade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cidade.Add(cidade);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CidadeExists(cidade.NomeCidade))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cidade.NomeCidade }, new CidadeDTO(cidade));
        }

        // DELETE: api/Cidades/5
        [ResponseType(typeof(CidadeDTO))]
        public IHttpActionResult DeleteCidade(string id)
        {
            Cidade cidade = db.Cidade.Find(id);
            if (cidade == null)
            {
                return NotFound();
            }

            CidadeDTO c = new CidadeDTO(cidade);
            db.Cidade.Remove(cidade);
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

        private bool CidadeExists(string id)
        {
            return db.Cidade.Count(e => e.NomeCidade == id) > 0;
        }
    }
}
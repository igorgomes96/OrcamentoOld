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
    public class ReajustesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Reajustes
        public IEnumerable<ReajusteDTO> GetReajuste(int? codSindicato = null, int? ano = null)
        {
            return db.Reajuste.ToList()
                .Where(x => (codSindicato == null || x.SindicatoCod == codSindicato) && (ano == null || x.Ano == ano))
                .Select(x => new ReajusteDTO(x));
        }

        // GET: api/Reajustes/5
        [Route("api/Reajustes/{codSindicato}/{ano}")]
        [ResponseType(typeof(ReajusteDTO))]
        public IHttpActionResult GetReajuste(int codSindicato, int ano)
        {
            Reajuste reajuste = db.Reajuste.Find(ano, codSindicato);
            if (reajuste == null)
            {
                return NotFound();
            }

            return Ok(new ReajusteDTO(reajuste));
        }

        // PUT: api/Reajustes/5
        [Route("api/Reajustes/{codSindicato}/{ano}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReajuste(int codSindicato, int ano, Reajuste reajuste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ano != reajuste.Ano || codSindicato != reajuste.SindicatoCod)
            {
                return BadRequest();
            }

            db.Entry(reajuste).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReajusteExists(codSindicato, ano))
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

        // POST: api/Reajustes
        [ResponseType(typeof(ReajusteDTO))]
        public IHttpActionResult PostReajuste(Reajuste reajuste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reajuste.Add(reajuste);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReajusteExists(reajuste.SindicatoCod, reajuste.Ano))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reajuste.Ano }, new ReajusteDTO(reajuste));
        }

        // DELETE: api/Reajustes/5
        [Route("api/Reajustes/{codSindicato}/{ano}")]
        [ResponseType(typeof(ReajusteDTO))]
        public IHttpActionResult DeleteReajuste(int codSindicato, int ano)
        {
            Reajuste reajuste = db.Reajuste.Find(ano, codSindicato);
            if (reajuste == null)
            {
                return NotFound();
            }
            ReajusteDTO r = new ReajusteDTO(reajuste);

            db.Reajuste.Remove(reajuste);
            db.SaveChanges();

            return Ok(r);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReajusteExists(int codSindicato, int ano)
        {
            return db.Reajuste.Count(e => e.Ano == ano && e.SindicatoCod == codSindicato) > 0;
        }
    }
}
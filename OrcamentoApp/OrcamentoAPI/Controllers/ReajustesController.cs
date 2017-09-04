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
using System.Data.Entity.Migrations;

namespace OrcamentoAPI.Controllers
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
        [Route("api/Reajustes/{ano}/{mes}/{codSindicato}")]
        [ResponseType(typeof(ReajusteDTO))]
        public IHttpActionResult GetReajuste(int ano, int mes, int codSindicato)
        {
            Reajuste reajuste = db.Reajuste.Find(ano, mes, codSindicato);
            if (reajuste == null)
            {
                return NotFound();
            }

            return Ok(new ReajusteDTO(reajuste));
        }

        [HttpPost]
        [Route("api/Reajustes/SaveAll")]
        [ResponseType(typeof(void))]
        public IHttpActionResult SaveAll (IEnumerable<Reajuste> reajustes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (Reajuste r in reajustes)
            {
                db.Reajuste.AddOrUpdate(r);
            }

            try
            {
                db.SaveChanges();
            } catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        // PUT: api/Reajustes/5
        [Route("api/Reajustes/{ano}/{mes}/{codSindicato}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReajuste(int ano, int mes, int codSindicato, Reajuste reajuste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ano != reajuste.Ano || codSindicato != reajuste.SindicatoCod || mes != reajuste.MesReajuste)
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
                if (!ReajusteExists(ano, mes, codSindicato))
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
                if (ReajusteExists(reajuste.Ano, reajuste.MesReajuste, reajuste.SindicatoCod))
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
        [Route("api/Reajustes/{ano}/{mes}/{codSindicato}")]
        [ResponseType(typeof(ReajusteDTO))]
        public IHttpActionResult DeleteReajuste(int ano, int mes, int codSindicato)
        {
            Reajuste reajuste = db.Reajuste.Find(ano, mes, codSindicato);
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

        private bool ReajusteExists(int ano, int mes, int codSindicato)
        {
            return db.Reajuste.Count(e => e.Ano == ano && e.SindicatoCod == codSindicato && e.MesReajuste == mes) > 0;
        }
    }
}
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
    public class ReajConvenioMedicosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/ReajConvenioMedicos
        public IEnumerable<ReajConvenioMedDTO> GetReajConvenioMed(int? ano = null, int? plano = null)
        {
            return db.ReajConvenioMed.ToList()
                .Where(x => (ano == null || x.Ano == ano) && (plano==null || x.ConvenioMedCod == plano))
                .Select(x => new ReajConvenioMedDTO(x));
        }

        // GET: api/ReajConvenioMedicos/5
        [Route("api/ReajConvenioMedicos/{ano}/{plano}/{mes}")]
        [ResponseType(typeof(ReajConvenioMedDTO))]
        public IHttpActionResult GetReajConvenioMed(int ano, int plano, int mes)
        {
            ReajConvenioMed reajConvenioMed = db.ReajConvenioMed.Find(ano, plano, mes);
            if (reajConvenioMed == null)
            {
                return NotFound();
            }

            return Ok(new ReajConvenioMedDTO(reajConvenioMed));
        }

        [HttpPost]
        [Route("api/ReajConvenioMedicos/SaveAll")]
        [ResponseType(typeof(void))]
        public IHttpActionResult SaveAll (IEnumerable<ReajConvenioMed> reajustes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (ReajConvenioMed r in reajustes)
            {
                db.ReajConvenioMed.AddOrUpdate(r);
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

        // PUT: api/ReajConvenioMedicos/5
        [Route("api/ReajConvenioMedicos/{ano}/{plano}/{mes}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReajConvenioMed(int ano, int plano, int mes, ReajConvenioMed reajConvenioMed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ano != reajConvenioMed.Ano || plano != reajConvenioMed.ConvenioMedCod || mes != reajConvenioMed.MesReajuste)
            {
                return BadRequest();
            }

            db.Entry(reajConvenioMed).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReajConvenioMedExists(ano, plano, mes))
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

        // POST: api/ReajConvenioMedicos
        [ResponseType(typeof(ReajConvenioMedDTO))]
        public IHttpActionResult PostReajConvenioMed(ReajConvenioMed reajConvenioMed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReajConvenioMed.Add(reajConvenioMed);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReajConvenioMedExists(reajConvenioMed.Ano, reajConvenioMed.ConvenioMedCod, reajConvenioMed.MesReajuste))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reajConvenioMed.Ano }, new ReajConvenioMedDTO(reajConvenioMed));
        }

        // DELETE: api/ReajConvenioMedicos/5
        [Route("api/ReajConvenioMedicos/{ano}/{plano}/{mes}")]
        [ResponseType(typeof(ReajConvenioMedDTO))]
        public IHttpActionResult DeleteReajConvenioMed(int ano, int plano, int mes)
        {
            ReajConvenioMed reajConvenioMed = db.ReajConvenioMed.Find(ano, plano, mes);
            if (reajConvenioMed == null)
            {
                return NotFound();
            }

            ReajConvenioMedDTO r = new ReajConvenioMedDTO(reajConvenioMed);
            db.ReajConvenioMed.Remove(reajConvenioMed);
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

        private bool ReajConvenioMedExists(int ano, int plano, int mes)
        {
            return db.ReajConvenioMed.Count(e => e.Ano == ano && e.ConvenioMedCod == plano && e.MesReajuste == mes) > 0;
        }
    }
}
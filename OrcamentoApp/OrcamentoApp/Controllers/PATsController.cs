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
    public class PATsController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/PATs
        public IEnumerable<PATDTO> GetPAT(int? sindicatoCod = null)
        {
            return db.PAT.ToList()
                .Where(x => sindicatoCod == null || x.SindicatoCod == sindicatoCod)
                .Select(x => new PATDTO(x));
        }

        // GET: api/PATs/5
        [Route("api/PATs/{sindicatoCod}/{cargaHoraria}")]
        [ResponseType(typeof(PATDTO))]
        public IHttpActionResult GetPAT(int sindicatoCod, int cargaHoraria)
        {
            PAT pAT = db.PAT.Find(cargaHoraria, sindicatoCod);
            if (pAT == null)
            {
                return NotFound();
            }

            return Ok(new PATDTO(pAT));
        }

        // PUT: api/PATs/5
        [Route("api/PATs/{sindicatoCod}/{cargaHoraria}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPAT(int sindicatoCod, int cargaHoraria, PAT pAT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cargaHoraria != pAT.CargaHoraria || sindicatoCod != pAT.SindicatoCod)
            {
                return BadRequest();
            }

            db.Entry(pAT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PATExists(sindicatoCod, cargaHoraria))
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

        // POST: api/PATs
        [ResponseType(typeof(PATDTO))]
        public IHttpActionResult PostPAT(PAT pAT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PAT.Add(pAT);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PATExists(pAT.SindicatoCod, pAT.CargaHoraria))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pAT.CargaHoraria }, new PATDTO(pAT));
        }

        // DELETE: api/PATs/5
        [Route("api/PATs/{sindicatoCod}/{cargaHoraria}")]
        [ResponseType(typeof(PATDTO))]
        public IHttpActionResult DeletePAT(int sindicatoCod, int cargaHoraria)
        {
            PAT pAT = db.PAT.Find(cargaHoraria, sindicatoCod);
            if (pAT == null)
            {
                return NotFound();
            }

            PATDTO p = new PATDTO(pAT);
            db.PAT.Remove(pAT);
            db.SaveChanges();

            return Ok(p);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PATExists(int sindicatoCod, int cargaHoraria)
        {
            return db.PAT.Count(e => e.CargaHoraria == cargaHoraria && e.SindicatoCod == sindicatoCod) > 0;
        }
    }
}
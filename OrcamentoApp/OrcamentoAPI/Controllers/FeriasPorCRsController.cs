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
    public class FeriasPorCRsController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/FeriasPorCRs
        public IEnumerable<FeriasPorCRDTO> GetFeriasPorCRAll(string cr = null, int? codCiclo = null)
        {
            return db.FeriasPorCR.ToList()
                .Where(x => (cr == null || x.CodigoCR == cr) && (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo))
                .Select(x => new FeriasPorCRDTO(x));
        }

        [ResponseType(typeof(void))]
        [Route("api/FeriasPorCRs/SaveAll/{codCiclo}")]
        [HttpPost]
        public IHttpActionResult SaveAllPorCiclo(int codCiclo, IEnumerable<FeriasPorCR> ferias)
        {
            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();
            float soma = 0;

            foreach (FeriasPorCR f in ferias)
            {
                MesOrcamento mes = db.MesOrcamento.Find(f.CodMesOrcamento);
                if (mes == null)
                    return NotFound();
                else if (mes.CicloCod != codCiclo)
                    return BadRequest("Mês fora do ciclo informado!");

                if (f.Percentual == 0)
                {
                    if (FeriasPorCRExists(f.CodigoCR, f.CodMesOrcamento))
                        db.Entry(f).State = EntityState.Deleted;
                } else
                {
                    db.FeriasPorCR.AddOrUpdate(f);
                    soma += f.Percentual;
                }
            }

            if (soma != 1 && soma != 0)
                return BadRequest("A soma dos percentuais não correspondem a 100% (" + soma + ")!");

            try
            {
                db.SaveChanges();
            } catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        // GET: api/FeriasPorCRs/5
        [ResponseType(typeof(FeriasPorCRDTO))]
        [Route("api/FeriasPorCRs/{cr}/{mes}")]
        public IHttpActionResult GetFeriasPorCR(string cr, int mes)
        {
            FeriasPorCR feriasPorCR = db.FeriasPorCR.Find(cr, mes);
            if (feriasPorCR == null)
            {
                return NotFound();
            }

            return Ok(new FeriasPorCRDTO(feriasPorCR));
        }

        // PUT: api/FeriasPorCRs/5
        [ResponseType(typeof(void))]
        [Route("api/FeriasPorCRs/{cr}/{mes}")]
        public IHttpActionResult PutFeriasPorCR(string cr, int mes, FeriasPorCR feriasPorCR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cr != feriasPorCR.CodigoCR || mes != feriasPorCR.CodMesOrcamento)
            {
                return BadRequest();
            }

            db.Entry(feriasPorCR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeriasPorCRExists(cr, mes))
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

        // POST: api/FeriasPorCRs
        [ResponseType(typeof(FeriasPorCRDTO))]
        public IHttpActionResult PostFeriasPorCR(FeriasPorCR feriasPorCR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FeriasPorCR.Add(feriasPorCR);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FeriasPorCRExists(feriasPorCR.CodigoCR, feriasPorCR.CodMesOrcamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = feriasPorCR.CodigoCR }, new FeriasPorCRDTO(feriasPorCR));
        }

        // DELETE: api/FeriasPorCRs/5
        [ResponseType(typeof(FeriasPorCRDTO))]
        [Route("api/FeriasPorCRs/{cr}/{mes}")]
        public IHttpActionResult DeleteFeriasPorCR(string cr, int mes)
        {
            FeriasPorCR feriasPorCR = db.FeriasPorCR.Find(cr, mes);
            if (feriasPorCR == null)
            {
                return NotFound();
            }

            FeriasPorCRDTO f = new FeriasPorCRDTO(feriasPorCR);
            db.FeriasPorCR.Remove(feriasPorCR);
            db.SaveChanges();

            return Ok(f);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeriasPorCRExists(string cr, int mes)
        {
            return db.FeriasPorCR.Count(e => e.CodigoCR == cr && e.CodMesOrcamento == mes) > 0;
        }
    }
}
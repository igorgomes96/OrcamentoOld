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
using System.Data.Entity.Migrations;

namespace OrcamentoApp.Controllers
{
    public class ValoresAbertosCRsController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/ValoresAbertosCRs
        public IEnumerable<ValoresAbertosCRDTO> GetValoresAbertosCR(string cr = null, string codEvento = null, int? codCiclo = null)
        {
            return db.ValoresAbertosCR.ToList()
                .Where(x => (cr == null || x.CodigoCR == cr) && (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo) && (codEvento == null || x.CodEvento == codEvento))
                .Select(x => new ValoresAbertosCRDTO(x));
        }

        [ResponseType(typeof(ValoresAbertosCRCicloDTO))]
        [HttpGet]
        [Route("api/ValoresAbertosCRs/PorCiclo/{cr}/{codEvento}/{codCiclo}")]
        public IHttpActionResult GetValoresPorCiclo(string cr, string codEvento, int codCiclo)
        {
            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return null;

            CentroCusto centroCusto = db.CentroCusto.Find(cr);
            if (centroCusto == null) return null;

            EventoFolha evento = db.EventoFolha.Find(codEvento);
            if (evento == null) return null;

            return Ok(new ValoresAbertosCRCicloDTO(centroCusto, evento, ciclo));

        }

        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/ValoresAbertosCRs/SaveAll")]
        public IHttpActionResult SaveAll (IEnumerable<ValoresAbertosCR> valores)
        {
            foreach(ValoresAbertosCR v in valores)
            {
                if (v.Valor == 0)
                {
                    if (ValoresAbertosCRExists(v.CodEvento, v.CodMesOrcamento, v.CodigoCR))
                        db.ValoresAbertosCR.Remove(v);
                }
                else
                    db.ValoresAbertosCR.AddOrUpdate(v);
            }

            try
            {
                db.SaveChanges();
            } catch(Exception e)
            {
                return InternalServerError(e);
            }
            return Ok();
        }

        // PUT: api/ValoresAbertosCRs/5
        [ResponseType(typeof(void))]
        [Route("api/ValoresAbertosCRs/{codEvento}/{codMes}/{cr}")]
        public IHttpActionResult PutValoresAbertosCR(string codEvento, int codMes, string cr, ValoresAbertosCR valoresAbertosCR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (codEvento != valoresAbertosCR.CodEvento || codMes != valoresAbertosCR.CodMesOrcamento || cr != valoresAbertosCR.CodigoCR)
            {
                return BadRequest();
            }

            db.Entry(valoresAbertosCR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValoresAbertosCRExists(codEvento, codMes, cr))
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

        // POST: api/ValoresAbertosCRs
        [ResponseType(typeof(ValoresAbertosCRDTO))]
        public IHttpActionResult PostValoresAbertosCR(ValoresAbertosCR valoresAbertosCR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ValoresAbertosCR.Add(valoresAbertosCR);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ValoresAbertosCRExists(valoresAbertosCR.CodEvento, valoresAbertosCR.CodMesOrcamento, valoresAbertosCR.CodigoCR))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = valoresAbertosCR.CodEvento }, new ValoresAbertosCRDTO(valoresAbertosCR));
        }

        // DELETE: api/ValoresAbertosCRs/5
        [ResponseType(typeof(ValoresAbertosCRDTO))]
        [Route("api/ValoresAbertosCRs/{codEvento}/{codMes}/{cr}")]
        public IHttpActionResult DeleteValoresAbertosCR(string codEvento, int codMes, string cr)
        {
            ValoresAbertosCR valoresAbertosCR = db.ValoresAbertosCR.Find(codEvento, codMes, cr);
            if (valoresAbertosCR == null)
            {
                return NotFound();
            }
            ValoresAbertosCRDTO v = new ValoresAbertosCRDTO(valoresAbertosCR);
            db.ValoresAbertosCR.Remove(valoresAbertosCR);
            db.SaveChanges();

            return Ok(v);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ValoresAbertosCRExists(string codEvento, int codMes, string cr)
        {
            return db.ValoresAbertosCR.Count(e => e.CodEvento == codEvento && e.CodMesOrcamento == codMes && e.CodigoCR == cr) > 0;
        }
    }
}
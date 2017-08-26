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
    public class HEsBaseController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/HEsBase
        public IEnumerable<HEBaseDTO> GetHEBase(int? percHoras = null, string matricula = null, int? codCiclo = null)
        {
            return db.HEBase.ToList()
                .Where(x => (percHoras == null || x.PercentualHoras == percHoras) && (matricula == null || x.FuncionarioMatricula == matricula) || (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo))
                .Select(x => new HEBaseDTO(x));
        }

        [ResponseType(typeof(FuncionarioHEsDTO))]
        [Route("api/HEsBase/FuncionarioHE/{matricula}/{codCiclo}")]
        public IHttpActionResult GetFuncionarioHEs(string matricula, int codCiclo)
        {
            Funcionario f = db.Funcionario.Find(matricula);

            if (f == null) return NotFound();

            Ciclo c = db.Ciclo.Find(codCiclo);

            if (c == null) return NotFound();

            return Ok(new FuncionarioHEsDTO(f, c));
        }

        // PUT: api/HEsBase/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHEBase(string matricula, int percentual, int mesOrcamento, HEBase hEBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (matricula != hEBase.FuncionarioMatricula || percentual != hEBase.PercentualHoras || mesOrcamento != hEBase.CodMesOrcamento)
            {
                return BadRequest();
            }

            db.Entry(hEBase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HEBaseExists(matricula, percentual, mesOrcamento))
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

        // POST: api/HEsBase
        [ResponseType(typeof(HEBaseDTO))]
        public IHttpActionResult PostHEBase(HEBase hEBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HEBase.Add(hEBase);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HEBaseExists(hEBase.FuncionarioMatricula, hEBase.PercentualHoras, hEBase.CodMesOrcamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hEBase.FuncionarioMatricula }, new HEBaseDTO(hEBase));
        }

        // DELETE: api/HEsBase/5
        [ResponseType(typeof(HEBaseDTO))]
        public IHttpActionResult DeleteHEBase(string matricula, int percentual, int mesOrcamento)
        {
            HEBase hEBase = db.HEBase.Find(matricula, percentual, mesOrcamento);
            if (hEBase == null)
            {
                return NotFound();
            }

            HEBaseDTO h = new HEBaseDTO(hEBase);

            db.HEBase.Remove(hEBase);
            db.SaveChanges();

            return Ok(h);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HEBaseExists(string matricula, int percentual, int mesOrcamento)
        {
            return db.HEBase.Count(e => e.FuncionarioMatricula == matricula && e.PercentualHoras == percentual && e.CodMesOrcamento == mesOrcamento) > 0;
        }
    }
}
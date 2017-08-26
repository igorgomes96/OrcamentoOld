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
    public class AdNoturnosBaseController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/AdNoturnosBase
        public IEnumerable<AdNoturnoBaseDTO> GetAdNoturno(int? percHoras = null, string matricula = null, int? codCiclo = null)
        {
            return db.AdNoturnoBase.ToList()
                .Where(x => (percHoras == null || x.PercentualHoras == percHoras) && (matricula == null || x.FuncionarioMatricula == matricula) || (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo))
                .Select(x => new AdNoturnoBaseDTO(x));
        }

        [ResponseType(typeof(FuncionarioHNsDTO))]
        [Route("api/AdNoturnosBase/FuncionarioHN/{matricula}/{codCiclo}")]
        public IHttpActionResult GetFuncionarioHEs(string matricula, int codCiclo)
        {
            Funcionario f = db.Funcionario.Find(matricula);

            if (f == null) return NotFound();

            Ciclo c = db.Ciclo.Find(codCiclo);

            if (c == null) return NotFound();

            return Ok(new FuncionarioHNsDTO(f, c));
        }

        // PUT: api/AdNoturnosBase/5
        [ResponseType(typeof(void))]
        [Route("api/AdNoturnosBase/{matricula}/{percentual}/{mesOrcamento}")]
        public IHttpActionResult PutAdNoturno(string matricula, int percentual, int mesOrcamento, AdNoturnoBase adNoturno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (matricula != adNoturno.FuncionarioMatricula || percentual != adNoturno.PercentualHoras || mesOrcamento != adNoturno.CodMesOrcamento)
            {
                return BadRequest();
            }

            db.Entry(adNoturno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdNoturnoExists(matricula, percentual, mesOrcamento))
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

        // POST: api/AdNoturnosBase
        [ResponseType(typeof(AdNoturnoBaseDTO))]
        public IHttpActionResult PostAdNoturno(AdNoturnoBase adNoturno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdNoturnoBase.Add(adNoturno);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdNoturnoExists(adNoturno.FuncionarioMatricula, adNoturno.PercentualHoras, adNoturno.CodMesOrcamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = adNoturno.FuncionarioMatricula }, new AdNoturnoBaseDTO(adNoturno));
        }

        // DELETE: api/AdNoturnosBase/5
        [ResponseType(typeof(AdNoturnoBaseDTO))]
        public IHttpActionResult DeleteAdNoturno(string matricula, int percentual, int mesOrcamento)
        {
            AdNoturnoBase adNoturno = db.AdNoturnoBase.Find(matricula, percentual, mesOrcamento);
            if (adNoturno == null)
            {
                return NotFound();
            }

            AdNoturnoBaseDTO h = new AdNoturnoBaseDTO(adNoturno);

            db.AdNoturnoBase.Remove(adNoturno);
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

        private bool AdNoturnoExists(string matricula, int percentual, int mesOrcamento)
        {
            return db.AdNoturnoBase.Count(e => e.FuncionarioMatricula == matricula && e.PercentualHoras == percentual && e.CodMesOrcamento == mesOrcamento) > 0;
        }
    }
}
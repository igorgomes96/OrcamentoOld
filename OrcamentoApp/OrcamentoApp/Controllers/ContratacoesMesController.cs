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
    public class ContratacoesMesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/ContratacoesMes
        public IEnumerable<ContratacaoMesDTO> GetContratacaoMes(Nullable<int> codContratacao = null)
        {
            return db.ContratacaoMes.ToList().Where(x => codContratacao == null || x.ContratacaoCod == codContratacao)
                .Select(x => new ContratacaoMesDTO(x));
        }

        // GET: api/ContratacoesMes/5
        [Route("api/ContratacoesMes/{idContr}/{idMes}")]
        [ResponseType(typeof(ContratacaoMesDTO))]
        public IHttpActionResult GetContratacaoMes(int idContr, int idMes)
        {
            ContratacaoMes contratacaoMes = db.ContratacaoMes.Find(idContr, idMes);
            if (contratacaoMes == null)
            {
                return NotFound();
            }

            return Ok(new ContratacaoMesDTO(contratacaoMes));
        }

        // PUT: api/ContratacoesMes/5
        [Route("api/ContratacoesMes/{idContr}/{idMes}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContratacaoMes(int idContr, int idMes, ContratacaoMes contratacaoMes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (idContr != contratacaoMes.ContratacaoCod || idMes != contratacaoMes.MesOrcamentoCod)
            {
                return BadRequest();
            }

            db.Entry(contratacaoMes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratacaoMesExists(idContr, idMes))
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

        // POST: api/ContratacoesMes
        [ResponseType(typeof(ContratacaoMes))]
        public IHttpActionResult PostContratacaoMes(ContratacaoMes contratacaoMes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContratacaoMes.Add(contratacaoMes);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ContratacaoMesExists(contratacaoMes.ContratacaoCod, contratacaoMes.MesOrcamentoCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = contratacaoMes.ContratacaoCod }, new ContratacaoMesDTO(contratacaoMes));
        }

        // DELETE: api/ContratacoesMes/5
        [Route("api/ContratacoesMes/{idContr}/{idMes}")]
        [ResponseType(typeof(ContratacaoMes))]
        public IHttpActionResult DeleteContratacaoMes(int idContr, int idMes)
        {
            ContratacaoMes contratacaoMes = db.ContratacaoMes.Find(idContr, idMes);
            if (contratacaoMes == null)
            {
                return NotFound();
            }

            ContratacaoMesDTO c = new ContratacaoMesDTO(contratacaoMes);
            db.ContratacaoMes.Remove(contratacaoMes);
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

        private bool ContratacaoMesExists(int idContr, int idMes)
        {
            return db.ContratacaoMes.Count(e => e.ContratacaoCod == idContr && e.MesOrcamentoCod == idMes) > 0;
        }
    }
}
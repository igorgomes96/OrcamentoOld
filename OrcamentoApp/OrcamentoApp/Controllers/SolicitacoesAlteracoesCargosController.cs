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
    public class SolicitacoesAlteracoesCargosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/SolicitacoesAlteracoesCargos
        public IEnumerable<SolicitacaoAlteracaoCargoDTO> GetSolicitacaoAlteracaoCargo()
        {
            return db.SolicitacaoAlteracaoCargo.ToList().Select(x => new SolicitacaoAlteracaoCargoDTO(x));
        }

        // GET: api/SolicitacoesAlteracoesCargos/5
        [ResponseType(typeof(SolicitacaoAlteracaoCargoDTO))]
        public IHttpActionResult GetSolicitacaoAlteracaoCargo(int id)
        {
            SolicitacaoAlteracaoCargo solicitacaoAlteracaoCargo = db.SolicitacaoAlteracaoCargo.Find(id);
            if (solicitacaoAlteracaoCargo == null)
            {
                return NotFound();
            }

            return Ok(new SolicitacaoAlteracaoCargoDTO(solicitacaoAlteracaoCargo));
        }

        // PUT: api/SolicitacoesAlteracoesCargos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSolicitacaoAlteracaoCargo(int id, SolicitacaoAlteracaoCargo solicitacaoAlteracaoCargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solicitacaoAlteracaoCargo.SolicitacaoCod)
            {
                return BadRequest();
            }

            db.Entry(solicitacaoAlteracaoCargo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitacaoAlteracaoCargoExists(id))
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

        // POST: api/SolicitacoesAlteracoesCargos
        [ResponseType(typeof(SolicitacaoAlteracaoCargoDTO))]
        public IHttpActionResult PostSolicitacaoAlteracaoCargo(SolicitacaoAlteracaoCargo solicitacaoAlteracaoCargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SolicitacaoAlteracaoCargo.Add(solicitacaoAlteracaoCargo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SolicitacaoAlteracaoCargoExists(solicitacaoAlteracaoCargo.SolicitacaoCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = solicitacaoAlteracaoCargo.SolicitacaoCod }, new SolicitacaoAlteracaoCargoDTO(solicitacaoAlteracaoCargo));
        }

        // DELETE: api/SolicitacoesAlteracoesCargos/5
        [ResponseType(typeof(SolicitacaoAlteracaoCargoDTO))]
        public IHttpActionResult DeleteSolicitacaoAlteracaoCargo(int id)
        {
            SolicitacaoAlteracaoCargo solicitacaoAlteracaoCargo = db.SolicitacaoAlteracaoCargo.Find(id);
            if (solicitacaoAlteracaoCargo == null)
            {
                return NotFound();
            }

            SolicitacaoAlteracaoCargoDTO s = new SolicitacaoAlteracaoCargoDTO(solicitacaoAlteracaoCargo);
            db.SolicitacaoAlteracaoCargo.Remove(solicitacaoAlteracaoCargo);
            db.SaveChanges();

            return Ok(s);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SolicitacaoAlteracaoCargoExists(int id)
        {
            return db.SolicitacaoAlteracaoCargo.Count(e => e.SolicitacaoCod == id) > 0;
        }
    }
}
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
    public class SolicitacoesAlteracoesSalariosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/SolicitacoesAlteracoesSalarios
        public IEnumerable<SolicitacaoAlteracaoSalarioDTO> GetSolicitacaoAlteracaoSalario()
        {
            return db.SolicitacaoAlteracaoSalario.ToList().Select(x => new SolicitacaoAlteracaoSalarioDTO(x));
        }

        // GET: api/SolicitacoesAlteracoesSalarios/5
        [ResponseType(typeof(SolicitacaoAlteracaoSalarioDTO))]
        public IHttpActionResult GetSolicitacaoAlteracaoSalario(int id)
        {
            SolicitacaoAlteracaoSalario solicitacaoAlteracaoSalario = db.SolicitacaoAlteracaoSalario.Find(id);
            if (solicitacaoAlteracaoSalario == null)
            {
                return NotFound();
            }

            return Ok(new SolicitacaoAlteracaoSalarioDTO(solicitacaoAlteracaoSalario));
        }

        // PUT: api/SolicitacoesAlteracoesSalarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSolicitacaoAlteracaoSalario(int id, SolicitacaoAlteracaoSalario solicitacaoAlteracaoSalario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solicitacaoAlteracaoSalario.SolicitacaoCod)
            {
                return BadRequest();
            }

            db.Entry(solicitacaoAlteracaoSalario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitacaoAlteracaoSalarioExists(id))
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

        // POST: api/SolicitacoesAlteracoesSalarios
        [ResponseType(typeof(SolicitacaoAlteracaoSalarioDTO))]
        public IHttpActionResult PostSolicitacaoAlteracaoSalario(SolicitacaoAlteracaoSalario solicitacaoAlteracaoSalario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SolicitacaoAlteracaoSalario.Add(solicitacaoAlteracaoSalario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SolicitacaoAlteracaoSalarioExists(solicitacaoAlteracaoSalario.SolicitacaoCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = solicitacaoAlteracaoSalario.SolicitacaoCod }, new SolicitacaoAlteracaoSalarioDTO(solicitacaoAlteracaoSalario));
        }

        // DELETE: api/SolicitacoesAlteracoesSalarios/5
        [ResponseType(typeof(SolicitacaoAlteracaoSalarioDTO))]
        public IHttpActionResult DeleteSolicitacaoAlteracaoSalario(int id)
        {
            SolicitacaoAlteracaoSalario solicitacaoAlteracaoSalario = db.SolicitacaoAlteracaoSalario.Find(id);
            if (solicitacaoAlteracaoSalario == null)
            {
                return NotFound();
            }

            SolicitacaoAlteracaoSalarioDTO s = new SolicitacaoAlteracaoSalarioDTO(solicitacaoAlteracaoSalario);
            db.SolicitacaoAlteracaoSalario.Remove(solicitacaoAlteracaoSalario);
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

        private bool SolicitacaoAlteracaoSalarioExists(int id)
        {
            return db.SolicitacaoAlteracaoSalario.Count(e => e.SolicitacaoCod == id) > 0;
        }
    }
}
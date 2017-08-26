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
    public class TransferenciasController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Transferencias
        public IEnumerable<TransferenciaDTO> GetTransferencias(string centroCustoOrigem = null, string centroCustoDestino = null, int? idCiclo = null)
        {
            if (centroCustoOrigem != null && centroCustoDestino != null)
                return db.Transferencia.ToList().Where(x => x.Funcionario.CentroCustoCod == centroCustoOrigem && x.CRDestino == centroCustoDestino && (idCiclo == null || x.MesOrcamento.CicloCod == idCiclo)).Select(x => new TransferenciaDTO(x));
            
            if (centroCustoOrigem != null)
                return db.Transferencia.ToList().Where(x => x.Funcionario.CentroCustoCod == centroCustoOrigem && (idCiclo == null || x.MesOrcamento.CicloCod == idCiclo)).Select(x => new TransferenciaDTO(x));

            if (centroCustoDestino != null)
                return db.Transferencia.ToList().Where(x => x.CRDestino == centroCustoDestino && (idCiclo == null || x.MesOrcamento.CicloCod == idCiclo)).Select(x => new TransferenciaDTO(x));

            return db.Transferencia.ToList().Where(x => idCiclo == null || x.MesOrcamento.CicloCod == idCiclo).Select(x => new TransferenciaDTO(x));
        }

        // GET: api/Transferencias/5
        [Route("api/Transferencias/{crDestino}/{funcMatricula}")]
        [ResponseType(typeof(TransferenciaDTO))]
        public IHttpActionResult GetTransferencia(string crDestino, string funcMatricula)
        {
            Transferencia transferencia = db.Transferencia.Find(crDestino, funcMatricula);
            if (transferencia == null)
            {
                return NotFound();
            }

            return Ok(new TransferenciaDTO(transferencia));
        }

        // PUT: api/Transferencias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransferencia(string crDestino, string funcMatricula, Transferencia transferencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crDestino != transferencia.CRDestino && funcMatricula != transferencia.FuncionarioMatricula)
            {
                return BadRequest();
            }

            db.Entry(transferencia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransferenciaExists(crDestino, funcMatricula))
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

        // POST: api/Transferencias
        [ResponseType(typeof(TransferenciaDTO))]
        public IHttpActionResult PostTransferencia(Transferencia transferencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transferencia.Add(transferencia);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if (TransferenciaExists(transferencia.CRDestino, transferencia.FuncionarioMatricula))
                {
                    return Conflict();
                }
                else
                {
                    throw e;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = transferencia.CRDestino }, new TransferenciaDTO(transferencia));
        }

        // DELETE: api/Transferencias/5
        [ResponseType(typeof(TransferenciaDTO))]
        public IHttpActionResult DeleteTransferencia(string crDestino, string funcMatricula)
        {
            Transferencia transferencia = db.Transferencia.Find(crDestino, funcMatricula);
            if (transferencia == null)
            {
                return NotFound();
            }

            TransferenciaDTO t = new TransferenciaDTO(transferencia);
            db.Transferencia.Remove(transferencia);
            db.SaveChanges();

            return Ok(t);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransferenciaExists(string crDestino, string funcMatricula)
        {
            return db.Transferencia.Count(e => e.CRDestino == crDestino && e.FuncionarioMatricula == funcMatricula) > 0;
        }
    }
}
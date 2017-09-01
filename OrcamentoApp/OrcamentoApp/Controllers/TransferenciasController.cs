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
        public IEnumerable<TransferenciaDTO> GetTransferencias(string centroCustoOrigem = null, string centroCustoDestino = null, int? idCiclo = null, bool? pendente = null)
        {
            return db.Transferencia.ToList()
                .Where(x => (centroCustoOrigem == null || x.CROrigem == centroCustoOrigem) && (centroCustoDestino == null || x.CRDestino == centroCustoDestino) && 
                (idCiclo == null || x.MesOrcamento.CicloCod == idCiclo) && (pendente == null || (x.Aprovado == null && pendente.Value) || (x.Aprovado != null && !pendente.Value)))
                .Select(x => new TransferenciaDTO(x));
        }

        // GET: api/Transferencias/5
        [Route("api/Transferencias/{crDestino}/{funcMatricula}/{mesTrans}")]
        [ResponseType(typeof(TransferenciaDTO))]
        public IHttpActionResult GetTransferencia(string crDestino, string funcMatricula, int mesTrans)
        {
            Transferencia transferencia = db.Transferencia.Find(crDestino, funcMatricula, mesTrans);
            if (transferencia == null)
            {
                return NotFound();
            }

            return Ok(new TransferenciaDTO(transferencia));
        }

        // PUT: api/Transferencias/5
        [ResponseType(typeof(void))]
        [Route("api/Transferencias/{crDestino}/{funcMatricula}/{mesTrans}")]
        public IHttpActionResult PutTransferencia(string crDestino, string funcMatricula, int mesTrans, Transferencia transferencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crDestino != transferencia.CRDestino || funcMatricula != transferencia.FuncionarioMatricula || mesTrans != transferencia.MesTransferencia)
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
                if (!TransferenciaExists(crDestino, funcMatricula, mesTrans))
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

            if (transferencia.CRDestino == null || transferencia.CRDestino == "" ||  db.CentroCusto.Find(transferencia.CRDestino) == null)
                return NotFound();

            db.Transferencia.Add(transferencia);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                if (TransferenciaExists(transferencia.CRDestino, transferencia.FuncionarioMatricula, transferencia.MesTransferencia))
                {
                    return Conflict();
                }
                else
                {
                    return InternalServerError(e);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = transferencia.CRDestino }, new TransferenciaDTO(transferencia));
        }

        // DELETE: api/Transferencias/5
        [ResponseType(typeof(TransferenciaDTO))]
        [Route("api/Transferencias/{crDestino}/{funcMatricula}/{mesTrans}")]
        public IHttpActionResult DeleteTransferencia(string crDestino, string funcMatricula, int mesTrans)
        {
            Transferencia transferencia = db.Transferencia.Find(crDestino, funcMatricula, mesTrans);
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

        private bool TransferenciaExists(string crDestino, string funcMatricula, int mesTrans)
        {
            return db.Transferencia.Count(e => e.CRDestino == crDestino && e.FuncionarioMatricula == funcMatricula && e.MesTransferencia == mesTrans) > 0;
        }
    }
}
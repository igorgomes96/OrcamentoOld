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
    public class MesesOrcamentoController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/MesesOrcamento
        public IEnumerable<MesOrcamentoDTO> GetMesOrcamento(Nullable<int> idCiclo = null)
        {
            return idCiclo == null ? 
                db.MesOrcamento.ToList().Select(x => new MesOrcamentoDTO(x)) :
                db.MesOrcamento.ToList().Where(x => x.CicloCod == idCiclo).Select(x => new MesOrcamentoDTO(x));
        }

        // GET: api/MesesOrcamento/5
        [ResponseType(typeof(MesOrcamentoDTO))]
        public IHttpActionResult GetMesOrcamento(int id)
        {
            MesOrcamento mesOrcamento = db.MesOrcamento.Find(id);
            if (mesOrcamento == null)
            {
                return NotFound();
            }

            return Ok(new MesOrcamentoDTO(mesOrcamento));
        }

        // PUT: api/MesesOrcamento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMesOrcamento(int id, MesOrcamento mesOrcamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mesOrcamento.Codigo)
            {
                return BadRequest();
            }

            db.Entry(mesOrcamento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MesOrcamentoExists(id))
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

        // POST: api/MesesOrcamento
        [ResponseType(typeof(MesOrcamentoDTO))]
        public IHttpActionResult PostMesOrcamento(MesOrcamento mesOrcamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MesOrcamento.Add(mesOrcamento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mesOrcamento.Codigo }, new MesOrcamentoDTO(mesOrcamento));
        }

        // DELETE: api/MesesOrcamento/5
        [ResponseType(typeof(MesOrcamentoDTO))]
        public IHttpActionResult DeleteMesOrcamento(int id)
        {
            MesOrcamento mesOrcamento = db.MesOrcamento.Find(id);
            if (mesOrcamento == null)
            {
                return NotFound();
            }

            MesOrcamentoDTO m = new MesOrcamentoDTO(mesOrcamento);
            db.MesOrcamento.Remove(mesOrcamento);
            db.SaveChanges();

            return Ok(m);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MesOrcamentoExists(int id)
        {
            return db.MesOrcamento.Count(e => e.Codigo == id) > 0;
        }
    }
}
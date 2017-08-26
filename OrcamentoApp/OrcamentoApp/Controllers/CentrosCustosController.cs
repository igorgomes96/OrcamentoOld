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
    public class CentrosCustosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/CentrosCustos
        public IEnumerable<CentroCustoDTO> GetCentrosCusto(int? codSetor = null, int? codEmpresa = null)
        {
            return db.CentroCusto.ToList()
                .Where(x => (codSetor == null || x.SetorCod == codSetor) && (codEmpresa == null || x.EmpresaCod == codEmpresa))
                .Select(x => new CentroCustoDTO(x));
        }

        // GET: api/CentrosCustos/5
        [ResponseType(typeof(CentroCustoDTO))]
        public IHttpActionResult GetCentroCusto(string id)
        {
            CentroCusto centroCusto = db.CentroCusto.Find(id);
            if (centroCusto == null)
            {
                return NotFound();
            }

            return Ok(new CentroCustoDTO(centroCusto));
        }

        // PUT: api/CentrosCustos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCentroCusto(string id, CentroCusto centroCusto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != centroCusto.Codigo)
            {
                return BadRequest();
            }

            db.Entry(centroCusto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentroCustoExists(id))
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

        // POST: api/CentrosCustos
        [ResponseType(typeof(CentroCustoDTO))]
        public IHttpActionResult PostCentroCusto(CentroCusto centroCusto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CentroCusto.Add(centroCusto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CentroCustoExists(centroCusto.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = centroCusto.Codigo }, new CentroCustoDTO(centroCusto));
        }

        // DELETE: api/CentrosCustos/5
        [ResponseType(typeof(CentroCustoDTO))]
        public IHttpActionResult DeleteCentroCusto(string id)
        {
            CentroCusto centroCusto = db.CentroCusto.Find(id);
            if (centroCusto == null)
            {
                return NotFound();
            }

            CentroCustoDTO c = new CentroCustoDTO(centroCusto);

            db.CentroCusto.Remove(centroCusto);
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

        private bool CentroCustoExists(string id)
        {
            return db.CentroCusto.Count(e => e.Codigo == id) > 0;
        }
    }
}
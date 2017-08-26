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
    public class FiliaisController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Filiais
        public IEnumerable<FilialDTO> GetFilial(int? empresaCod = null, string cidadeNome = null)
        {
            return db.Filial.ToList()
                .Where(x => (cidadeNome == null || x.CidadeNome == cidadeNome) && (empresaCod == null || x.EmpresaCod == empresaCod))
                .Select(x => new FilialDTO(x));
        }

        // GET: api/Filiais/5
        [Route("api/Filiais/{empresaCod}/{cidadeNome}")]
        [ResponseType(typeof(FilialDTO))]
        public IHttpActionResult GetFilial(int empresaCod, string cidadeNome)
        {
            Filial filial = db.Filial.Find(empresaCod, cidadeNome);
            if (filial == null)
            {
                return NotFound();
            }

            return Ok(new FilialDTO(filial));
        }

        [Route("api/Filiais/{empresaCod}/{cidadeNome}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFilial(int empresaCod, string cidadeNome, Filial filial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (empresaCod != filial.EmpresaCod || cidadeNome != filial.CidadeNome)
            {
                return BadRequest();
            }

            db.Entry(filial).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilialExists(empresaCod, cidadeNome))
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

        // POST: api/Filiais
        [ResponseType(typeof(FilialDTO))]
        public IHttpActionResult PostFilial(Filial filial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Filial.Add(filial);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FilialExists(filial.EmpresaCod, filial.CidadeNome))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = filial.EmpresaCod }, new FilialDTO(filial));
        }

        // DELETE: api/Filiais/5
        [Route("api/Filiais/{empresaCod}/{cidadeNome}")]
        [ResponseType(typeof(FilialDTO))]
        public IHttpActionResult DeleteFilial(int empresaCod, string cidadeNome)
        {
            Filial filial = db.Filial.Find(empresaCod, cidadeNome);
            if (filial == null)
            {
                return NotFound();
            }

            FilialDTO f = new FilialDTO(filial);
            db.Filial.Remove(filial);
            db.SaveChanges();

            return Ok(f);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilialExists(int empresaCod, string cidadeNome)
        {
            return db.Filial.Count(e => e.EmpresaCod == empresaCod && e.CidadeNome == cidadeNome) > 0;
        }
    }
}
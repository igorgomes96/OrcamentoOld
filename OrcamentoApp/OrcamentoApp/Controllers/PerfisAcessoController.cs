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
    public class PerfisAcessoController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/PerfisAcesso
        public IEnumerable<PerfilAcessoDTO> GetPerfilAcesso()
        {
            return db.PerfilAcesso.ToList().Select(x => new PerfilAcessoDTO(x));
        }

        // GET: api/PerfisAcesso/5
        [ResponseType(typeof(PerfilAcessoDTO))]
        public IHttpActionResult GetPerfilAcesso(string id)
        {
            PerfilAcesso perfilAcesso = db.PerfilAcesso.Find(id);
            if (perfilAcesso == null)
            {
                return NotFound();
            }

            return Ok(new PerfilAcessoDTO(perfilAcesso));
        }

        // PUT: api/PerfisAcesso/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerfilAcesso(string id, PerfilAcesso perfilAcesso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != perfilAcesso.NomePerfil)
            {
                return BadRequest();
            }

            db.Entry(perfilAcesso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilAcessoExists(id))
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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerfilAcessoExists(string id)
        {
            return db.PerfilAcesso.Count(e => e.NomePerfil == id) > 0;
        }
    }
}
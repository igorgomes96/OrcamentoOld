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
    public class EmpresasController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Empresas
        public IEnumerable<EmpresaDTO> GetEmpresa()
        {
            return db.Empresa.ToList().Select(x => new EmpresaDTO(x));
        }

        // GET: api/Empresas/5
        [ResponseType(typeof(EmpresaDTO))]
        public IHttpActionResult GetEmpresa(int id)
        {
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(new EmpresaDTO(empresa));
        }

        // PUT: api/Empresas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmpresa(int id, Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empresa.Codigo)
            {
                return BadRequest();
            }

            db.Entry(empresa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
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

        // POST: api/Empresas
        [ResponseType(typeof(EmpresaDTO))]
        public IHttpActionResult PostEmpresa(Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Empresa.Add(empresa);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmpresaExists(empresa.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = empresa.Codigo }, new EmpresaDTO(empresa));
        }

        // DELETE: api/Empresas/5
        [ResponseType(typeof(EmpresaDTO))]
        public IHttpActionResult DeleteEmpresa(int id)
        {
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return NotFound();
            }

            EmpresaDTO e = new EmpresaDTO(empresa);
            db.Empresa.Remove(empresa);
            db.SaveChanges();

            return Ok(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpresaExists(int id)
        {
            return db.Empresa.Count(e => e.Codigo == id) > 0;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OrcamentoAPI.Models;
using OrcamentoAPI.DTO;

namespace OrcamentoAPI.Controllers
{
    public class CiclosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Ciclos
        public IEnumerable<CicloDTO> GetCiclo(Nullable<int> statusCod = null)
        {
            return db.Ciclo.ToList().Where(x => statusCod == null || x.StatusCod == statusCod).Select(x => new CicloDTO(x));
        }

        // GET: api/Ciclos/5
        [ResponseType(typeof(CicloDTO))]
        public IHttpActionResult GetCiclo(int id)
        {
            Ciclo ciclo = db.Ciclo.Find(id);
            if (ciclo == null)
            {
                return NotFound();
            }

            return Ok(new CicloDTO(ciclo));
        }

        // PUT: api/Ciclos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCiclo(int id, Ciclo ciclo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ciclo.Codigo)
            {
                return BadRequest();
            }

            db.Entry(ciclo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CicloExists(id))
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

        // POST: api/Ciclos
        [ResponseType(typeof(CicloDTO))]
        public IHttpActionResult PostCiclo(Ciclo ciclo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime data = new DateTime(ciclo.DataInicio.Year, ciclo.DataInicio.Month, 1);
            DateTime fim =  new DateTime(ciclo.DataFim.Year, ciclo.DataFim.Month, 1);

            while (data <= fim)
            {
                db.MesOrcamento.Add(new MesOrcamento {

                });
            }

            db.Entry(ciclo).State = EntityState.Added;
            db.SaveChanges();
            db.Entry(ciclo).State = EntityState.Detached;

            return CreatedAtRoute("DefaultApi", new { id = ciclo.Codigo }, new CicloDTO(db.Ciclo.Find(ciclo.Codigo)));
        }

        // DELETE: api/Ciclos/5
        [ResponseType(typeof(Ciclo))]
        public IHttpActionResult DeleteCiclo(int id)
        {
            Ciclo ciclo = db.Ciclo.Find(id);
            if (ciclo == null)
            {
                return NotFound();
            }

            CicloDTO c = new CicloDTO(ciclo);
            db.Ciclo.Remove(ciclo);
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

        private bool CicloExists(int id)
        {
            return db.Ciclo.Count(e => e.Codigo == id) > 0;
        }
    }
}
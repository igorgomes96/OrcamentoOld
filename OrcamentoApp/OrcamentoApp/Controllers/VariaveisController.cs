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
    public class VariaveisController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Variaveis
        public IEnumerable<VariaveisDTO> GetVariaveis(int? empresaCod = null, int? cargoCod = null)
        {
            return db.Variaveis.ToList()
                .Where(x => (empresaCod == null || x.EmpresaCod == empresaCod) && (cargoCod == null || x.CargoCod == cargoCod))
                .Select(x => new VariaveisDTO(x));
        }

        // GET: api/Variaveis/5
        [Route("api/Variaveis/{empresaCod}/{cargoCod}/{cargaHoraria}")]
        [ResponseType(typeof(VariaveisDTO))]
        public IHttpActionResult GetVariaveis(int empresaCod, int cargoCod, int cargaHoraria)
        {
            Variaveis variaveis = db.Variaveis.Find(cargaHoraria, empresaCod, cargoCod);
            if (variaveis == null)
            {
                return NotFound();
            }

            return Ok(new VariaveisDTO(variaveis));
        }

        // PUT: api/Variaveis/5
        [Route("api/Variaveis/{empresaCod}/{cargoCod}/{cargaHoraria}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVariaveis(int empresaCod, int cargoCod, int cargaHoraria, Variaveis variaveis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cargaHoraria != variaveis.CargaHoraria || cargoCod != variaveis.CargoCod || empresaCod != variaveis.EmpresaCod)
            {
                return BadRequest();
            }

            db.Entry(variaveis).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariaveisExists(empresaCod, cargoCod, cargaHoraria))
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

        // POST: api/Variaveis
        [ResponseType(typeof(VariaveisDTO))]
        public IHttpActionResult PostVariaveis(Variaveis variaveis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Variaveis.Add(variaveis);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VariaveisExists(variaveis.EmpresaCod, variaveis.CargoCod, variaveis.CargaHoraria))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = variaveis.CargaHoraria }, new VariaveisDTO(variaveis));
        }

        // DELETE: api/Variaveis/5
        [Route("api/Variaveis/{empresaCod}/{cargoCod}/{cargaHoraria}")]
        [ResponseType(typeof(VariaveisDTO))]
        public IHttpActionResult DeleteVariaveis(int empresaCod, int cargoCod, int cargaHoraria)
        {
            Variaveis variaveis = db.Variaveis.Find(cargaHoraria, empresaCod, cargoCod);
            if (variaveis == null)
            {
                return NotFound();
            }

            VariaveisDTO v = new VariaveisDTO(variaveis);
            db.Variaveis.Remove(variaveis);
            db.SaveChanges();

            return Ok(v);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VariaveisExists(int empresaCod, int cargoCod, int cargaHoraria)
        {
            return db.Variaveis.Count(e => e.CargaHoraria == cargaHoraria && e.EmpresaCod == empresaCod && e.CargoCod == cargoCod) > 0;
        }
    }
}
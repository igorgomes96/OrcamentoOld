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
using PagedList;

namespace OrcamentoApp.Controllers
{
    public class SalariosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Salarios
        public IEnumerable<SalarioDTO> GetSalarios(int? cargoCod = null, string cidadeNome = null, int? empresaCod = null)
        {
            return db.Salario.ToList()
                .Where(x => (cargoCod == null || x.CargoCod == cargoCod) && 
                (cidadeNome == null || x.CidadeNome == cidadeNome) && 
                (empresaCod == null || x.EmpresaCod == empresaCod))
                .Select(x => new SalarioDTO(x));
        }

        [Route("api/Salarios/Paged/{pageNumber}/{pageSize}")]
        public IHttpActionResult GetSalariosPaged(int pageNumber, int pageSize, int? cargoCod = null, string cidadeNome = null, int? empresaCod = null, string consultaCargo = null)
        {
            if (consultaCargo != null) consultaCargo = consultaCargo.Trim().ToLower();

            IEnumerable<SalarioDTO> salarios =  db.Salario.ToList()
                .Where(x => (cargoCod == null || x.CargoCod == cargoCod) &&
                (cidadeNome == null || x.CidadeNome == cidadeNome) &&
                (empresaCod == null || x.EmpresaCod == empresaCod) && 
                (consultaCargo == null || x.Variaveis.Cargo.NomeCargo.ToLower().Contains(consultaCargo)))
                .Select(x => new SalarioDTO(x));

            double r = Convert.ToDouble(salarios.Count()) / pageSize;
            int n = Convert.ToInt32(Math.Ceiling(r));

            if (n <= 0) return Ok(new
            {
                Salarios = new List<SalarioDTO>(),
                NumberOfPages = n,
                Page = 0
            });

            if (n < pageNumber) pageNumber = n;
            if (pageNumber <= 0) pageNumber = 1;

            return Ok(new
            {
                Salarios = salarios.ToPagedList(pageNumber, pageSize),
                NumberOfPages = n,
                Page = pageNumber
            });

        }


        // GET: api/Salarios/5
        [ResponseType(typeof(SalarioDTO))]
        [Route("api/Salarios/{cargoCod}/{cargaHoraria}/{empresaCod}/{cidadeNome}")]
        public IHttpActionResult GetSalario(int cargoCod, int cargaHoraria, int empresaCod, string cidadeNome)
        {
            Salario salario = db.Salario.Find(cargoCod, cargaHoraria, empresaCod, cidadeNome);
            if (salario == null)
            {
                return NotFound();
            }

            return Ok(new SalarioDTO(salario));
        }

        // PUT: api/Salarios/5
        [ResponseType(typeof(void))]
        [Route("api/Salarios/{cargoCod}/{cargaHoraria}/{empresaCod}/{cidadeNome}")]
        public IHttpActionResult PutSalario(int cargoCod, int cargaHoraria, int empresaCod, string cidadeNome, Salario salario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cargoCod != salario.CargoCod || salario.CargaHoraria != cargaHoraria || salario.EmpresaCod != empresaCod || salario.CidadeNome != cidadeNome)
            {
                return BadRequest();
            }

            db.Entry(salario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalarioExists(cargoCod, cargaHoraria, empresaCod, cidadeNome))
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

        // POST: api/Salarios
        [ResponseType(typeof(SalarioDTO))]
        public IHttpActionResult PostSalario(Salario salario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Salario.Add(salario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalarioExists(salario.CargoCod, salario.CargaHoraria, salario.EmpresaCod, salario.CidadeNome))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salario.CargoCod }, new SalarioDTO(salario));
        }

        // DELETE: api/Salarios/5
        [ResponseType(typeof(SalarioDTO))]
        [Route("api/Salarios/{cargoCod}/{cargaHoraria}/{empresaCod}/{cidadeNome}")]
        public IHttpActionResult DeleteSalario(int cargoCod, int cargaHoraria, int empresaCod, string cidadeNome)
        {
            Salario salario = db.Salario.Find(cargoCod, cargaHoraria, empresaCod, cidadeNome);
            if (salario == null)
            {
                return NotFound();
            }

            SalarioDTO s = new SalarioDTO(salario);
            db.Salario.Remove(salario);
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

        private bool SalarioExists(int cargoCod, int cargaHoraria, int empresaCod, string cidadeNome)
        {
            return db.Salario.Count(e => e.CargoCod == cargoCod && e.CargaHoraria == cargaHoraria && e.EmpresaCod == empresaCod && e.CidadeNome == cidadeNome) > 0;
        }
    }
}
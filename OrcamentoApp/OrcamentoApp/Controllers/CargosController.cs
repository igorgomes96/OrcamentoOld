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
    public class CargosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Cargos
        public IEnumerable<CargoDTO> GetCargo(string consulta = null)
        {
            if (consulta == null || consulta.Trim() == "")
                return db.Cargo.ToList().Select(x => new CargoDTO(x));
            else
            {
                consulta = consulta.Trim().ToLower();
                return db.Cargo.ToList().Where(x => x.NomeCargo.ToLower().Contains(consulta)).Select(x => new CargoDTO(x));
            }
        }

        [Route("api/Cargos/Paged/{pageNumber}/{pageSize}")]
        public IHttpActionResult GetCargosPaged(int pageNumber, int pageSize, string consulta = null)
        {
            int n = GetNumberOfPages(pageSize, consulta);
            if (n < pageNumber) pageNumber = n;
            if (n <= 0) return Ok();
            IEnumerable<CargoDTO> cargos = new List<CargoDTO>();
            if (consulta == null || consulta.Trim() == "")
                cargos = db.Cargo.ToList().OrderBy(x => x.NomeCargo).ToPagedList(pageNumber, pageSize).Select(x => new CargoDTO(x));
            else
            {
                consulta = consulta.Trim().ToLower();
                cargos = db.Cargo.ToList().Where(x => x.NomeCargo.ToLower().Contains(consulta)).OrderBy(x => x.NomeCargo).ToPagedList(pageNumber, pageSize).Select(x => new CargoDTO(x));
            }
            return Ok(new
            {
                Cargos = cargos,
                NumberOfPages = n
            });
        }

        [Route("api/Cargos/NumberOfPages/{pageSize}")]
        public int GetNumberOfPages(int pageSize, string consulta = null)
        {
            consulta = consulta?.Trim().ToLower();
            double r = Convert.ToDouble(db.Cargo.Where(x => consulta == null || x.NomeCargo.ToLower().Contains(consulta)).Count()) / pageSize;
            return Convert.ToInt32(Math.Ceiling(r));
        }

        [Route("api/Cargos/CHs")]
        public IEnumerable<CHCargoDTO> GetCHsPorCargo(int? empresaCod = null)
        {
            return db.Cargo.ToList().Where(x => x.Variaveis.Where(y => (empresaCod == null || y.EmpresaCod == empresaCod)).Count() > 0).Select(x => new CHCargoDTO(x));
        }

        // GET: api/Cargos/5
        [ResponseType(typeof(CargoDTO))]
        public IHttpActionResult GetCargo(int id)
        {
            Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return NotFound();
            }

            return Ok(new CargoDTO(cargo));
        }

        // PUT: api/Cargos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCargo(int id, Cargo cargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cargo.Codigo)
            {
                return BadRequest();
            }

            db.Entry(cargo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoExists(id))
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

        // POST: api/Cargos
        [ResponseType(typeof(CargoDTO))]
        public IHttpActionResult PostCargo(Cargo cargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cargo.Add(cargo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cargo.Codigo }, new CargoDTO(cargo));
        }

        // DELETE: api/Cargos/5
        [ResponseType(typeof(Cargo))]
        public IHttpActionResult DeleteCargo(int id)
        {
            Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return NotFound();
            }

            CargoDTO c = new CargoDTO(cargo);
            db.Cargo.Remove(cargo);
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

        private bool CargoExists(int id)
        {
            return db.Cargo.Count(e => e.Codigo == id) > 0;
        }
    }
}
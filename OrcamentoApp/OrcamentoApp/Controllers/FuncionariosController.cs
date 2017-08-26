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
    public class FuncionariosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Funcionarios
        public IEnumerable<FuncionarioDTO> GetFuncionarios(string cr="")
        {
            return cr == "" ? db.Funcionario.ToList().Select(x => new FuncionarioDTO(x)) : 
                db.Funcionario.ToList().Where(x => x.CentroCustoCod == cr).Select(x => new FuncionarioDTO(x));
        }

        // GET: api/Funcionarios/5
        [ResponseType(typeof(FuncionarioDTO))]
        public IHttpActionResult GetFuncionario(string id)
        {
            Funcionario funcionario = db.Funcionario.Find(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(new FuncionarioDTO(funcionario));
        }

        // PUT: api/Funcionarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFuncionario(string id, Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != funcionario.Matricula)
            {
                return BadRequest();
            }

            db.Entry(funcionario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                if (!FuncionarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return InternalServerError(e);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Funcionarios
        [ResponseType(typeof(FuncionarioDTO))]
        public IHttpActionResult PostFuncionario(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Funcionario.Add(funcionario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FuncionarioExists(funcionario.Matricula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = funcionario.Matricula }, new FuncionarioDTO(funcionario));
        }

        // DELETE: api/Funcionarios/5
        [ResponseType(typeof(FuncionarioDTO))]
        public IHttpActionResult DeleteFuncionario(string id)
        {
            Funcionario funcionario = db.Funcionario.Find(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            FuncionarioDTO func = new FuncionarioDTO(funcionario);
            db.Funcionario.Remove(funcionario);
            db.SaveChanges();

            return Ok(func);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FuncionarioExists(string id)
        {
            return db.Funcionario.Count(e => e.Matricula == id) > 0;
        }
    }
}
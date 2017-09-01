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
        public IEnumerable<FuncionarioDTO> GetFuncionarios(string cr=null, int? codCiclo = null)
        {
            if (codCiclo == null)
                return db.Funcionario.ToList().Where(x => cr == null || x.CentroCustoCod == cr)
                        .Union(db.Transferencia.ToList()
                        .Where(x => x.Aprovado.HasValue && x.Aprovado.Value && (x.CRDestino == cr || x.CROrigem == cr))
                        .Select(x => x.Funcionario)).Distinct()
                        .Select(x => cr == null ? new FuncionarioDTO(x) : new FuncionarioDTO(x, cr));
            else
            {
                Ciclo c = db.Ciclo.Find(codCiclo);
                if (c != null) { 
                    return db.Funcionario.ToList().Where(x => cr == null || x.CentroCustoCod == cr)
                        .Union(db.Transferencia.ToList()
                        .Where(x => x.Aprovado.HasValue && x.Aprovado.Value && ((x.CRDestino == cr || x.CROrigem == cr) && x.MesOrcamento.CicloCod == codCiclo))
                        .Select(x => x.Funcionario)).Distinct()
                        .Select(x => new FuncionarioDTO(x, c, cr));
                }
                return null;
            }

        }

        // GET: api/Funcionarios/5
        [ResponseType(typeof(FuncionarioDTO))]
        [Route("api/Funcionarios/{matricula}/{cr}/{codCiclo}")]
        public IHttpActionResult GetFuncionarioHistorico(string matricula, string cr, int codCiclo)
        {
            Funcionario funcionario = db.Funcionario.Find(matricula);
            CentroCusto ce = db.CentroCusto.Find(cr);
            Ciclo c = db.Ciclo.Find(codCiclo);
            if (funcionario == null || cr == null || c == null)
            {
                return NotFound();
            }

            return Ok(new FuncionarioDTO(funcionario, c, cr));
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

        [ResponseType(typeof(void))]
        [Route("api/Funcionarios/SaveAll")]
        [HttpPost]
        public IHttpActionResult SaveFuncionarios(IEnumerable<Funcionario> funcionarios)
        {
            funcionarios.ToList().ForEach(x =>
            {
                db.Entry(x).State = EntityState.Modified;
            });

            try
            {
                db.SaveChanges();
            } catch(Exception e)
            {
                throw e;
            }

            return Ok();
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
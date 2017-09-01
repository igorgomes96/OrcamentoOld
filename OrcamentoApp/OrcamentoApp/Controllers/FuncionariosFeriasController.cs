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
    public class FuncionariosFeriasController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/FuncionariosFerias
        public IEnumerable<FuncionarioFeriasDTO> GetFuncionarioFeriasAll(string matricula = null, int? codCiclo = null)
        {
            return db.FuncionarioFerias.ToList()
                .Where(x => (matricula == null || x.MatriculaFuncionario == matricula) && (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo))
                .Select(x => new FuncionarioFeriasDTO(x));
        }

        // GET: api/FuncionariosFerias/5
        [ResponseType(typeof(FuncionarioFeriasDTO))]
        [Route("api/FuncionariosFerias/{matricula}/{codMes}")]
        public IHttpActionResult GetFuncionarioFerias(string matricula, int codMes)
        {
            FuncionarioFerias funcionarioFerias = db.FuncionarioFerias.Find(matricula, codMes);
            if (funcionarioFerias == null)
            {
                return NotFound();
            }

            return Ok(new FuncionarioFeriasDTO(funcionarioFerias));
        }

        [ResponseType(typeof(void))]
        [Route("api/FuncionariosFerias/SaveAll/{codCiclo}")]
        [HttpPost]
        public IHttpActionResult SaveAllFuncionariosFerias(int codCiclo, IEnumerable<FuncionarioFerias> ferias)
        {
            db.Database.ExecuteSqlCommand("delete a from FuncionarioFerias a inner join MesOrcamento b on a.CodMesOrcamento = b.Codigo where b.CicloCod = {0}", codCiclo);

            foreach (FuncionarioFerias f in ferias)
            {
                if (f.CodMesOrcamento > 0) { 
                    if (FuncionarioFeriasExists(f.MatriculaFuncionario, f.CodMesOrcamento))
                    {
                        if (f.QtdaDias <= 0) db.Entry(f).State = EntityState.Deleted;
                        else db.Entry(f).State = EntityState.Modified;
                    } else
                    {
                        db.Entry(f).State = EntityState.Added;
                    }
                }
            }

            try
            {
                db.SaveChanges();
            } catch(Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        // PUT: api/FuncionariosFerias/5
        [ResponseType(typeof(void))]
        [Route("api/FuncionariosFerias/{matricula}/{codMes}")]
        public IHttpActionResult PutFuncionarioFerias(string matricula, int codMes, FuncionarioFerias funcionarioFerias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (matricula != funcionarioFerias.MatriculaFuncionario || codMes != funcionarioFerias.CodMesOrcamento)
            {
                return BadRequest();
            }

            if (!FuncionarioFeriasExists(matricula, codMes))
            {
                return NotFound();
            }

            if (funcionarioFerias.QtdaDias <= 0)
                db.Entry(funcionarioFerias).State = EntityState.Deleted;
            else
                db.Entry(funcionarioFerias).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FuncionariosFerias
        [ResponseType(typeof(FuncionarioFeriasDTO))]
        public IHttpActionResult PostFuncionarioFerias(FuncionarioFerias funcionarioFerias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (funcionarioFerias.QtdaDias <= 0) return Ok();

            db.FuncionarioFerias.Add(funcionarioFerias);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FuncionarioFeriasExists(funcionarioFerias.MatriculaFuncionario, funcionarioFerias.CodMesOrcamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = funcionarioFerias.MatriculaFuncionario }, new FuncionarioFeriasDTO(funcionarioFerias));
        }

        // DELETE: api/FuncionariosFerias/5
        [ResponseType(typeof(FuncionarioFeriasDTO))]
        [Route("api/FuncionariosFerias/{matricula}/{codMes}")]
        public IHttpActionResult DeleteFuncionarioFerias(string matricula, int codMes)
        {
            FuncionarioFerias funcionarioFerias = db.FuncionarioFerias.Find(matricula, codMes);
            if (funcionarioFerias == null)
            {
                return NotFound();
            }

            FuncionarioFeriasDTO f = new FuncionarioFeriasDTO(funcionarioFerias);
            db.FuncionarioFerias.Remove(funcionarioFerias);
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

        private bool FuncionarioFeriasExists(string matricula, int codMes)
        {
            return db.FuncionarioFerias.Count(x => x.CodMesOrcamento == codMes && x.MatriculaFuncionario == matricula) > 0;
        }
    }
}
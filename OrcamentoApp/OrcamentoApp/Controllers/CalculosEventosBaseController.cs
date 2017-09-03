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
using OrcamentoApp.Services;

namespace OrcamentoApp.Controllers
{
    public class CalculosEventosBaseController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/CalculosEventosBase
        public IEnumerable<CalculoEventoBaseDTO> GetCalculoEventoBase(int? codCiclo = null, string matricula = null, string codEvento = null)
        {
            return db.CalculoEventoBase.ToList()
                .Where(x => (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo) && (matricula == null || x.MatriculaFuncionario == matricula) && (codEvento == null || x.CodEvento == codEvento))
                .Select(x => new CalculoEventoBaseDTO(x));
        }

        [ResponseType(typeof(FuncionarioEventosDTO))]
        [Route("api/CalculosEventosBase/PorCiclo/{matricula}/{codCiclo}")]
        public IHttpActionResult GetValoresPorCiclo(string matricula, int codCiclo)
        {
            Funcionario f = db.Funcionario.Find(matricula);
            if (f == null) return NotFound();

            Ciclo c = db.Ciclo.Find(codCiclo);
            if (c == null) return NotFound();

            return Ok(new FuncionarioEventosDTO(f, c));
        }

        [ResponseType(typeof(IEnumerable<FuncionarioEventosDTO>))]
        [Route("api/CalculosEventosBase/PorCiclo/PorCR/{cr}/{codCiclo}")]
        public IHttpActionResult GetValoresPorCicloCR(string cr, int codCiclo)
        {
            CentroCusto c = db.CentroCusto.Find(cr);
            if (c == null) return NotFound();

            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();

            return Ok(c.Funcionario.ToList().Select(x => new FuncionarioEventosDTO(x, ciclo)));
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("api/CalculosEventosBase/Calcula/PorCiclo/PorCR/{cr}/{codCiclo}")]
        public IHttpActionResult CalculaBasePorCR(string cr, int codCiclo)
        {
            CentroCusto c = db.CentroCusto.Find(cr);
            if (c == null) return NotFound();

            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();

            db.Database.ExecuteSqlCommand("delete a from CalculoEventoBase a inner join MesOrcamento b on a.CodMesOrcamento = b.Codigo inner join Funcionario c on a.MatriculaFuncionario = c.Matricula where b.CicloCod = {0} and c.CentroCustoCod = {1}", codCiclo, cr);
            db.Database.ExecuteSqlCommand("insert into CalculoEventoBase (CodEvento, MatriculaFuncionario, CodMesOrcamento, Valor) select CodEvento, MatriculaFuncionario, CodMesOrcamento, Valor from ValoresAbertosBase a inner join MesOrcamento b on a.CodMesOrcamento = b.Codigo inner join Funcionario c on a.MatriculaFuncionario = c.Matricula where b.CicloCod = {0} and c.CentroCustoCod = {1}", codCiclo, cr);

            c.Funcionario.ToList().ForEach(x =>
            {
                CalculosBaseService.CalculaFuncionarioCiclo(x, ciclo).ToList()
                    .ForEach(y => db.Entry(y).State = EntityState.Added);
            });

            try
            {
                db.SaveChanges();
            } catch(Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        // PUT: api/CalculosEventosBase/5
        [ResponseType(typeof(void))]
        [Route("api/CalculosEventosBase/{evento}/{matricula}/{mes}")]
        public IHttpActionResult PutCalculoEventoBase(string evento, string matricula, int mes, CalculoEventoBase calculoEventoBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (evento != calculoEventoBase.CodEvento || matricula != calculoEventoBase.MatriculaFuncionario || mes != calculoEventoBase.CodMesOrcamento)
            {
                return BadRequest();
            }

            db.Entry(calculoEventoBase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculoEventoBaseExists(evento, matricula, mes))
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

        // POST: api/CalculosEventosBase
        [ResponseType(typeof(CalculoEventoBaseDTO))]
        public IHttpActionResult PostCalculoEventoBase(CalculoEventoBase calculoEventoBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CalculoEventoBase.Add(calculoEventoBase);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CalculoEventoBaseExists(calculoEventoBase.CodEvento, calculoEventoBase.MatriculaFuncionario, calculoEventoBase.CodMesOrcamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = calculoEventoBase.CodEvento }, new CalculoEventoBaseDTO(calculoEventoBase));
        }

        // DELETE: api/CalculosEventosBase/5
        [ResponseType(typeof(CalculoEventoBaseDTO))]
        [Route("api/CalculosEventosBase/{evento}/{matricula}/{mes}")]
        public IHttpActionResult DeleteCalculoEventoBase(string evento, string matricula, int mes)
        {
            CalculoEventoBase calculoEventoBase = db.CalculoEventoBase.Find(evento, matricula, mes);
            if (calculoEventoBase == null)
            {
                return NotFound();
            }

            CalculoEventoBaseDTO c = new CalculoEventoBaseDTO(calculoEventoBase);
            db.CalculoEventoBase.Remove(calculoEventoBase);
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

        private bool CalculoEventoBaseExists(string evento, string matricula, int mes)
        {
            return db.CalculoEventoBase.Count(e => e.CodEvento == evento && e.MatriculaFuncionario == matricula && e.CodMesOrcamento == mes) > 0;
        }
    }
}
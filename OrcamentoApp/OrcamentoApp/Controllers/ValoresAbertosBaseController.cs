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
    public class ValoresAbertosBaseController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/ValoresAbertosBase
        public IEnumerable<ValoresAbertosBaseDTO> GetValoresAbertosBase(string codEvento = null, string matricula = null, int? codCiclo = null)
        {
            if (matricula != null && codCiclo != null)
            {
                IEnumerable<ValoresAbertosBaseDTO> lista = new HashSet<ValoresAbertosBaseDTO>();
                Ciclo ciclo = db.Ciclo.Find(codCiclo);
                if (ciclo == null) return null;

                db.ValoresAbertosBase
                    .Where(x => x.MatriculaFuncionario == matricula && x.MesOrcamento.CicloCod == codCiclo)
                    .Select(x => x.CodEvento).Distinct().ToList().ForEach(x =>
                {
                    foreach (MesOrcamento m in ciclo.MesesOrcamento)
                    {
                        ValoresAbertosBase v = db.ValoresAbertosBase.Find(x, m.Codigo, matricula);

                        if (v == null)
                        {
                            ((HashSet<ValoresAbertosBaseDTO>)lista).Add(new ValoresAbertosBaseDTO
                            {
                                CodEvento = x,
                                CodMesOrcamento = m.Codigo,
                                MatriculaFuncionario = matricula,
                                Valor = 0
                            });
                        }
                        else
                            ((HashSet<ValoresAbertosBaseDTO>)lista).Add(new ValoresAbertosBaseDTO(v));
                    }
                });
                
                return lista;
            }
            return db.ValoresAbertosBase.ToList()
                .Where(x => (codEvento == null || x.CodEvento == codEvento) && (matricula == null || x.MatriculaFuncionario == matricula) && (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo))
                .Select(x => new ValoresAbertosBaseDTO(x));
        }

        // GET: api/ValoresAbertosBase/5
        /*[ResponseType(typeof(ValoresAbertosBaseDTO))]
        public IHttpActionResult GetValoresAbertosBase(string id)
        {
            ValoresAbertosBase valoresAbertosBase = db.ValoresAbertosBase.Find(id);
            if (valoresAbertosBase == null)
            {
                return NotFound();
            }

            return Ok(valoresAbertosBase);
        }*/

        // PUT: api/ValoresAbertosBase/5
        [ResponseType(typeof(void))]
        [Route("api/ValoresAbertosBase/{codEvento}/{mes}/{matricula}")]
        public IHttpActionResult PutValoresAbertosBase(string codEvento, int mes, string matricula, ValoresAbertosBase valoresAbertosBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (codEvento != valoresAbertosBase.CodEvento || mes != valoresAbertosBase.CodMesOrcamento || matricula != valoresAbertosBase.MatriculaFuncionario)
            {
                return BadRequest();
            }

            db.Entry(valoresAbertosBase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValoresAbertosBaseExists(codEvento, mes, matricula))
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

        // POST: api/ValoresAbertosBase
        [ResponseType(typeof(ValoresAbertosBaseDTO))]
        public IHttpActionResult PostValoresAbertosBase(ValoresAbertosBase valoresAbertosBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ValoresAbertosBase.Add(valoresAbertosBase);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ValoresAbertosBaseExists(valoresAbertosBase.CodEvento, valoresAbertosBase.CodMesOrcamento, valoresAbertosBase.MatriculaFuncionario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = valoresAbertosBase.CodEvento }, new ValoresAbertosBaseDTO(valoresAbertosBase));
        }

        // DELETE: api/ValoresAbertosBase/5
        [ResponseType(typeof(ValoresAbertosBaseDTO))]
        [Route("api/ValoresAbertosBase/{codEvento}/{mes}/{matricula}")]
        public IHttpActionResult DeleteValoresAbertosBase(string codEvento, int mes, string matricula)
        {
            ValoresAbertosBase valoresAbertosBase = db.ValoresAbertosBase.Find(codEvento, mes, matricula);
            if (valoresAbertosBase == null)
            {
                return NotFound();
            }
            ValoresAbertosBaseDTO v = new ValoresAbertosBaseDTO(valoresAbertosBase);
            db.ValoresAbertosBase.Remove(valoresAbertosBase);
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

        private bool ValoresAbertosBaseExists(string codEvento, int mes, string matricula)
        {
            return db.ValoresAbertosBase.Count(e => e.CodEvento == codEvento && e.CodMesOrcamento == mes && e.MatriculaFuncionario == matricula) > 0;
        }
    }
}
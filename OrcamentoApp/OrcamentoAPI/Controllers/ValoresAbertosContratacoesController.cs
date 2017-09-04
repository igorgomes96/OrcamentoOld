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
using OrcamentoAPI.Models;
using OrcamentoAPI.DTO;
using System.Data.Entity.Migrations;

namespace OrcamentoAPI.Controllers
{
    public class ValoresAbertosContratacoesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/ValoresAbertosContratacoes
        public IEnumerable<ValoresAbertosContratacaoDTO> GetValoresAbertosContratacao(string codEvento = null, int? codContratacao = null, int? codCiclo = null)
        {
            if (codContratacao != null && codCiclo != null)
            {
                IEnumerable<ValoresAbertosContratacaoDTO> lista = new HashSet<ValoresAbertosContratacaoDTO>();
                Ciclo ciclo = db.Ciclo.Find(codCiclo);
                if (ciclo == null) return null;

                db.ValoresAbertosContratacao
                    .Where(x => x.CodContratacao == codContratacao && x.MesOrcamento.CicloCod == codCiclo)
                    .Select(x => x.CodEvento).Distinct().ToList().ForEach(x =>
                    {
                        foreach (MesOrcamento m in ciclo.MesesOrcamento)
                        {
                            ValoresAbertosContratacao v = db.ValoresAbertosContratacao.Find(x, m.Codigo, codContratacao);

                            if (v == null)
                            {
                                ((HashSet<ValoresAbertosContratacaoDTO>)lista).Add(new ValoresAbertosContratacaoDTO
                                {
                                    CodEvento = x,
                                    CodMesOrcamento = m.Codigo,
                                    CodContratacao = codContratacao.Value,
                                    Valor = 0
                                });
                            }
                            else
                                ((HashSet<ValoresAbertosContratacaoDTO>)lista).Add(new ValoresAbertosContratacaoDTO(v));
                        }
                    });

                return lista;
            }
            return db.ValoresAbertosContratacao.ToList()
                .Where(x => (codEvento == null || x.CodEvento == codEvento) && (codContratacao == null || x.CodContratacao == codContratacao) && (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo))
                .Select(x => new ValoresAbertosContratacaoDTO(x));
        }

        [HttpPost]
        [Route("api/ValoresAbertosContratacoes/SaveAll")]
        [ResponseType(typeof(void))]
        public IHttpActionResult SaveAll(IEnumerable<ValoresAbertosContratacao> contratacoes)
        {
            foreach(ValoresAbertosContratacao v in contratacoes)
            {
                if (v.Valor == 0)
                {
                    if (ValoresAbertosContratacaoExists(v.CodEvento, v.CodMesOrcamento, v.CodContratacao))
                        db.Entry(v).State = EntityState.Deleted;
                } else
                {
                    db.ValoresAbertosContratacao.AddOrUpdate(v);
                }
            }

            try
            {
                db.SaveChanges();
            } catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        // PUT: api/ValoresAbertosContratacoes/5
        [ResponseType(typeof(void))]
        [Route("api/ValoresAbertosContratacoes/{codEvento}/{mesOrcamento}/{codContratacao}")]
        public IHttpActionResult PutValoresAbertosContratacao(string codEvento, int mesOrcamento, int codContratacao, ValoresAbertosContratacao valoresAbertosContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (codEvento != valoresAbertosContratacao.CodEvento || mesOrcamento != valoresAbertosContratacao.CodMesOrcamento || codContratacao != valoresAbertosContratacao.CodContratacao)
            {
                return BadRequest();
            }

            db.Entry(valoresAbertosContratacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValoresAbertosContratacaoExists(codEvento, mesOrcamento, codContratacao))
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

        // POST: api/ValoresAbertosContratacoes
        [ResponseType(typeof(ValoresAbertosContratacaoDTO))]
        public IHttpActionResult PostValoresAbertosContratacao(ValoresAbertosContratacao valoresAbertosContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ValoresAbertosContratacao.Add(valoresAbertosContratacao);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ValoresAbertosContratacaoExists(valoresAbertosContratacao.CodEvento, valoresAbertosContratacao.CodMesOrcamento, valoresAbertosContratacao.CodContratacao))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = valoresAbertosContratacao.CodEvento }, new ValoresAbertosContratacaoDTO(valoresAbertosContratacao));
        }

        // DELETE: api/ValoresAbertosContratacoes/5
        [ResponseType(typeof(ValoresAbertosContratacaoDTO))]
        [Route("api/ValoresAbertosContratacoes/{codEvento}/{mesOrcamento}/{codContratacao}")]
        public IHttpActionResult DeleteValoresAbertosContratacao(string codEvento, int mesOrcamento, int codContratacao)
        {
            ValoresAbertosContratacao valoresAbertosContratacao = db.ValoresAbertosContratacao.Find(codEvento, mesOrcamento, codContratacao);
            if (valoresAbertosContratacao == null)
            {
                return NotFound();
            }

            ValoresAbertosContratacaoDTO v = new ValoresAbertosContratacaoDTO(valoresAbertosContratacao);
            db.ValoresAbertosContratacao.Remove(valoresAbertosContratacao);
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

        private bool ValoresAbertosContratacaoExists(string codEvento, int mesOrcamento, int codContratacao)
        {
            return db.ValoresAbertosContratacao.Count(e => e.CodEvento == codEvento && e.CodMesOrcamento == mesOrcamento && e.CodContratacao == codContratacao) > 0;
        }
    }
}
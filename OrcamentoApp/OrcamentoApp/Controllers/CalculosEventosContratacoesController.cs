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
    public class CalculosEventosContratacoesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/CalculosEventosContratacoes
        public IEnumerable<CalculoEventoContratacaoDTO> GetCalculoEventoContratacao(int? codCiclo = null, int? contratacao = null, string codEvento = null)
        {
            return db.CalculoEventoContratacao.ToList()
                .Where(x => (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo) && (contratacao == null || x.CodContratacao == contratacao) && (codEvento == null || x.CodEvento == codEvento))
                .Select(x => new CalculoEventoContratacaoDTO(x));
        }

        [ResponseType(typeof(ContratacaoEventosDTO))]
        [Route("api/CalculosEventosContratacoes/PorCiclo/{codContratacao}/{codCiclo}")]
        public IHttpActionResult GetValoresPorCiclo(int codContratacao, int codCiclo)
        {
            Contratacao cont = db.Contratacao.Find(codContratacao);
            if (cont == null) return NotFound();

            Ciclo c = db.Ciclo.Find(codCiclo);
            if (c == null) return NotFound();

            return Ok(new ContratacaoEventosDTO(cont, c));
        }

        [ResponseType(typeof(IEnumerable<ContratacaoEventosDTO>))]
        [Route("api/CalculosEventosContratacoes/PorCiclo/PorCR/{cr}/{codCiclo}")]
        public IHttpActionResult GetValoresPorCicloCR(string cr, int codCiclo)
        {
            CentroCusto c = db.CentroCusto.Find(cr);
            if (c == null) return NotFound();

            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();

            return Ok(c.Contratacoes.ToList().Select(x => new ContratacaoEventosDTO(x, ciclo)));
        }


        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("api/CalculosEventosContratacoes/Calcula/PorCiclo/PorCR/{cr}/{codCiclo}")]
        public IHttpActionResult CalculaContratacoesPorCR(string cr, int codCiclo)
        {
            CentroCusto c = db.CentroCusto.Find(cr);
            if (c == null) return NotFound();

            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();

            db.Database.ExecuteSqlCommand("delete a from CalculoEventoContratacao a inner join MesOrcamento b on a.CodMesOrcamento = b.Codigo inner join Contratacao c on a.CodContratacao = c.Codigo where b.CicloCod = {0} and c.CentroCustoCod = {1}", codCiclo, cr);
            db.Database.ExecuteSqlCommand("insert into CalculoEventoContratacao (CodEvento, CodContratacao, CodMesOrcamento, Valor) select CodEvento, CodContratacao, CodMesOrcamento, Valor from ValoresAbertosContratacao a inner join MesOrcamento b on a.CodMesOrcamento = b.Codigo inner join Contratacao c on a.CodContratacao = c.Codigo where b.CicloCod = {0} and c.CentroCustoCod = {1}", codCiclo, cr);

            c.Contratacoes.ToList().ForEach(x =>
            {
                CalculosContratacaoService.CalculaContratacaoCiclo(x, ciclo).ToList()
                    .ForEach(y => db.Entry(y).State = EntityState.Added);
            });

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }


        // PUT: api/CalculosEventosContratacoes/5
        [ResponseType(typeof(void))]
        [Route("api/CalculosEventosContratacoes/{contratacao}/{evento}/{mes}")]
        public IHttpActionResult PutCalculoEventoContratacao(int contratacao, string evento, int mes, CalculoEventoContratacao calculoEventoContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (contratacao != calculoEventoContratacao.CodContratacao || evento != calculoEventoContratacao.CodEvento || mes != calculoEventoContratacao.CodMesOrcamento)
            {
                return BadRequest();
            }

            db.Entry(calculoEventoContratacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculoEventoContratacaoExists(contratacao, evento, mes))
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

        // POST: api/CalculosEventosContratacoes
        [ResponseType(typeof(CalculoEventoContratacaoDTO))]
        public IHttpActionResult PostCalculoEventoContratacao(CalculoEventoContratacao calculoEventoContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CalculoEventoContratacao.Add(calculoEventoContratacao);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CalculoEventoContratacaoExists(calculoEventoContratacao.CodContratacao, calculoEventoContratacao.CodEvento, calculoEventoContratacao.CodMesOrcamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = calculoEventoContratacao.CodContratacao }, calculoEventoContratacao);
        }

        // DELETE: api/CalculosEventosContratacoes/5
        [ResponseType(typeof(CalculoEventoContratacaoDTO))]
        [Route("api/CalculosEventosContratacoes/{contratacao}/{evento}/{mes}")]
        public IHttpActionResult DeleteCalculoEventoContratacao(int contratacao, string evento, int mes)
        {
            CalculoEventoContratacao calculoEventoContratacao = db.CalculoEventoContratacao.Find(contratacao, evento, mes);
            if (calculoEventoContratacao == null)
            {
                return NotFound();
            }

            CalculoEventoContratacaoDTO c = new CalculoEventoContratacaoDTO(calculoEventoContratacao);
            db.CalculoEventoContratacao.Remove(calculoEventoContratacao);
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

        private bool CalculoEventoContratacaoExists(int contratacao, string evento, int mes)
        {
            return db.CalculoEventoContratacao.Count(e => e.CodContratacao == contratacao && e.CodEvento == evento && e.CodMesOrcamento == mes) > 0;
        }
    }
}
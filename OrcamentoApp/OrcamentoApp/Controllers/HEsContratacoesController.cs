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
    public class HEsContratacoesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/HEsContratacoes
        public IEnumerable<HEContratacaoDTO> GetHEContratacao(int? percHoras = null, int? codContratacao = null, int? codCiclo = null)
        {
            return db.HEContratacao.ToList()
                .Where(x => (percHoras == null || x.PercentualHoras == percHoras) && (codContratacao == null || x.ContratacaoCod == codContratacao) || (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo))
                .Select(x => new HEContratacaoDTO(x));
        }

        [ResponseType(typeof(ContratacaoHEsDTO))]
        [Route("api/HEsContratacoes/ContratacaoHE/{codContratacao}/{codCiclo}")]
        public IHttpActionResult GetFuncionarioHEs(int codContratacao, int codCiclo)
        {
            Contratacao con = db.Contratacao.Find(codContratacao);

            if (con == null) return NotFound();

            Ciclo c = db.Ciclo.Find(codCiclo);

            if (c == null) return NotFound();

            return Ok(new ContratacaoHEsDTO(con, c));
        }

        // PUT: api/HEsContratacoes/5
        [ResponseType(typeof(void))]
        [Route("api/HEsContratacoes/{codContratacao}/{percentual}/{mesOrcamento}")]
        public IHttpActionResult PutHEContratacao(int codContratacao, int percentual, int mesOrcamento, HEContratacao HEContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (codContratacao != HEContratacao.ContratacaoCod || percentual != HEContratacao.PercentualHoras || mesOrcamento != HEContratacao.CodMesOrcamento)
            {
                return BadRequest();
            }

            db.Entry(HEContratacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HEContratacaoExists(codContratacao, percentual, mesOrcamento))
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

        // POST: api/HEsContratacoes
        [ResponseType(typeof(HEContratacaoDTO))]
        public IHttpActionResult PostHEContratacao(HEContratacao HEContratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HEContratacao.Add(HEContratacao);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HEContratacaoExists(HEContratacao.ContratacaoCod, HEContratacao.PercentualHoras, HEContratacao.CodMesOrcamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = HEContratacao.ContratacaoCod }, new HEContratacaoDTO(HEContratacao));
        }

        // DELETE: api/HEsContratacoes/5
        [ResponseType(typeof(HEContratacaoDTO))]
        [Route("api/HEsContratacoes/{codContratacao}/{percentual}/{mesOrcamento}")]
        public IHttpActionResult DeleteHEContratacao(int codContratacao, int percentual, int mesOrcamento)
        {
            HEContratacao HEContratacao = db.HEContratacao.Find(codContratacao, percentual, mesOrcamento);
            if (HEContratacao == null)
            {
                return NotFound();
            }

            HEContratacaoDTO h = new HEContratacaoDTO(HEContratacao);

            db.HEContratacao.Remove(HEContratacao);
            db.SaveChanges();

            return Ok(h);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HEContratacaoExists(int codContratacao, int percentual, int mesOrcamento)
        {
            return db.HEContratacao.Count(e => e.ContratacaoCod == codContratacao && e.PercentualHoras == percentual && e.CodMesOrcamento == mesOrcamento) > 0;
        }
    }
}
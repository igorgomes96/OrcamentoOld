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
using System.Data.Entity.Migrations;

namespace OrcamentoApp.Controllers
{
    public class AdNoturnosContratacoesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/AdNoturnosBase
        public IEnumerable<AdNoturnoContratacaoDTO> GetAdNoturno(int? percHoras = null, int? contratacao = null, int? codCiclo = null)
        {
            return db.AdNoturnoContratacao.ToList()
                .Where(x => (percHoras == null || x.PercentualHoras == percHoras) && (contratacao == null || x.CodContratacao == contratacao) || (codCiclo == null || x.MesOrcamento.CicloCod == codCiclo))
                .Select(x => new AdNoturnoContratacaoDTO(x));
        }

        [ResponseType(typeof(ContratacaoHNsDTO))]
        [Route("api/AdNoturnosContratacoes/ContratacaoHN/{codContratacao}/{codCiclo}")]
        public IHttpActionResult GetFuncionarioHEs(int codContratacao, int codCiclo)
        {
            Contratacao con = db.Contratacao.Find(codContratacao);

            if (con == null) return NotFound();

            Ciclo c = db.Ciclo.Find(codCiclo);

            if (c == null) return NotFound();

            return Ok(new ContratacaoHNsDTO(con, c));
        }

        [ResponseType(typeof(void))]
        [Route("api/AdNoturnosContratacoes/SaveAll")]
        [HttpPost]
        public IHttpActionResult SaveAllAdNoturnos(IEnumerable<AdNoturnoContratacao> contratacoes)
        {

            foreach (AdNoturnoContratacao cont in contratacoes)
            {
                if (cont.QtdaHoras == 0)
                {
                    if (AdNoturnoExists(cont.CodContratacao, cont.CodMesOrcamento, cont.PercentualHoras))
                        db.AdNoturnoContratacao.Remove(cont);
                }
                else
                {
                    db.AdNoturnoContratacao.AddOrUpdate(cont);
                }
            }

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

        // PUT: api/AdNoturnosBase/5
        [ResponseType(typeof(void))]
        [Route("api/AdNoturnosContratacoes/{codContratacao}/{percentual}/{mesOrcamento}")]
        public IHttpActionResult PutAdNoturno(int codContratacao, int mesOrcamento, int percentual, AdNoturnoContratacao adNoturno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (codContratacao != adNoturno.CodContratacao || percentual != adNoturno.PercentualHoras || mesOrcamento != adNoturno.CodMesOrcamento)
            {
                return BadRequest();
            }

            db.Entry(adNoturno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdNoturnoExists(codContratacao, mesOrcamento, percentual))
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

        // POST: api/AdNoturnosBase
        [ResponseType(typeof(AdNoturnoContratacaoDTO))]
        public IHttpActionResult PostAdNoturno(AdNoturnoContratacao adNoturno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdNoturnoContratacao.Add(adNoturno);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdNoturnoExists(adNoturno.CodContratacao, adNoturno.CodMesOrcamento, adNoturno.PercentualHoras))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = adNoturno.CodContratacao }, new AdNoturnoContratacaoDTO(adNoturno));
        }

        // DELETE: api/AdNoturnosBase/5
        [ResponseType(typeof(AdNoturnoContratacaoDTO))]
        [Route("api/AdNoturnosContratacoes/{codContratacao}/{percentual}/{mesOrcamento}")]
        public IHttpActionResult DeleteAdNoturno(int codContratacao, int mesOrcamento, int percentual)
        {
            AdNoturnoContratacao adNoturno = db.AdNoturnoContratacao.Find(codContratacao, mesOrcamento, percentual);
            if (adNoturno == null)
            {
                return NotFound();
            }

            AdNoturnoContratacaoDTO a = new AdNoturnoContratacaoDTO(adNoturno);

            db.AdNoturnoContratacao.Remove(adNoturno);
            db.SaveChanges();

            return Ok(a);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdNoturnoExists(int codContratacao, int mesOrcamento, int percentual)
        {
            return db.AdNoturnoContratacao.Count(e => e.CodContratacao == codContratacao && e.PercentualHoras == percentual && e.CodMesOrcamento == mesOrcamento) > 0;
        }
    }
}
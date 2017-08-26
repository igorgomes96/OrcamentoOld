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
    public class ContratacoesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Contratacoes
        public IEnumerable<ContratacaoDTO> GetContratacao(Nullable<int> codCiclo = null, string codCentroCusto = null)
        {
            return db.Contratacao.ToList()
                .Where(x => (codCiclo == null || x.CicloCod == codCiclo) && (codCentroCusto == null || x.CentroCustoCod == codCentroCusto))
                .Select(x => new ContratacaoDTO(x));
        }

        // GET: api/Contratacoes/5
        [ResponseType(typeof(ContratacaoDTO))]
        public IHttpActionResult GetContratacao(int id)
        {
            Contratacao contratacao = db.Contratacao.Find(id);
            if (contratacao == null)
            {
                return NotFound();
            }

            return Ok(new ContratacaoDTO(contratacao));
        }

        // PUT: api/Contratacoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContratacao(int id, Contratacao contratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contratacao.Codigo)
            {
                return BadRequest();
            }

            db.Entry(contratacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratacaoExists(id))
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

        // POST: api/Contratacoes
        [ResponseType(typeof(ContratacaoDTO))]
        public IHttpActionResult PostContratacao(Contratacao contratacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contratacao.Add(contratacao);
            try {
                db.SaveChanges();
            } catch (Exception e)
            {
                return InternalServerError(new Exception("Erro ao salvar contratação! " + e.Message));
            }

            return CreatedAtRoute("DefaultApi", new { id = contratacao.Codigo }, new ContratacaoDTO(contratacao));
        }

        // DELETE: api/Contratacoes/5
        [ResponseType(typeof(ContratacaoDTO))]
        public IHttpActionResult DeleteContratacao(int id)
        {
            Contratacao contratacao = db.Contratacao.Find(id);
            if (contratacao == null)
            {
                return NotFound();
            }

            ContratacaoDTO c = new ContratacaoDTO(contratacao);

            contratacao.ContratacaoMeses
                .Select(x => new { ContrCod = x.ContratacaoCod, MesCod = x.MesOrcamentoCod }).ToList()
                .ForEach(x => contratacao.ContratacaoMeses.Remove(db.ContratacaoMes.Find(x.ContrCod, x.MesCod)));

            db.Contratacao.Remove(contratacao);
            try { 
                db.SaveChanges();
            } catch(Exception e)
            {
                return InternalServerError(e);
            }

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

        private bool ContratacaoExists(int id)
        {
            return db.Contratacao.Count(e => e.Codigo == id) > 0;
        }
    }
}
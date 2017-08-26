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
    public class UsuariosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Usuarios
        public IEnumerable<UsuarioDTO> GetUsuario()
        {
            return db.Usuario.ToList().Select(x => new UsuarioDTO(x));
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(UsuarioDTO))]
        public IHttpActionResult GetUsuario(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(new UsuarioDTO(usuario));
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(string id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Login)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [ResponseType(typeof(UsuarioDTO))]
        public HttpResponseMessage PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.Usuario.Add(usuario);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                if (UsuarioExists(usuario.Login))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Usuário já cadastrado!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro a salvar informações");
                }
            }

            return Request.CreateResponse(HttpStatusCode.Created, "Usuário adicionado com sucesso!");
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            UsuarioDTO u = new UsuarioDTO(usuario);
            db.Usuario.Remove(usuario);
            db.SaveChanges();

            return Ok(u);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(string id)
        {
            return db.Usuario.Count(e => e.Login == id) > 0;
        }
    }
}
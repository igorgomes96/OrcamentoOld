using OrcamentoApp.Excel;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using OrcamentoApp.Filters;
using OrcamentoApp.Models;

namespace OrcamentoApp.Controllers
{
    [AuthenticationFilter]
    //[Authorize(Roles = "Administrador, Gestor de CR")]
    public class ImportacoesController : ApiController
    {
        private Contexto db = new Contexto();

        [HttpPost]
        [Route("api/Importacoes/Upload")]
        public async Task<HttpResponseMessage> Upload()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
                
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (MultipartFileData file in provider.FileData)
            {
                try
                {
                    /*if (!int.TryParse(provider.FormData.Get("mes"), out int idMesRateio))
                        return Request.CreateResponse(HttpStatusCode.BadRequest);*/

                    ImportacaoOleDB imp = new ImportacaoOleDB(file.LocalFileName, User);
                    imp.ExecutarImportacao();

                    db.Importacao.Add(new Importacao
                    {
                        DataHora = DateTime.Now,
                        LoginUsuario = User.Identity.Name,
                        NomeArquivo = file.Headers.ContentDisposition.FileName.Replace("\"", ""),
                        Arquivo = File.ReadAllBytes(file.LocalFileName)
                    });
                    db.SaveChanges();
                    //File.Delete(file.LocalFileName);

                }
                catch (Exception e)
                {       
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                } finally
                {
                    File.Delete(file.LocalFileName);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

}


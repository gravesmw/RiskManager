using GravesConsultingLLC.RiskManager.Administration.Common;
using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GravesConsultingLLC.RiskManager.Administration.Controllers
{
    [RoutePrefix("upload")]
    public class UploadController : BaseController
    {
        public UploadController(IRepository SqlRepository) : base(SqlRepository) { }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Upload()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                string RootDir = 
                    HttpContext.Current.Server.MapPath("~/App_Data/Upload");

                var StreamProvider = 
                    new CustomMultipartFormDataStreamProvider(RootDir);

                Request.Content.ReadAsMultipartAsync(StreamProvider).ContinueWith(t =>
                {
                    if (t.IsFaulted || t.IsCanceled){
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }

                    IEnumerable<string> Files = 
                        StreamProvider.FileData.Select(f => f.LocalFileName);

                    foreach(string File in Files)
                    {
                        UploadFile NewFile =
                            new UploadFile(File);

                        NewFile.Track(_Repository);
                    }
                });

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

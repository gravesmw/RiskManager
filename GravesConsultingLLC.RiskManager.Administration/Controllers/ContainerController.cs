using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using GravesConsultingLLC.RiskManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GravesConsultingLLC.RiskManager.Administration.Controllers
{
    [RoutePrefix("container")]
    public class ContainerController : BaseController
    {
        public ContainerController(IRepository SqlRepository) : base(SqlRepository) { }

        [Route("{viewid:int}")]
        [HttpGet]
        public IHttpActionResult GetPossibleContainers(int ViewID)
        {
            return Ok<IEnumerable<string>>(
                Container.GetPossibleContainers(ViewID, _Repository)
            );
        }
    }
}

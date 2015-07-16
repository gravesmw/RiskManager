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
    [RoutePrefix("view")]
    public class ViewController : BaseController
    {
        public ViewController(IRepository SqlRepository) : base(SqlRepository) { }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<IEnumerable<ContainerView>>(
                ContainerView.Get(_Repository)
            );
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Create(ContainerView NewContainerView)
        {
            NewContainerView.Create(_Repository);

            return Created<ContainerView>(
                Request.RequestUri + "/" + NewContainerView.ViewID.ToString(),
                NewContainerView
            );
        }

    }
}

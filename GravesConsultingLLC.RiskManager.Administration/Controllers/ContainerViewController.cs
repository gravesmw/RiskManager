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
    [RoutePrefix("containerview")]
    public class ContainerViewController : BaseController
    {
        public ContainerViewController(IRepository SqlRepository) : base(SqlRepository) { }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<IEnumerable<ContainerView>>(
                ContainerView.Get(_Repository)
            );
        }

        [Route("{viewid:int}")]
        [HttpGet]
        public IHttpActionResult GetHierarchy(int ViewID)
        {
            return Ok<List<ContainerViewHierarchy>>(
                ContainerView.GetHierarchy(ViewID, _Repository)
            );
        }

        [Route("{viewid:int}")]
        [HttpPost]
        public IHttpActionResult AddContainerToView(int ViewID, ContainerViewEntry NewContainer)
        {
            NewContainer.Create(_Repository);

            return Created<ContainerViewEntry>(
                Request.RequestUri + "/" + NewContainer.ContainerViewID.ToString(),
                NewContainer
            );
        }

        [Route("{viewid:int}/container/{containerviewid:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteContainerFromView(int ViewID, int ContainerViewID)
        {
            ContainerViewHierarchy.DeleteContainerViewEntry(
                   ContainerViewID, _Repository
           );
            
            return Ok();
        }
    }
}

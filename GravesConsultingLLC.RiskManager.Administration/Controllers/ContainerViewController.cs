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

        [Route("{viewid:int}")]
        [HttpGet]
        public IHttpActionResult GetHierarchy(int ViewID)
        {
            return Ok<List<Hierarchy>>(
                ContainerView.GetHierarchy(ViewID, _Repository)
            );
        }

        [Route("{viewid:int}")]
        [HttpPost]
        public IHttpActionResult AddContainerToView(int ViewID, ContainerViewEntry NewContainer)
        {
            NewContainer.Create(_Repository);

            return Created<ContainerViewEntry>(
                Request.RequestUri + "/" + NewContainer.NodeID.ToString(),
                NewContainer
            );
        }

        [Route("{viewid:int}")]
        [HttpPut]
        public IHttpActionResult UpdateContainerInView(int ViewID, ContainerViewEntry NewContainer)
        {
            NewContainer.Update(_Repository);

            return Ok();
        }

        [Route("{viewid:int}/container/{containerviewid:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteContainerFromView(int ViewID, int ContainerViewID)
        {
            Hierarchy.DeleteContainerViewEntry(
                   ContainerViewID, _Repository
           );
            
            return Ok();
        }
    }
}

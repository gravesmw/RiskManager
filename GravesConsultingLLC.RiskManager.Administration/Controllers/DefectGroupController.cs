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
    [RoutePrefix("defectgroup")]
    public class DefectGroupController : BaseController
    {
        public DefectGroupController(IRepository SqlRepository) : base(SqlRepository) { }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<IEnumerable<Hierarchy>>(
                DefectGroup.GetHierarchy(_Repository)
            );
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Create(DefectGroup NewDefectGroup)
        {
            NewDefectGroup.Create(_Repository);

            return Created<DefectGroup>(
                Request.RequestUri + "/" + NewDefectGroup.NodeID.ToString(),
                NewDefectGroup
            );
        }

        [Route("{defectgroupid:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int DefectGroupID)
        {
            DefectGroup.Delete(
                   DefectGroupID, _Repository
           );

            return Ok();
        }

        [Route("{defectgroupid:int}")]
        [HttpPut]
        public IHttpActionResult Update(int DefectGroupID, DefectGroup UpdatedDefectGroup)
        {
            UpdatedDefectGroup.Update(_Repository);

            return Ok();
        }
    }
}

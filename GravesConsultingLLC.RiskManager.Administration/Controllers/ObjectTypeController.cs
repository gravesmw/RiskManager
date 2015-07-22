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
     [RoutePrefix("objecttype")]
    public class ObjectTypeController : BaseController
    {
        public ObjectTypeController(IRepository SqlRepository) : base(SqlRepository) { }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<IEnumerable<Hierarchy>>(
                ObjectType.GetHierarchy(_Repository)
            );
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Create(ObjectType NewObjectType)
        {
            NewObjectType.Create(_Repository);

            return Created<ObjectType>(
                Request.RequestUri + "/" + NewObjectType.NodeID.ToString(),
                NewObjectType
            );
        }


        [Route("{objecttypeid:int}")]
        [HttpPut]
        public IHttpActionResult Update(int ObjectTypeID, ObjectType UpdatedObjectType)
        {
            UpdatedObjectType.Update(_Repository);

            return Ok();
        }

        [Route("{objecttypeid:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int ObjectTypeID)
        {
            ObjectType.Delete(
                   ObjectTypeID, _Repository
           );

            return Ok();
        }
    }
}

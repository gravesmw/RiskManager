using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GravesConsultingLLC.RiskManager.Administration.Controllers
{
    public class BaseController : ApiController
    {
        protected IRepository _Repository;

        public BaseController(IRepository SqlRepository)
        {
            _Repository = SqlRepository;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_Repository != null)
                {
                    _Repository.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}

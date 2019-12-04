using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FusionLOS.Controllers
{
    public class UnderwritingCheckController : ApiController
    {
        public bool Get([FromBody]string customerId)
        {
            return customerId?.Length > 0 ? true : false;
        }
    }
}
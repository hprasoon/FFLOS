using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FusionLOS.Models;

namespace FusionLOS.Controllers
{
    public class LoanController : ApiController
    {
        public IEnumerable<Lead> Get()
        {
            return new Lead().GetLead();
        }

        public void Post([FromBody]Lead newloan)
        {
            new Lead().Add(newloan);
        }
    }
}

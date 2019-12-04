using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FusionLOS.Controllers
{
    public class MailController : ApiController
    {
        public void Post([FromBody]string mailId)
        {
            //new Customer().AddCustomer(newCustomer);
        }
    }

    //public class Mail
}

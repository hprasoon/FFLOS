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
}
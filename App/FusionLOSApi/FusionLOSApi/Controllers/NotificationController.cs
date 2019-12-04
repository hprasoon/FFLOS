using System.Web.Http;
using FusionLOS.Models;

namespace App1WebApi.Controllers
{
    public class NotificationMessageController : ApiController
    {
        public void Post([FromBody]NotificationMessage newloan)
        {
            new NotificationMessage().Add(newloan);
        }
    }
}
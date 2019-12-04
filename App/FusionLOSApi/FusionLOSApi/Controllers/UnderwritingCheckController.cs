using System.Web.Http;

namespace FusionLOS.Controllers
{
    public class UnderwritingCheckController : ApiController
    {
        public bool Get(int id)
        {
            return id > 0 ? true : false;
        }
    }
}
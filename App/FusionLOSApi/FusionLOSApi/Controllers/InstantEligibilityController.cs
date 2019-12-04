using System;
using System.Threading.Tasks;
using System.Web.Http;
using CallRequestResponseService;

namespace FusionLOS.Controllers
{
    public class InstantEligibilityController : ApiController
    {
        public async Task<string> InstantEligibilityCheck([FromBody]string customerId)
        {
            return await RequestResponseService(Convert.ToInt32(customerId));
        }

        public async Task<string> RequestResponseService(int consumerid)
        {
            var eligibilityparams = await MLSERVICE_CALL.GetFFDCDataForInitialCheck(consumerid);
            return await MLSERVICE_CALL.InvokeInitialCheckService(eligibilityparams).ConfigureAwait(false);
        }
    }
}

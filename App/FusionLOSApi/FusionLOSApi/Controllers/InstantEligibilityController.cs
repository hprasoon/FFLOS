using System;
using System.Threading.Tasks;
using System.Web.Http;
using CallRequestResponseService;

namespace FusionLOS.Controllers
{
    public class InstantEligibilityController : ApiController
    {
        public async Task<bool> Get(int id)
        {
            return await RequestResponseService(id);
        }

        public async Task<bool> RequestResponseService(int consumerid)
        {
            var eligibilityparams = await MLSERVICE_CALL.GetFFDCDataForInitialCheck(consumerid);
            return await MLSERVICE_CALL.InvokeInitialCheckService(eligibilityparams).ConfigureAwait(false);
        }
    }
}

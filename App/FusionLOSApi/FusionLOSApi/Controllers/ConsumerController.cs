using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static CallRequestResponseService.MLSERVICE_CALL;

namespace FusionLOS.Controllers
{
    public class ConsumerController : ApiController
    {
        public ConsumerDetais Get(int id)
        {
            var ConsumerData = GetFFDCConsumerData(id);
            return ConsumerData?.Result;
        }

        private static async Task<ConsumerDetais> GetFFDCConsumerData(int ConsumerId)
        {
            ConsumerDetais consdetails = new ConsumerDetais();
            using (var client = new HttpClient())
            {
                var Tockenclient = new HttpClient();
                Token tok = await GetEligilityToken(Tockenclient);
                Tockenclient.Dispose();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "test");

                client.BaseAddress = new Uri("https://api.preprod.fusionfabric.cloud/retail-us/customer-read/v1/consumers/831");
                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject joResponse = JObject.Parse(result);
                    consdetails = JsonConvert.DeserializeObject<ConsumerDetais>(result);
                }
            }
            return consdetails;
        }

        private static async Task<Token> GetEligilityToken(HttpClient client)
        {
            try
            {
                string baseAddress = @"https://api.preprod.fusionfabric.cloud/login/v1/sandbox/oidc/token";

                string grant_type = "client_credentials";
                string client_id = "1eb8d602-2e53-403a-8e61-f760dc1ac6c8";
                //string client_secret = "e9f10a9f-f716-42f4-8f4f-c3254922e466";
                string client_secret = "e99b9f2b-eb59-4f24-aabe-a2e53b73f910";

                var form = new Dictionary<string, string>
                {
                    {"grant_type", grant_type},
                    {"client_id", client_id},
                    {"client_secret", client_secret},
                };

                HttpResponseMessage tokenResponse = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
                var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
                Token tok = JsonConvert.DeserializeObject<Token>(jsonContent);
                return tok;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

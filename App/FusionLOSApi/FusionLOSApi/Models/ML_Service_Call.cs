using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CallRequestResponseService
{
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class MLSERVICE_CALL
    {
        public static void Main(string[] args)
        {
            InvokeRequestResponseService().Wait();
        }

        public static async Task InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"A11", "6", "A34", "A43", "1169", "A65", "A75", "4", "A93", "A101", "4 (2)", "A121", "67", "A143", "A152", "2", "A173", "1", "A192", "A201", "1 (2)"},
                                Values = new string[,] {  { "A11", "6", "A34", "A43", "1169", "A65", "A75", "4", "A93", "A101", "4", "A121", "67", "A143", "A152", "2", "A173", "1", "A192", "A201", "1" },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "H/+v0qcqWolAGqYP0g5DzpfeYApPT+r48t9+kxZXXMi+zdhLDzA237CZ3SwQZy3z7HSE4RkrJ25IxCKl06NxYQ=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/e823b504482f4aaabad95f380e685cae/services/f8dd74f017ca41528c1cd3b236de839d/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }

        public static async Task<string> InvokeInitialCheckService(Eligibilityparameters eligParams)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"Age", "Income", "Avg Monthly Savings", "Credit Duration", "Granted"},
                                Values = new string[,] {  { eligParams.Age, eligParams.Income, eligParams.AvgMonthlySavings.ToString(), eligParams.CreditDuration, "0" },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "SP2YNJ46jle/OttUR4NG2G+2DuURyynT/A+7Rc7jVP1kP63dJIZJ2Aizn4Ps9Q5sryHh7bkLDNkpKnJ4QulYoA=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/e823b504482f4aaabad95f380e685cae/services/67f6954049de4038bd6f32f00668e564/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
            return "";
        }

        public static async Task<ConsumerDetais> GetFFDCConsumerData(int ConsumerId)
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

        public static async Task<List<AccountDetails>> GetFFDCAccountData(int ConsumerId)
        {
            List<AccountDetails> accdetails = new List<AccountDetails>();
            using (var client = new HttpClient())
            {
                var Tockenclient = new HttpClient();
                Token tok = await GetEligilityToken(Tockenclient);
                Tockenclient.Dispose();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tok.AccessToken);
                client.BaseAddress = new Uri("https://api.preprod.fusionfabric.cloud/retail-us/account/v1/consumers/831/accounts");
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject joResponse = JObject.Parse(result);
                    accdetails = JsonConvert.DeserializeObject<List<AccountDetails>>(result);
                }
            }
            return accdetails;
        }

        public static async Task<List<AccountBalanceDetails>> GetFFDCAccountBalanceData(int ConsumerId, int accountId)
        {
            List<AccountBalanceDetails> accdetails = new List<AccountBalanceDetails>();
            using (var client = new HttpClient())
            {
                var Tockenclient = new HttpClient();
                Token tok = await GetEligilityToken(Tockenclient);
                Tockenclient.Dispose();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tok.AccessToken);
                client.BaseAddress = new Uri("https://api.preprod.fusionfabric.cloud/retail-us/account/v1/consumers/" + ConsumerId + "/accounts/" + accountId + "/ balances");
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject joResponse = JObject.Parse(result);
                    accdetails = JsonConvert.DeserializeObject<List<AccountBalanceDetails>>(result);
                }
            }
            return accdetails;
        }

        public static async Task<List<AccountTransactionDetails>> GetFFDCAccountTransactionData(int ConsumerId, int accountId)
        {
            List<AccountTransactionDetails> accdetails = new List<AccountTransactionDetails>();
            using (var client = new HttpClient())
            {
                var Tockenclient = new HttpClient();
                Token tok = await GetEligilityToken(Tockenclient);
                Tockenclient.Dispose();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tok.AccessToken);
                client.BaseAddress = new Uri("https://api.preprod.fusionfabric.cloud/retail-us/account/v1/consumers/" + ConsumerId + "/accounts/" + accountId + "/ transactions");
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject joResponse = JObject.Parse(result);
                    accdetails = JsonConvert.DeserializeObject<List<AccountTransactionDetails>>(result);
                }
            }
            return accdetails;
        }

        public static async Task<Eligibilityparameters> GetFFDCDataForInitialCheck(int ConsumerId)
        {
            var eligParams = new Eligibilityparameters();
            var consumerdetails = await GetFFDCConsumerData(ConsumerId);
            eligParams.Age = getAge(consumerdetails.dateOfBirth);
            var accountdetails = await GetFFDCAccountData(ConsumerId);
            foreach (var account in accountdetails)
            {
                var FFDCAccountBalanceData = await GetFFDCAccountBalanceData(ConsumerId, Convert.ToInt32(account.accountId));
                foreach (var balance in FFDCAccountBalanceData)
                {
                    if (balance.type.ToString().ToUpper() != "LOAN")
                        eligParams.AvgMonthlySavings += balance.amount;
                }
                var FFDCAccountTransData = await GetFFDCAccountTransactionData(ConsumerId, Convert.ToInt32(account.accountId));
                for (int i = 1; i < 7; i++)
                {
                    List<AccountTransactionDetails> transdetails = FFDCAccountTransData.Where(x => Convert.ToDateTime(x.transactionDate).Month <= DateTime.Today.Month && (Convert.ToDateTime(x.transactionDate).Month <= DateTime.Today.Month - i && x.debit == false)).ToList();
                    if (transdetails != null)
                    {
                        foreach (var transdetail in transdetails)
                        {
                            eligParams.Income += transdetail.transactionAmount;
                        }
                    }
                }
                eligParams.Income = (Convert.ToDecimal(eligParams.Income) / 6).ToString();
                // As of now making it to 0 since the accounttype with Loan is not returning any value from FFDC.
                eligParams.CreditDuration = "0";
            }
            return eligParams;
        }

        private static string getAge(string dateOfBirth)
        {
            var dob = Convert.ToDateTime(dateOfBirth);
            var age = dob.Year - DateTime.Today.Year;
            return age.ToString();
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

        public async void DOintialOperation()
        {
            Eligibilityparameters eligParams = new Eligibilityparameters();
            await InvokeInitialCheckService(eligParams).ConfigureAwait(false);
        }

        internal class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
        }

        public class Address
        {
            public string streetLine_1 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postalCode { get; set; }
            public string addressType { get; set; }
        }

        public class Email
        {
            public string address { get; set; }
            public string emailType { get; set; }
        }

        public class Phone
        {
            public string number { get; set; }
            public string phoneType { get; set; }
        }

        public class ConsumerDetais
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string dateOfBirth { get; set; }
            public string employer { get; set; }
            public string occupation { get; set; }
            public string businessType { get; set; }
            public List<Address> addresses { get; set; }
            public List<Email> emails { get; set; }
            public List<Phone> phones { get; set; }
        }

        public List<AccountDetails> AccountDetailsprop { get; set; }

        public class AccountDetails
        {
            public string accountId { get; set; }
            public string accountType { get; set; }
            public string nickname { get; set; }
            public string formattedAccountNumber { get; set; }
        }

        public class AccountBalanceDetails
        {
            public string type { get; set; }
            public decimal amount { get; set; }

        }

        public class AccountTransactionDetails
        {
            public string id { get; set; }
            public string accountId { get; set; }
            public string transactionDate { get; set; }
            public decimal transactionAmount { get; set; }
            public decimal runningBalance { get; set; }
            public bool pending { get; set; }
            public bool debit { get; set; }
            public string checkImage { get; set; }
            public int checkNumber { get; set; }
            public string description { get; set; }
            public string description2 { get; set; }
            public string transactionCategory { get; set; }
            public string markAttended { get; set; }
        }
    }
}

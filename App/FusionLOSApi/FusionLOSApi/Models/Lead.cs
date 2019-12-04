using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static CallRequestResponseService.MLSERVICE_CALL;

namespace FusionLOS.Models
{
    public class Lead
    {
        string[] formats = { "M/d/yyyy", "d/M/yyyy", "M-d-yyyy", "d-M-yyyy", "d-MMM-yy", "d-MMMM-yyyy", };
        string path = HttpContext.Current.Server.MapPath("../lead.json");
        public List<Lead> GetLead()
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<Lead> loans = JsonConvert.DeserializeObject<List<Lead>>(json);
                return loans;
            }
        }

        public void Add(Lead newloan)
        {
            //FFDC
            FFDCSave(newloan);

            LocalSave(newloan);
        }

        private void FFDCSave(Lead newloan)
        {
            CreateFFDCLoan(newloan);
        }

        private static async void CreateFFDCLoan(Lead newloan)
        {
            using (var client = new HttpClient())
            {
                var Tockenclient = new HttpClient();
                Token tok = await GetEligilityToken(Tockenclient);
                Tockenclient.Dispose();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tok.AccessToken);

                client.BaseAddress = new Uri("https://api.preprod.fusionfabric.cloud/mortgagebot/los/lead-import/v1/loans");
                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject joResponse = JObject.Parse(result);
                }
            }
        }

        private static async Task<Token> GetEligilityToken(HttpClient client)
        {
            try
            {
                string baseAddress = @"https://api.preprod.fusionfabric.cloud/login/v1/sandbox/oidc/token";
                string grant_type = "client_credentials";
                string client_id = "1eb8d602-2e53-403a-8e61-f760dc1ac6c8";
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

        private void LocalSave(Lead newloan)
        {
            if (newloan == null)
            {
                throw new ArgumentNullException("newLoan");
            }

            List<Lead> loans;
            double maxLoanNumber;

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                loans = JsonConvert.DeserializeObject<List<Lead>>(json);
                maxLoanNumber = loans.Max(y => y.leadId);
                r.Close();
            }

            string applndate = string.Empty;
            string impdate = string.Empty;

            var loan = new Lead
            {
                leadId = leadId,
                borrowers = newloan.borrowers,
                loan = newloan.loan,
                realtor = newloan.realtor,
                seller = newloan.seller
            };

            loans.Add(loan);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, loans);
            }
        }

        public long leadId
        {
            get;
            set;
        }

        public seller seller
        {
            get;
            set;
        }

        public realtor realtor
        {
            get;
            set;
        }

        public loandetails loan
        {
            get;
            set;
        }

        public List<borrower> borrowers
        {
            get;
            set;
        }

        public subjectProperty subjectProperty
        {
            get;
            set;
        }
    }

    public class borrower
    {
        public string birthDate
        {
            get;
            set;
        }
        public string borrowerPositionType
        {
            get;
            set;
        }

        public name name
        {
            get;
            set;
        }

        public contactInformation contactInformation
        {
            get;
            set;
        }

        public addressDetail currentAddress
        {
            get;
            set;
        }
    }

    public class addressDetail
    {
        public string countryCode
        {
            get;
            set;
        }
        public string unit
        {
            get;
            set;
        }
        public string street
        {
            get;
            set;
        }
        public string city
        {
            get;
            set;
        }
        public string postalCode
        {
            get;
            set;
        }
        public string stateCode
        {
            get;
            set;
        }
    }

    public class contactInformation
    {
        public string emailAddress
        {
            get;
            set;
        }

        public List<phoneNumbers> phoneNumbers
        {
            get;
            set;
        }
    }

    public class phoneNumbers
    {
        public string extension
        {
            get;
            set;
        }

        public string number
        {
            get;
            set;
        }

        public string type
        {
            get;
            set;
        }
    }

    public class name
    {
        public string firstName
        {
            get;
            set;
        }

        public string middleName
        {
            get;
            set;
        }

        public string lastName
        {
            get;
            set;
        }

        public string suffix
        {
            get;
            set;
        }
    }

    public class loandetails
    {
        public string baseLoanAmount
        {
            get;
            set;
        }

        public string loanOfficerNmlsNumber
        {
            get;
            set;
        }

        public string loanPurposeType
        {
            get;
            set;
        }

        public string salesContractAmount
        {
            get;
            set;
        }
    }

    public class realtor
    {
        public string fullName
        {
            get;
            set;
        }

        public string nmlsNumber
        {
            get;
            set;
        }

        public string phoneNumber
        {
            get;
            set;
        }
    }

    public class seller
    {
        public string firstName
        {
            get;
            set;
        }

        public string lastName
        {
            get;
            set;
        }

        public string phoneNumber
        {
            get;
            set;
        }
    }

    public class subjectProperty
    {
        public string propertyType
        {
            get;
            set;
        }

        public string propertyUsageType
        {
            get;
            set;
        }

        public List<addressdetails> address
        {
            get;
            set;
        }
    }

    public class addressdetails
    {
        public string fipsCode
        {
            get;
            set;
        }

        public string unit
        {
            get;
            set;
        }

        public string street
        {
            get;
            set;
        }

        public string city
        {
            get;
            set;
        }

        public string postalCode
        {
            get;
            set;
        }

        public string stateCode
        {
            get;
            set;
        }
    }
}
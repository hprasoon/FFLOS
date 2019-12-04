using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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
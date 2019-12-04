using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FusionLOS.Models
{
    public class Loan
    {
        string[] formats = { "M/d/yyyy", "d/M/yyyy", "M-d-yyyy", "d-M-yyyy", "d-MMM-yy", "d-MMMM-yyyy", };
        string path = HttpContext.Current.Server.MapPath("../loan.json");
        public List<Loan> GetLoans()
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<Loan> loans = JsonConvert.DeserializeObject<List<Loan>>(json);
                return loans;
            }
        }

        public void Add(Loan newloan)
        {
            if (newloan == null)
            {
                throw new ArgumentNullException("newLoan");
            }

            List<Loan> loans;
            double maxLoanNumber;

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                loans = JsonConvert.DeserializeObject<List<Loan>>(json);
                maxLoanNumber = loans.Max(y => y.LoanNumber);
                r.Close();
            }
            DateTime date;
            string applndate = string.Empty;
            string impdate = string.Empty;
            if (DateTime.TryParseExact(newloan.ApplicationDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                applndate = date.ToString("MM/dd/yyyy");
            if (DateTime.TryParseExact(newloan.ImportantDates, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                impdate = date.ToString("MM/dd/yyyy");
            var loan = new Loan
            {
                LoanNumber = maxLoanNumber + 1,

                ApplicationDate = applndate,
                BorrowerName = newloan.BorrowerName,
                BorrowerAddress1 = newloan.BorrowerAddress1,
                BorrowerAddress2 = newloan.BorrowerAddress2,
                BorrowerAddress3 = newloan.BorrowerAddress3,
                PropertyAddress = newloan.PropertyAddress,
                email = newloan.email,
                SSN = newloan.SSN,
                SalesPrice = newloan.SalesPrice,
                AppraisedValue = newloan.AppraisedValue,
                BaseLoanAmount = newloan.BaseLoanAmount,
                Product = newloan.Product,
                LTV = newloan.LTV,
                LoanStatus = newloan.LoanStatus,
                ImportantDates = impdate
            };

            loans.Add(loan);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, loans);
            }
        }

        public double LoanNumber
        {
            get;
            set;
        }
        public string ApplicationDate
        {
            get;
            set;
        }

        public string BorrowerName
        {
            get;
            set;
        }

        public string BorrowerAddress1
        {
            get;
            set;
        }
        public string BorrowerAddress2
        {
            get;
            set;
        }
        public string BorrowerAddress3
        {
            get;
            set;
        }

        public string PropertyAddress
        {
            get;
            set;
        }
        public string email
        {
            get;
            set;
        }
        public double SSN
        {
            get;
            set;
        }

        public double SalesPrice
        {
            get;
            set;
        }

        public double AppraisedValue
        {
            get;
            set;
        }

        public double BaseLoanAmount
        {
            get;
            set;
        }

        public string Product
        {
            get;
            set;
        }

        public string LTV
        {
            get;
            set;
        }

        public string LoanStatus
        {
            get;
            set;
        }

        public string ImportantDates
        {
            get;
            set;
        }
    }
}
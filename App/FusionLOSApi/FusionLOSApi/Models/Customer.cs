using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FusionLOS.Models
{
    public class Customer
    {
        string[] formats = { "M/d/yyyy", "d/M/yyyy", "M-d-yyyy", "d-M-yyyy", "d-MMM-yy", "d-MMMM-yyyy", };
        string path = HttpContext.Current.Server.MapPath("../customers.json");
        public List<Customer> GetCustomers()
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                return customers;
            }
        }

        public void AddCustomer(Customer newCustomer)
        {
            if (newCustomer == null)
            {
                throw new ArgumentNullException("newCustomer");
            }

            List<Customer> customers;
            double maxLoanNumber;

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                maxLoanNumber = customers.Max(y => y.CustomerNumber);
                r.Close();
            }

            DateTime date;
            string applndate = string.Empty;
            string impdate = string.Empty;
            if (DateTime.TryParseExact(newCustomer.dateOfBirth, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                applndate = date.ToString("MM/dd/yyyy");

            var customer = new Customer
            {
                CustomerNumber = maxLoanNumber + 1,

                dateOfBirth = applndate,
                businessType = newCustomer.employer,
                firstName = newCustomer.firstName,
                lastName = newCustomer.lastName,
                occupation = newCustomer.occupation,
                employer = newCustomer.employer,

                addresses = (List<address>)newCustomer.addresses,
                coreIdentifications = (List<coreIdentification>)newCustomer.coreIdentifications,
                emails = (List<email>)newCustomer.emails,
                phones = (List<phone>)newCustomer.phones
            };

            customers.Add(customer);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, customers);
            }
        }

        private double CustomerNumber
        {
            get;
            set;
        }

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

        public string employer
        {
            get;
            set;
        }

        public string occupation
        {
            get;
            set;
        }
        public string businessType
        {
            get;
            set;
        }
        public string dateOfBirth
        {
            get;
            set;
        }

        public List<address> addresses
        {
            get;
            set;
        }
        public List<phone> phones
        {
            get;
            set;
        }
        public List<email> emails
        {
            get;
            set;
        }

        public List<coreIdentification> coreIdentifications
        {
            get;
            set;
        }
    }

    public class address
    {
        public string ImportantDatesstreetLine_1
        {
            get;
            set;
        }
        public string streetLine_2
        {
            get;
            set;
        }
        public string streetLine_3
        {
            get;
            set;
        }
        public string city
        {
            get;
            set;
        }
        public string state
        {
            get;
            set;
        }
        public string country
        {
            get;
            set;
        }
        public string province
        {
            get;
            set;
        }
        public string postalCode
        {
            get;
            set;
        }
        public string postalCodeExtension
        {
            get;
            set;
        }
        public string addressType
        {
            get;
            set;
        }
    }

    public class phone
    {
        public string countryCode { get; set; }
        public string number { get; set; }
        public string extension { get; set; }
        public string phoneType { get; set; }
    }

    public class email
    {
        public string address { get; set; }
        public string emailType { get; set; }
    }

    public class coreIdentification
    {
        public string idNumber { get; set; }
        public string idType { get; set; }
    }
}
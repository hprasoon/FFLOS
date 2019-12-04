using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FusionLOS.Models
{
    public class Notification
    {
        string[] formats = { "M/d/yyyy", "d/M/yyyy", "M-d-yyyy", "d-M-yyyy", "d-MMM-yy", "d-MMMM-yyyy", };
        string path = HttpContext.Current.Server.MapPath("../notification.json");

        public int id;

        public string statusMessage
        {
            get;
            set;
        }

        public string updateduser
        {
            get;
            set;
        }

        public void Add(Notification newloan)
        {
            if (newloan == null)
            {
                throw new ArgumentNullException("newLoan");
            }

            List<Notification> loans;
            int maxLoanNumber;

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                loans = JsonConvert.DeserializeObject<List<Notification>>(json);
                maxLoanNumber = loans.Max(y => y.id);
                r.Close();
            }

            var loan = new Notification
            {
                id = maxLoanNumber + 1,
                statusMessage = newloan.statusMessage,
                updateduser = newloan.updateduser
            };

            loans.Add(loan);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, loans);
            }
        }
    }
}
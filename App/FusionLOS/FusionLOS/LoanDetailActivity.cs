using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Globalization;

namespace FusionLOS
{
    [Activity(Label = "Loan Details")]
    public class LoanDetailActivity : Activity
    {
        TextView textBorrowerName;
        TextView textBorrowerAddress1;
        TextView textBorrowerAddress2;
        TextView textBorrowerAddress3;
        TextView textSSN;
        TextView textEmail;
        TextView textAmount;
        TextView textLTV;
        TextView textLoanStatus;
        TextView textImportantDates;
        TextView textProduct;
        TextView textViewComplianceAlert;
        TextView textPropertyAddress;
        TextView texttextSalesPrice;
        TextView textLoanNumber;
        TextView textAppraisedValue;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout4);

            string text = Intent.GetStringExtra("LoanNumber");

            if (!string.IsNullOrEmpty(text))
            {
                textBorrowerName = FindViewById<TextView>(Resource.Id.textBorrowerName);
                textBorrowerAddress1 = FindViewById<TextView>(Resource.Id.BorrowerAddress1);
                textBorrowerAddress2 = FindViewById<TextView>(Resource.Id.BorrowerAddress2);
                textBorrowerAddress3 = FindViewById<TextView>(Resource.Id.BorrowerAddress3);
                textAmount = FindViewById<TextView>(Resource.Id.textLoanAmount);
                textLTV = FindViewById<TextView>(Resource.Id.textLTV);
                textLoanStatus = FindViewById<TextView>(Resource.Id.textLoanStatus);
                textImportantDates = FindViewById<TextView>(Resource.Id.textImportantDates);
                textEmail = FindViewById<TextView>(Resource.Id.email);
                textSSN = FindViewById<TextView>(Resource.Id.SSN);
                textProduct = FindViewById<TextView>(Resource.Id.textProduct);
                textPropertyAddress = FindViewById<TextView>(Resource.Id.textPropertyAddress);
                texttextSalesPrice = FindViewById<TextView>(Resource.Id.textSalesPrice);
                textLoanNumber = FindViewById<TextView>(Resource.Id.textViewLoanNumber);
                textAppraisedValue = FindViewById<TextView>(Resource.Id.AppraisedValue);

                Loan loan = new ListActivity().GetLoans().First(x => x.LoanNumber == Convert.ToDouble(text));

                textBorrowerName.Text = loan.BorrowerName;
                textBorrowerAddress1.Text = loan.BorrowerAddress1;
                textBorrowerAddress2.Text = loan.BorrowerAddress2;
                textBorrowerAddress3.Text = loan.BorrowerAddress3;
                textLTV.Text = Math.Round(Convert.ToDecimal(loan.LTV.Replace("%", "")), 2, MidpointRounding.AwayFromZero).ToString() + "%";
                textLoanStatus.Text = loan.LoanStatus;
                textImportantDates.Text = loan.ImportantDates;
                textAmount.Text = "$" + loan.BaseLoanAmount.ToString();
                textEmail.Text = loan.email;
                textSSN.Text = "SSN : " + loan.SSN.ToString();
                textProduct.Text = loan.Product;
                textPropertyAddress.Text = loan.PropertyAddress;
                texttextSalesPrice.Text = "$" + loan.SalesPrice.ToString();
                textLoanNumber.Text = loan.LoanNumber.ToString();
                textAppraisedValue.Text = "$" + loan.AppraisedValue.ToString();

                textViewComplianceAlert = FindViewById<TextView>(Resource.Id.textViewComplianceAlert);

                DateTime appdate = DateTime.Now;
                DateTime tday = DateTime.Now;

                string[] formats = { "M/d/yyyy", "d/M/yyyy", "M-d-yyyy", "d-M-yyyy", "d-MMM-yy", "d-MMMM-yyyy", };
                if (DateTime.TryParseExact(loan.ApplicationDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out appdate))
                {
                    appdate = appdate;
                }
                if ((tday.Date - appdate.Date).Days > 3)
                {
                    textViewComplianceAlert.Visibility = ViewStates.Invisible;
                }
                else
                {
                    Android.Widget.Toast.MakeText(this, "Loan Disclosure Pending", Android.Widget.ToastLength.Long).Show();
                }
            }
        }

        private void LoanDetailActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ListActivity));
            //Finish();
        }
    }
}
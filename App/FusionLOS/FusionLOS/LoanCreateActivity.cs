using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Android.Content;

namespace FusionLOS
{
    [Activity(Label = "Create Loan")]
    public class LoanCreateActivity : Activity
    {
        Button btnSubmit;
        Button btnBack;
        Button btnMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout5);

            //Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            //spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            //var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.product_array, Android.Resource.Layout.SimpleSpinnerItem);
            //adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //spinner.Adapter = adapter;

            EditText editFirstName = FindViewById<EditText>(Resource.Id.editFirstName);
            EditText editLastName = FindViewById<EditText>(Resource.Id.editLastName);
            EditText editBorrowerEmail = FindViewById<EditText>(Resource.Id.editBorrowerEmail);
            EditText editBorrowerAddress1 = FindViewById<EditText>(Resource.Id.editBorrowerAddress1);
            EditText editBaseLoanAmount = FindViewById<EditText>(Resource.Id.editBaseLoanAmount);




            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnMap = FindViewById<Button>(Resource.Id.btnMap);

            btnSubmit.Click += BtnSubmit_Click;
            btnBack.Click += BtnBack_Click;
            btnMap.Click += BtnMap_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MenuActivity));
        }

        private void BtnMap_Click(object sender, EventArgs e)
        {
            //StartActivity(typeof(MapActivity));
            EditText editPropertyAddress = FindViewById<EditText>(Resource.Id.editPropertyAddress);
            EditText editAppraisedValue = FindViewById<EditText>(Resource.Id.editAppraisedValue);

            Android.Net.Uri gmmIntentUri = Android.Net.Uri.Parse("geo:0,0?q=" + editPropertyAddress.Text);
            Intent mapIntent = new Intent(Intent.ActionView, gmmIntentUri);
            mapIntent.SetPackage("com.google.android.apps.maps");
            StartActivity(mapIntent);

            editPropertyAddress.Text = editPropertyAddress.Text + " Charleston, SC 29403 USA";
            editAppraisedValue.Text = "200000";
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                EditText editFirstName = FindViewById<EditText>(Resource.Id.editFirstName);
                EditText editLastName = FindViewById<EditText>(Resource.Id.editLastName);
                EditText editBorrowerEmail = FindViewById<EditText>(Resource.Id.editBorrowerEmail);
                EditText editBorrowerAddress1 = FindViewById<EditText>(Resource.Id.editBorrowerAddress1);
                EditText editPropertyAddress = FindViewById<EditText>(Resource.Id.editPropertyAddress);
                EditText editSalesPrice = FindViewById<EditText>(Resource.Id.editSalesPrice);
                EditText editAppraisedValue = FindViewById<EditText>(Resource.Id.editAppraisedValue);
                EditText editBaseLoanAmount = FindViewById<EditText>(Resource.Id.editBaseLoanAmount);

                Loan newLoan = new Loan();
                newLoan.FirstName = editFirstName.Text;
                newLoan.LastName = editLastName.Text;
                newLoan.email = editBorrowerEmail.Text;
                newLoan.BorrowerAddress1 = editBorrowerAddress1.Text;
                newLoan.ApplicationDate = DateTime.Now.ToString("MM/dd/yyyy");
                newLoan.PropertyAddress = editPropertyAddress.Text;                
                newLoan.SalesPrice = Convert.ToDouble(editSalesPrice.Text);
                newLoan.AppraisedValue = Convert.ToDouble(editAppraisedValue.Text);
                newLoan.BaseLoanAmount = Convert.ToDouble(editBaseLoanAmount.Text);
                newLoan.LTV = (newLoan.BaseLoanAmount / newLoan.AppraisedValue).ToString() + "%";

                
                newLoan.LoanStatus = "New";
                newLoan.ImportantDates = DateTime.Now.ToString("MM/dd/yyyy");

                //CreateLoan(newLoan);
                StartActivity(typeof(MainActivity1));
            }
            catch (Exception ex)
            {

            }
        }

        private void CreateLoan(Loan newLoan)
        {
            var request = HttpWebRequest.Create(string.Format(@"https://losmobile.azurewebsites.net/api/values"));
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(newLoan);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
        }

        
        //private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        //{
        //    Spinner spinner = (Spinner)sender;
        //    string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

        //    EditText editProduct = FindViewById<EditText>(Resource.Id.editProduct);
        //    editProduct.Text = toast;
        //    //Toast.MakeText(this, toast, ToastLength.Long).Show();
        //}
    }
}
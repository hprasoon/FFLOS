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
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace FusionLOS
{
    [Activity(Label ="Loan Pipeline")]
    public class ListActivity : Activity
    {
        private ListView studentlistView;
        private List<Loan> mlist;
        LoanAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout2);

            List<Loan> objstud = GetLoans();

            studentlistView = FindViewById<ListView>(Resource.Id.listView1);
            mlist = new List<Loan>();
            mlist = objstud.OrderByDescending(x => x.ApplicationDate).ToList();
            adapter = new LoanAdapter(this, mlist);
            studentlistView.Adapter = adapter;
            studentlistView.ItemClick += StudentlistView_ItemClick;

            //FindViewById<Button>(Resource.Id.back).Click += ListActivity_Click;
        }

        private void ListActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            //Finish();
        }

        private void StudentlistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var select = mlist[e.Position].LoanNumber;
            //Android.Widget.Toast.MakeText(this, select, Android.Widget.ToastLength.Long).Show();

            var loandetailActivity = new Intent(this, typeof(LoanDetailActivity));
            loandetailActivity.PutExtra("LoanNumber", select.ToString());
            StartActivity(loandetailActivity);
            //Finish();
        }

        public List<Loan> GetLoans()
        {
            List<Loan> loanList = new List<Loan>();

            var request = HttpWebRequest.Create(string.Format(@"https://losmobile.azurewebsites.net/api/values"));
            //request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                }

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    loanList = JsonConvert.DeserializeObject<List<Loan>>(content);

                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                    }
                }
            }

            return loanList;
        }
    }
}
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

namespace FusionLOS
{
    [Activity(Label = "Menu")]
    public class MenuActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout6);

            FindViewById<Button>(Resource.Id.buttonCreateLoan).Click += buttonCreate_Click;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoanCreateActivity));
            //Finish();
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ListActivity));
            //Finish();
        }
    }
}
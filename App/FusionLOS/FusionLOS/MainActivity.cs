using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using System;
using Android.Graphics;

namespace FusionLOS
{
    [Activity(MainLauncher = true, NoHistory = true)]
    public class MainActivity : Activity
    {
        EditText editText1;
        EditText editText2;

        int number;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //TextView textAuthentication = FindViewById<TextView>(Resource.Id.textAuthentication);
            //textAuthentication.Visibility = ViewStates.Invisible;
            FindViewById<Button>(Resource.Id.btnSubmit).Click += MainActivity_Click;
        }

        private ListView studentlistView;
        private List<Loan> mlist;
        LoanAdapter adapter;

        #region Commented
        //private void MainActivity2_Click(object sender, System.EventArgs e)
        //{
        //    editText1 = FindViewById<EditText>(Resource.Id.editText1);
        //    editText2 = FindViewById<EditText>(Resource.Id.editText2);
        //    if (editText1.Text.Trim() == "avista3" && editText2.Text.Trim() == "Book@111")
        //    {
        //        SetContentView(Resource.Layout.layout3);

        //        List<Loan> objstud = new List<Loan>();
        //        objstud.Add(new Loan
        //        {
        //            LoanNumber = "Suresh",
        //            ApplicationDate = "26"
        //        });
        //        objstud.Add(new Loan
        //        {
        //            LoanNumber = "C#Cornet",
        //            ApplicationDate = "26"
        //        });
        //        studentlistView = FindViewById<ListView>(Resource.Id.listView1);
        //        mlist = new List<Loan>();
        //        mlist = objstud;
        //        adapter = new LoanAdapter(this, mlist);
        //        studentlistView.Adapter = adapter;
        //        studentlistView.ItemClick += StudentlistView_ItemClick;

        //    }
        //}

        //private void StudentlistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    var select = mlist[e.Position].LoanNumber;
        //    Android.Widget.Toast.MakeText(this, select, Android.Widget.ToastLength.Long).Show();
        //}

        //private void MainActivity_Click(object sender, System.EventArgs e)
        //{
        //    editText1 = FindViewById<EditText>(Resource.Id.editText1);
        //    editText2 = FindViewById<EditText>(Resource.Id.editText2);

        //    if (editText1.Text.Trim() == "avista3" && editText2.Text.Trim() == "Book@111")
        //    {
        //        SetContentView(Resource.Layout.layout1);
        //        string[] items = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
        //        //ArrayAdapter<string> ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);

        //        LinearLayout linearLayout2 = FindViewById<LinearLayout>(Resource.Id.linearLayout2);//.Click += MainActivity_Click;
        //        List<LoanDetails> loans = LoanDetails.GetLoanDetails();

        //        for (int index = 0; index < loans.Count; index++)
        //        {
        //            LinearLayout llInner = new LinearLayout(this);
        //            LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.WrapContent);
        //            llInner.Orientation = Orientation.Horizontal;
        //            llInner.WeightSum = 2;
        //            linearLayout2.AddView(llInner);

        //            TextView tv = new TextView(this);
        //            TextView tv1 = new TextView(this);
        //            //if (index == 0)
        //            {
        //                tv.Text = "Loan Number";
        //                tv1.Text = "Application Date";
        //                //continue;
        //            }

        //            tv.Id = index + 100;
        //            tv.SetTextColor(global::Android.Graphics.Color.ParseColor("#FFFFFF"));
        //            tv.SetTextAppearance(this, global::Android.Resource.Style.TextAppearanceMedium);
        //            lp = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.WrapContent);
        //            lp.Gravity = Android.Views.GravityFlags.Right;
        //            lp.Weight = 1;
        //            llInner.AddView(tv);

        //            tv = new TextView(this);
        //            tv.Text = loans[index].loanNumber;
        //            tv.Id = index + 120;
        //            tv.SetTextColor(global::Android.Graphics.Color.ParseColor("#FFFFFF"));
        //            tv.SetTextAppearance(this, global::Android.Resource.Style.TextAppearanceMedium);
        //            lp = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.WrapContent);
        //            lp.Weight = 1;
        //            llInner.AddView(tv);

        //            tv1.Id = index + 140;
        //            tv1.SetTextColor(global::Android.Graphics.Color.ParseColor("#FFFFFF"));
        //            tv1.SetTextAppearance(this, global::Android.Resource.Style.TextAppearanceMedium);
        //            lp = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.WrapContent);
        //            lp.Gravity = Android.Views.GravityFlags.Right;
        //            lp.Weight = 1;
        //            llInner.AddView(tv1);

        //            tv1 = new TextView(this);
        //            tv1.Text = loans[index].loanNumber;
        //            tv1.Id = index + 160;
        //            tv1.SetTextColor(global::Android.Graphics.Color.ParseColor("#FFFFFF"));
        //            tv1.SetTextAppearance(this, global::Android.Resource.Style.TextAppearanceMedium);
        //            lp = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.WrapContent);
        //            lp.Weight = 1;
        //            llInner.AddView(tv1);
        //        }
        //    }
        //}

        //public class LoanDetails
        //{
        //    public string loanNumber;
        //    public string applicationDate;

        //    public static List<LoanDetails> GetLoanDetails()
        //    {
        //        List<LoanDetails> loans = new List<FusionLOS.LoanDetails>();

        //        loans.Add(new LoanDetails() { applicationDate = "09/02/2018", loanNumber = "101" });
        //        loans.Add(new LoanDetails() { applicationDate = "08/02/2018", loanNumber = "102" });
        //        loans.Add(new LoanDetails() { applicationDate = "07/02/2018", loanNumber = "103" });
        //        loans.Add(new LoanDetails() { applicationDate = "06/02/2018", loanNumber = "104" });

        //        return loans;
        //    }
        //}

        #endregion

        private void MainActivity_Click(object sender, System.EventArgs e)
        {
            //editText1 = FindViewById<EditText>(Resource.Id.editText1);
            editText2 = FindViewById<EditText>(Resource.Id.editText2);

            //TextView textAuthentication = FindViewById<TextView>(Resource.Id.textAuthentication);
            //if (editText1.Text.Trim() == "avista3" && editText2.Text.Trim() == "Book@111")
            //{
            //    textAuthentication.Visibility = ViewStates.Invisible;
            //    //StartActivity(typeof(ListActivity));
                StartActivity(typeof(MenuActivity));
                Finish();
            //}
            //else
            //{
            //    textAuthentication.Visibility = ViewStates.Visible;
            //}
        }
    }

    public class Loan
    {
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

    public class LoanAdapter : BaseAdapter<Loan>
    {
        public List<Loan> sList;
        private Context sContext;
        public LoanAdapter(Context context, List<Loan> list)
        {
            sList = list;
            sContext = context;
        }
        public override Loan this[int position]
        {
            get
            {
                return sList[position];
            }
        }
        public override int Count
        {
            get
            {
                return sList.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            try
            {
                if (row == null)
                {
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.layout2, null, false);
                }
                //LayoutParams lparams = new LayoutParams(50, 30); // Width , height
                TextView txtName = row.FindViewById<TextView>(Resource.Id.textView1);
                txtName.Text = "Loan Number : " + sList[position].LoanNumber;
                txtName.LayoutParameters.Height = 70;

                TextView txtName2 = row.FindViewById<TextView>(Resource.Id.textView2);
                txtName2.Text = "Application Date : " + sList[position].ApplicationDate;
                txtName2.LayoutParameters.Height = 55;

                long loannumber = Convert.ToInt64(sList[position].LoanNumber);
                long div = loannumber % 2;

                //if (div == 0)
                //{
                //    txtName.SetBackgroundColor(Color.White);
                //    txtName2.SetBackgroundColor(Color.White);
                //}
                //else
                //{
                //    txtName.SetBackgroundColor(Color.WhiteSmoke);
                //    txtName2.SetBackgroundColor(Color.WhiteSmoke);
                //}


                //Button back = row.FindViewById<Button>(Resource.Id.back);
                /////*txtName*/2.Text = "Application Date : " + sList[position].ApplicationDate;
                //back.Visibility = ViewStates.Invisible;// .LayoutParameters.Height = 50;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally { }
            return row;
        }
    }
}
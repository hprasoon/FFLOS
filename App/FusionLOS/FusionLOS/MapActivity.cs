using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Net;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FusionLOS
{
    [Activity(Label = "Map")]

    public class MapActivity : Activity, IOnMapReadyCallback
    {
        private GoogleMap GMap;
        SearchView searchView;
        Button btnSearch;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.MapActivity);
            SetUpMap();
            btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            btnSearch.Click += BtnSearch_Click;            
        }


        private void SetUpMap()
        {
            if (GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googlemap).GetMapAsync(this);
            }
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            this.GMap = googleMap;
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Android.Net.Uri gmmIntentUri = Android.Net.Uri.Parse("geo:0,0?q=101+main+street");
            Intent mapIntent = new Intent(Intent.ActionView, gmmIntentUri);
            mapIntent.SetPackage("com.google.android.apps.maps");
            StartActivity(mapIntent);
        }
    }
}
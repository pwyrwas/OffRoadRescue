using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;

using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;

namespace OFFroadRescue
{
    [Activity(Label = "mainView", Theme = "@style/MyTheme")]
    public class mainView : ActionBarActivity
    {
        private SupportToolbar mToolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Create your application here
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainView);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
        }
    }
}
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
using SupportActionBarDrawerToogle = Android.Support.V7.App.ActionBarDrawerToggle;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace OFFroadRescue
{
    class MyActionBarDrawerToggle: SupportActionBarDrawerToogle
    {
        private ActionBarActivity mHostActivity;
        private int mOpenedResoruce;
        private int mClosedResoruce;
        public MyActionBarDrawerToggle(ActionBarActivity host, DrawerLayout drawerLayout, int openedResoruce, int closedResource)
            : base(host,drawerLayout,openedResoruce,closedResource)
        {
            mHostActivity = host;
            mOpenedResoruce = openedResoruce;
            mClosedResoruce = closedResource;
        }
        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            mHostActivity.SupportActionBar.SetTitle(mClosedResoruce);

        }
        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            mHostActivity.SupportActionBar.SetTitle(mOpenedResoruce);
        }
        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}
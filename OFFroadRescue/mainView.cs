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
using Android.Support.V4.Widget;
using static Android.Support.V7.Internal.Widget.AdapterViewCompat;
using static Android.Widget.AdapterView;
using OFFroadRescue.Resources.Class;

namespace OFFroadRescue
{
    [Activity(Label = "mainView", Theme = "@style/MyTheme")]
    public class mainView : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        //Create option on left drawer
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Create your application here
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainView);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            
           SetSupportActionBar(mToolbar);

            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("Settings");
            mLeftDataSet.Add("Logout!");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;

            mLeftDrawer.ItemClick += (sender, e) => selectItem(e.Position);


            mDrawerToggle = new MyActionBarDrawerToggle(
                this,
                mDrawerLayout,
                Resource.String.openDrawer,
                Resource.String.closeDrawer
                );

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();

            if (savedInstanceState != null)
            {
                if(savedInstanceState.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.openDrawer);
                }
                else
                {
                    SupportActionBar.SetTitle(Resource.String.closeDrawer);
                }
            }
            else
            {
                //this is the first time the activity is ran
                SupportActionBar.SetTitle(Resource.String.closeDrawer);
            }

        }
        public void selectItem(int item)
        {
            switch (item)
            {
                //item - item on the drawer list on left side
                case 0:
                    Toast.MakeText(ApplicationContext, "Naciśnięto Settings", ToastLength.Long).Show();
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                     SupportActionBar.SetTitle(Resource.String.settingsDrawer);
                    break;
                case 1:
                    Toast.MakeText(ApplicationContext, "Naciśnięto Logout ", ToastLength.Long).Show();
                    //function to logout
                    //change activity to sing in / sing up activity
                    LogInModule lg = new LogInModule();
                    lg.setFalseRemeberMe(); //set RememberMe to false - autoLogin to NoAutoLogin

                    Intent intent = new Intent(this, typeof(MainActivity)); // i tutaj trzeba dodać że jak się wylogowuje to wpisuje w plik LogInData.xml false wtedy będzie sie mogło wrzucić okno do logowania
                    this.StartActivity(intent);
                    this.Finish();
                    break;
                default:
                    Toast.MakeText(ApplicationContext, "Naciśnięto coś z poza!", ToastLength.Long).Show();
                    break;
            }
            //Toast.MakeText(ApplicationContext, "Naciśnięto", ToastLength.Long).Show();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");

            }else
            {
                outState.PutString("DrawerState", "Closed");
            }
            base.OnSaveInstanceState(outState);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

    }
}
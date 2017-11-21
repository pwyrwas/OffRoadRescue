using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using google = Android.Gms.Maps;
using ap = Android.App;
using Android.Gms.Maps;
//using OFFroadRescue.Resources.Class;



namespace OFFroadRescue.Resources.Fragments
{
    public class MainPage : Fragment
    {
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                // Use this to return your custom view for this Fragment
                // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
                View view = inflater.Inflate(Resource.Layout.mainPage, container, false);

            return view;
            }

        

    }
}
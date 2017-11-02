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

using System.Threading;
using System.Net;
using System.Collections.Specialized;

namespace OFFroadRescue.Resources.Class
{
    class ServerConnectionManager
    {
        String serverPath = "http://www.offroadresque.eu/";
        String registrationPHP = "registration.php";
        String loginPHP = "login.php";

        WebClient client;

        public bool LogIn()
        {
            Uri uri = new Uri(serverPath+loginPHP);
            NameValueCollection parameters = new NameValueCollection();
            return true;
        }
    }
}
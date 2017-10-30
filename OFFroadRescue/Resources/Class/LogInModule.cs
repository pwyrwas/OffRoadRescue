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

namespace OFFroadRescue.Resources.Class
{
    class LogInModule
    {
        public string sLogIn;
        private string sPassword;
        public bool blogInState = false;

        // Sign to sLogIn and sPassword new values
        public LogInModule(string login, string passowrd)
        {
            sLogIn = login;
            sPassword = passowrd;
        }
        // Prepare login process
        public void LogIn()
        {
            blogInState = true;
            
        }
        // get Login state
        public bool GetLogInState() => blogInState;
        // get actual user name     
        public string GetUserName() => sLogIn;
        // Change user name
        public bool UpDateUserName(string newLogin)
        {
            if (true)
            {
                sLogIn = newLogin;
                return true;
            }
            else
                return false;
        }
        // Change user password
        public bool UpDateUserPassword(string oldPassowrd, string newPassword1, string newPassword2)
        {
            if (newPassword1 == newPassword2)
            {
                sPassword = newPassword1;
                return true;
            }
            else
                return false;
        }
    }
} 
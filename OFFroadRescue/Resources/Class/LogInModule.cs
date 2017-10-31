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
using System.Xml;

namespace OFFroadRescue.Resources.Class
{
    class LogInModule
    {
        public string sLogIn { get; set; }
        private string sPassword { get; set; }
        private bool brememberMe { get; set; }
        public bool blogInState { get; set; }


        // Sign to sLogIn and sPassword new values
        public LogInModule(string login, string passowrd, bool rememberMe)
        {
            sLogIn = login;
            sPassword = passowrd;
            brememberMe = rememberMe;
        }

        // Prepare login process
        public bool LogIn()
        {
            blogInState = true;
            if (brememberMe)
                saveLoginData();
            return true;
        }
        public bool saveLoginData()
        {
            var doc = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            XmlDocument xml = new XmlDocument();
            string tempDoc = doc;
            doc = System.IO.Path.Combine(doc, "LogInfile.xml");
            bool state = System.IO.File.Exists(doc);
            if (!state)
            {
                try
                {
                    System.IO.File.Create(doc).Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
              
            }
            XmlElement el = (XmlElement)xml.AppendChild(xml.CreateElement("Data"));
            el.SetAttribute("Data1", "Data2");
            xml.Save(doc);
            string text = xml.InnerXml.ToString();

            state = System.IO.File.Exists(doc);
            string[] listaPlikow = System.IO.Directory.GetFiles(tempDoc);

            

            return true;
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
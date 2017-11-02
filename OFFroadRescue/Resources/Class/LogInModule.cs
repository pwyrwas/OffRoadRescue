﻿using System;
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
        public LogInModule()
        {
            //empty constructor
        }
        public void AddUserParams(string login, string passowrd, bool rememberMe)
        {
            sLogIn = login;
            sPassword = passowrd;
            brememberMe = rememberMe;
        }

        // Prepare login process
        public bool LogIn()
        {
            bool state = false;
            blogInState = true;
            if (brememberMe)
                saveLoginData();

            state = true;
            return state;
        }
        public bool saveLoginData()
        {
            var doc = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            XmlDocument xml = new XmlDocument();
           
            string tempDoc = doc;
            doc = System.IO.Path.Combine(doc, "LogInfile.xml");
            string[] listaPlikow = System.IO.Directory.GetFiles(tempDoc);
            System.IO.File.Delete(doc);
            //tutaj sprawdzać czy istnieje plik xml jeśli nie to stworzyć, zaktualizować dane lub utworzyć nowe, hasło w md5 (tak myślę) oraz login. i Opcja czy auto pamiętać cię.
            bool state = System.IO.File.Exists(doc);
            if (!state)
            {
                try
                {
                    System.IO.File.Create(doc).Dispose();

                    //add XML declaration
                    XmlNode docNode = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
                    xml.AppendChild(docNode);
                    XmlElement login = (XmlElement)xml.AppendChild(xml.CreateElement("LogInData"));
                   
                    login.SetAttribute("RememberMe", "true");
                    //User
                    XmlElement user = xml.CreateElement("User");
                    user.InnerText = sLogIn;
                    login.AppendChild(user);

                    //Password
                    XmlElement passwd = xml.CreateElement("Password");
                    passwd.InnerText = sPassword;
                    login.AppendChild(passwd);
                    /*
                     * <LoginData RememberMe = "true">
                     *      <User>LOGIN</User>
                     *      <Password>HASLO</Password>
                     * </LoginData>
                     */
                    xml.Save(doc);
                    string text = xml.InnerXml.ToString();
                    Console.WriteLine(text);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
            }
            else
            {
                try
                {
                    xml.Load(doc);
                    XmlNodeList xmlN = xml.SelectNodes("LogInData");
                    foreach (XmlNode element in xmlN)
                    {
                        foreach (XmlElement el in element)
                        {
                            if (el.Name == "User")
                                el.InnerText = sLogIn;
                            if (el.Name == "Password")
                                el.InnerText = sLogIn;
                        }
                    }
                    // Get the values of child elements of a book.
                    xml.Save(doc);
                }catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
            }
            //probably if we are in this place, everything is okay, and we can return possitive ;) - I think.... ;P - don't have idea to resolve it on another way.
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
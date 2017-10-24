using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using Android.Content;

namespace OFFroadRescue
{
    [Activity(Label = "OFFroadRescue", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;
        private Button mBtnSignIn;
        private string login;
        private string password1;
        private string password2;
        private string email;

        private ProgressBar mProgressBar;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mBtnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            mBtnSignUp.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_SignUp signUpDialog = new dialog_SignUp();
                signUpDialog.Show(transaction, "dialog fragment");

                signUpDialog.mOnSignUpComplete += singUpDialog_mOnSingUpComplete;
                
            };
            mBtnSignIn.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_sign_in signInDialog = new dialog_sign_in();
                signInDialog.Show(transaction, "dialog fragment");
                signInDialog.mOnSignInComplete += singInDialog_mOnSingInComplete;

            };
            }
        bool tryLogIn()
        {
            WebClient client = new WebClient();
            Uri uri = new Uri("http://www.offroadresque.eu/first.php");
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("imie","asdf");
            parameters.Add("test", "test");

            client.UploadValuesCompleted += client_UploadValuesCompleted;
            client.UploadValuesAsync(uri, parameters);

            return true;
        }

        void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            Console.WriteLine("Done!");
            Toast.MakeText(ApplicationContext, "Info!", ToastLength.Long).Show();
           
        }

        void singUpDialog_mOnSingUpComplete(object sender, OnSignUpEvenArgs e)
        {
          //  tryLogIn();
            //tutaj proces logowania
        /*    WebClient client = new WebClient();
            Uri uri = new Uri("http://www.offroadresque.eu/registration.php");
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("rejestruj", "rejestruj");
            parameters.Add("login", "Pawel");
            parameters.Add("haslo1", "orangepl1");
            parameters.Add("haslo2", "orangepl1");
            parameters.Add("email", "kudlaty951@gmail.com");

            client.UploadValuesCompleted += client_UploadValuesCompleted;
            client.UploadValuesAsync(uri, "POST",parameters);

            It's works properly, need to modify. 
          */  
            //Console.WriteLine(e.FirstName.ToString()); <- it's access to this data
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            Thread thread = new Thread(actLikeRequest);
            thread.Start();
            
            
        }
        void singInDialog_mOnSingInComplete(object sender, OnSignInEvenArgs e)
        {
                 bool loginStatus = tryLogIn();
                 //set mainView Layout - mainView layout should be the main layout our
                 //application after login. 
                 if(loginStatus)
                 {
                     Intent intent = new Intent(this, typeof(mainView));
                     this.StartActivity(intent);
                     this.Finish();
                 }
                 else
                 {
                     //infrmation about error during singIn process.
                 }

        }
        private void actLikeRequest() //request to database in the future
        {
           

            RunOnUiThread(() => { mProgressBar.Visibility = Android.Views.ViewStates.Invisible; });
        }
    }
}


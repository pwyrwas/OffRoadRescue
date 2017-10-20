using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using System.Threading;
using System.Net;
using System.Collections.Specialized;

namespace OFFroadRescue
{
    [Activity(Label = "OFFroadRescue", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;
        private Button mBtnSignIn;
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
            };
            }
        bool tryLogIn()
        {
            WebClient client = new WebClient();
            Uri uri = new Uri("http://www.offroadresque.eu/first.php");
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("imie","Pawel Sroka");

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
            //Console.WriteLine(e.FirstName.ToString()); <- it's access to this data
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            Thread thread = new Thread(actLikeRequest);
            thread.Start();
            
        }
        private void actLikeRequest() //request to database in the future
        {
            Thread.Sleep(3000);

            RunOnUiThread(() => { mProgressBar.Visibility = Android.Views.ViewStates.Invisible; });
        }
    }
}


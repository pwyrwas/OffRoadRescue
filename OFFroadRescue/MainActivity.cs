using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using Android.Content;
using Android.Graphics;


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
        bool singInStatus = false;
        dialog_SignUp signUpDialog;

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
                signUpDialog = new dialog_SignUp();

                
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
            //color to sign wrong wrote filed
            Color colorWrong = Color.ParseColor("#FFCDD2");             //red
            Color colorGood = Color.ParseColor("#ffffff");              //white
            Color colorAllGood = Color.ParseColor("#64FFDA");           //Green

            try
            {
                EditText mtxtFirstName = signUpDialog.View.FindViewById<EditText>(Resource.Id.txtFirstName);
                EditText mTxtEmail = signUpDialog.View.FindViewById<EditText>(Resource.Id.txtEmail);
                Button mBtnSignUp = signUpDialog.View.FindViewById<Button>(Resource.Id.btnDialogEmail);
                EditText mTxtPassword = signUpDialog.View.FindViewById<EditText>(Resource.Id.txtPassword);
                string result = System.Text.Encoding.UTF8.GetString(e.Result);

                Toast.MakeText(ApplicationContext, result, ToastLength.Long).Show();

                if (result == "All fields must be filled in")
                {
                    mtxtFirstName.SetBackgroundColor(colorWrong);
                    mTxtPassword.SetBackgroundColor(colorWrong);
                    mTxtEmail.SetBackgroundColor(colorWrong);
                }
                else
                {
                    mtxtFirstName.SetBackgroundColor(colorGood);
                    mTxtPassword.SetBackgroundColor(colorGood);
                    mTxtEmail.SetBackgroundColor(colorGood);
                }

                if (result == "Email Adress are incorrect!")
                    mTxtEmail.SetBackgroundColor(colorWrong);
                else if (result != "All fields must be filled in")
                    mTxtEmail.SetBackgroundColor(colorGood);
                if (result == "This login is arleady in use" && result != "All fields must be filled in")
                    mtxtFirstName.SetBackgroundColor(colorWrong);
                else if (result != "All fields must be filled in")
                    mtxtFirstName.SetBackgroundColor(colorGood);
                if (result == "Your password should have minmum 5 letters" && result != "All fields must be filled in")
                    mTxtPassword.SetBackgroundColor(colorWrong);
                else if (result != "All fields must be filled in")
                    mTxtPassword.SetBackgroundColor(colorGood);
                if (result == "Login should have minimum 4 sign" && result != "All fields must be filled in")
                    mtxtFirstName.SetBackgroundColor(colorWrong);
                else if (result != "All fields must be filled in")
                    mtxtFirstName.SetBackgroundColor(colorGood);


                //at least
                if (result == "Account was created!")
                {
                    mtxtFirstName.SetBackgroundColor(colorAllGood);
                    mTxtPassword.SetBackgroundColor(colorAllGood);
                    mTxtEmail.SetBackgroundColor(colorAllGood);
                    //don't knwo why colors not set at #64FFDA after all good field
                    singInStatus = true;

                    Thread.Sleep(1000);
                    signUpDialog.Dismiss();
                    singInStatus = false;
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        void singUpDialog_mOnSingUpComplete(object sender, OnSignUpEvenArgs e)
        {
            
            createAccount("rejestruj", e.FirstName, e.Password, e.Password, e.Email);
            //tutaj proces logowania

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
        void createAccount(string registrate, string s_login, string s_password1, string s_password2, string s_email)
        {

            WebClient client = new WebClient();
            Uri uri = new Uri("http://www.offroadresque.eu/registration.php");
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("rejestruj", registrate);
            parameters.Add("login", s_login);
            parameters.Add("haslo1", s_password1);
            parameters.Add("haslo2", s_password2);
            parameters.Add("email", s_email);

            client.UploadValuesCompleted += client_UploadValuesCompleted;
            client.UploadValuesAsync(uri, "POST", parameters);

        }
        private void actLikeRequest() //request to database in the future
        {
            //now it is for nothing ;)
            RunOnUiThread(() => { mProgressBar.Visibility = Android.Views.ViewStates.Invisible; });
        }
       
    }
}


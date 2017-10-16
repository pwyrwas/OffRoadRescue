using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace OFFroadRescue
{

    class dialog_SignUp : DialogFragment
    {
        private EditText mtxtFirstName;
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private Button mBtnSignUp;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);

            mtxtFirstName = view.FindViewById<EditText>(Resource.Id.txtFirstName);
            mTxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mBtnSignUp = view.FindViewById<Button>(Resource.Id.btnDialogEmail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);

            mBtnSignUp.Click += mBtnSignUp_Click;

            return view;
        }
        void mBtnSignUp_Click(object sender, EventArgs e)
        {
            //User has clicked the sign up button 
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //Sets the tite bar to invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; // set the animation
        }
    }
}
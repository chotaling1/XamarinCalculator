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
using XamarinApp;
using XamarinApp.Views.Activities;

namespace XamarinApp.Views.Fragments
{
    public class Login : Fragment
    {
        bool DEMO = true;
        string demoUser = "chotaling", demoPassword = "Test1111";
        private View view = null;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.login_fragment, container, false);
            Button submitButton = view.FindViewById(Resource.Id.submitButton) as Button;
            submitButton.Click += OnSubmitButtonClick;

            return view;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        private void OnSubmitButtonClick(object sender, EventArgs eventArgs)
        {
            EditText login = view.FindViewById(Resource.Id.loginEdit) as EditText;
            EditText password = view.FindViewById(Resource.Id.passwordEdit) as EditText;
            string loginInput = login.Text;
            string passwordInput = password.Text;
            if (DEMO)
            {
                if (loginInput.Equals(demoUser) && passwordInput.Equals(demoPassword))
                {
                    StartActivity(new Intent(this.Activity, typeof(CalculatorActivity)));
                }
            }


        }
    }
}
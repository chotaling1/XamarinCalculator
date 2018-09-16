using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using XamarinApp;

namespace XamarinApp.Views.Activities
{

    public class BaseActivity : AppCompatActivity
    {
        private static String _ctx = "BaseActivity";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public void display(String frg, String tag)
        {

            FragmentManager fm = FragmentManager;
            FragmentTransaction ft = fm.BeginTransaction();
            Fragment fragment = fm.FindFragmentByTag(tag);
            if (fragment != null)
            {
                Log.Info(_ctx, String.Format("Attach Fragment {0} with Tag {1}", frg, tag));
                ft.Attach(fragment);
            }
            else
            {
                try
                {
                    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    var type = assembly.GetTypes().First(t => t.Name == frg);
                    fragment = (Fragment)Activator.CreateInstance(type);
                    Log.Info(_ctx, String.Format("Replacing current fragment with {0}", frg));
                    ft.Replace(Resource.Id.fragmentContainer, fragment);
                }
                catch (Exception e)
                {
                    Log.Error(_ctx, String.Format("display: {0}", e.ToString()));
                }
            }

            ft.Commit();
        }
    }
}


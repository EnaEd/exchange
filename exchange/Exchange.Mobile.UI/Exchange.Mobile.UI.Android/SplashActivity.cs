
using Android.App;
using Android.Support.V7.App;

namespace Exchange.Mobile.UI.Droid
{
    [Activity(Icon = "@drawable/logo", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}

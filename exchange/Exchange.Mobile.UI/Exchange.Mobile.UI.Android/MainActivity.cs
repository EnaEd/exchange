
using Android.App;
using Android.Content.PM;
using Android.OS;
using Exchange.Mobile.Core.Services.Interfaces;
using Exchange.Mobile.UI.Droid.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;

namespace Exchange.Mobile.UI.Droid
{
    [Activity(Label = "Exchange.Mobile.UI", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<Core.App, UI.App>, Core.App, UI.App>
    {

        public override void InitializeForms(Bundle bundle)
        {
            Mvx.IoCProvider.RegisterSingleton<IDeviceInfoService>(() => new DeviceInfoService());
            Mvx.IoCProvider.RegisterSingleton<IDisplayAlertService>(() => new DisplayAlertService());

        }

    }

}

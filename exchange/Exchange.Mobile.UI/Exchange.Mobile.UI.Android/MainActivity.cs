using Android.App;
using Android.Content.PM;
using Android.OS;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Exchange.Mobile.Core.Services.Interfaces;
using Exchange.Mobile.UI.Droid.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using Plugin.CurrentActivity;
using System.Collections.Generic;

namespace Exchange.Mobile.UI.Droid
{
    [Activity(Label = "Exchange.Mobile.UI", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<Core.App, UI.App>, Core.App, UI.App>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            Forms9Patch.Droid.Settings.Initialize(this);
            CrossCurrentActivity.Current.Init(this, bundle);

            OneSignal.Current.StartInit("YOUR_ONESIGNAL_APP_ID")
            .Settings(new Dictionary<string, bool>() {
               { IOSSettings.kOSSettingsKeyAutoPrompt, false },
               { IOSSettings.kOSSettingsKeyInAppLaunchURL, false } })
            .InFocusDisplaying(OSInFocusDisplayOption.Notification)
            .EndInit();


            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();
        }

        public override void InitializeForms(Bundle bundle)
        {
            Mvx.IoCProvider.RegisterSingleton<IDeviceInfoService>(() => new DeviceInfoService());
            Mvx.IoCProvider.RegisterSingleton<IDisplayAlertService>(() => new DisplayAlertService());
            //Mvx.IoCProvider.RegisterSingleton<INotification>(() => new NotificationHelper());

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }

}

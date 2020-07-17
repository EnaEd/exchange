using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Exchange.Mobile.Core.Helpers.Interface;
using Exchange.Mobile.Core.Services.Interfaces;
using Exchange.Mobile.UI.Droid.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using Plugin.CurrentActivity;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Exchange.Mobile.UI.Droid
{
    [Activity(Label = "Exchange.Mobile.UI", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<Core.App, UI.App>, Core.App, UI.App>
    {

        internal static Context ActivityContext { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            //global exception handler
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            base.OnCreate(bundle);




            Xamarin.Essentials.Platform.Init(this, bundle);

            DisplayCrashReport();

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            //Forms9Patch.Droid.Settings.Initialize(this);
            CrossCurrentActivity.Current.Init(this, bundle);

            //Permissions.RequestAsync<PermissionRequest>();
        }


        private void DisplayCrashReport()
        {
            const string errorFilename = "Fatal.log";
            var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var errorFilePath = Path.Combine(libraryPath, errorFilename);

            if (!File.Exists(errorFilePath))
            {
                return;
            }

            var errorText = File.ReadAllText(errorFilePath);
            new AlertDialog.Builder(this)
                .SetPositiveButton("Clear", (sender, args) =>
                {
                    File.Delete(errorFilePath);
                })
                .SetNegativeButton("Close", (sender, args) =>
                {
                    // User pressed Close.
                })
                .SetMessage(errorText)
                .SetTitle("Crash Report")
                .Show();
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", e.Exception);
            LogUnhandledException(newExc);

        }

        private void LogUnhandledException(Exception newExc)
        {
            try
            {
                const string errorFileName = "Fatal.log";
                var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var errorFilePath = Path.Combine(libraryPath, errorFileName);
                var errorMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
                DateTime.Now, newExc.ToString());
                File.WriteAllText(errorFilePath, errorMessage);

                // Log to Android Device Logging.
                Android.Util.Log.Error("Crash Report", errorMessage);
            }
            catch
            {
                // just suppress any error logging exceptions
                DisplayCrashReport();
            }
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", e.ExceptionObject as Exception);
            LogUnhandledException(newExc);
        }

        public override void InitializeForms(Bundle bundle)
        {
            Mvx.IoCProvider.RegisterSingleton<IDeviceInfoService>(() => new DeviceInfoService());
            Mvx.IoCProvider.RegisterSingleton<IDisplayAlertService>(() => new DisplayAlertService());
            Mvx.IoCProvider.RegisterSingleton<INativeLocationService>(() => new NativeLocationService());
            Mvx.IoCProvider.RegisterSingleton<INativeLocationService>(() => new NativeLocationService());

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }

}

using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Exchange.Mobile.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();


            OneSignal.Current.StartInit("f771fa6f-0e28-4ec1-980d-c700e13b6383")
            .Settings(new Dictionary<string, bool>() {
             { IOSSettings.kOSSettingsKeyAutoPrompt, false },
             { IOSSettings.kOSSettingsKeyInAppLaunchURL, false } })
            .InFocusDisplaying(OSInFocusDisplayOption.Notification)
            .EndInit();

            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

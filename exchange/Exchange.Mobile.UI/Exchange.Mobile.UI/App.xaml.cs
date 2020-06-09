using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Exchange.Mobile.Core.ViewModels;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
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
            //.HandleNotificationOpened(HandleNotificationOpened)
            //.HandleNotificationReceived(HandleNotificationReceived)
            .EndInit();

            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();



        }

        private void HandleNotificationReceived(OSNotification notification)
        {
            OSNotificationPayload payload = notification.payload;
            string message = payload.body;
        }

        private async void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            var navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            OSNotificationPayload payload = result.notification.payload;
            string message = payload.body;

            await navigationService.ChangePresentation(new MvxPagePresentationHint(typeof(OfferDiscussViewModel)));//<OfferDiscussViewModel, object>(message);


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

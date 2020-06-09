using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Exchange.Mobile.Core.Services.Interfaces;
using Exchange.Mobile.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
using System.Collections.Generic;

namespace Exchange.Mobile.Core.Services
{
    public class OneSignalService : IOneSignalService
    {
        private IMvxNavigationService _navigationService;
        public OneSignalService(IMvxNavigationService mvxNavigationService)
        {
            _navigationService = mvxNavigationService;
            OneSignal.Current.StartInit("f771fa6f-0e28-4ec1-980d-c700e13b6383")
            .Settings(new Dictionary<string, bool>() {
             { IOSSettings.kOSSettingsKeyAutoPrompt, false },
             { IOSSettings.kOSSettingsKeyInAppLaunchURL, false } })
            .InFocusDisplaying(OSInFocusDisplayOption.Notification)
            .HandleNotificationOpened(HandleNotificationOpened)
            .HandleNotificationReceived(HandleNotificationReceived)
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
            OSNotificationPayload payload = result.notification.payload;
            string message = payload.body;

            await _navigationService.ChangePresentation(new MvxPagePresentationHint(typeof(OfferDiscussViewModel)));//<OfferDiscussViewModel, object>(message);


        }
    }
}

using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly IAuthService<User> _authService;
        private readonly IMvxNavigationService _navigationService;
        private readonly IDeviceInfoService _deviceInfoService;

        public AuthViewModel(IAuthService<User> authService, IMvxNavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
            _deviceInfoService = Mvx.IoCProvider.Resolve<IDeviceInfoService>();

            //string number = _deviceInfoService.GetPhoneNumber();
            string number = "111111111";
            string pushId = string.Empty;

            OneSignal.Current.IdsAvailable(new IdsAvailableCallback((id, token) =>
            {
                pushId = id;
            }));
            //var notification = new Dictionary<string, object>();
            //notification["contents"] = new Dictionary<string, string>() { { "en", "Test message" } };
            //notification["include_external_user_ids"] = new List<string>() { "6" };
            //OneSignal.Current.PostNotification(notification,
            //    (responseSuccess) => { Debug.WriteLine("success"); },
            //    (responseFailure) => { Debug.WriteLine($"{Json.Serialize(responseFailure)}"); });

            Device.InvokeOnMainThreadAsync(async () =>
        {
            if (await _authService.CheckUserPhone(number))
            {

                await _authService.UpdatePushIdIfNeededAsync(pushId);
                await _navigationService.Navigate<OfferViewModel>();
                return;
            }
            //var model = new PhoneRequestModel { PhoneNumber = number };
            await _navigationService.Navigate<RegistrationViewModel, string>(number);

        });
        }

        private static void OneSignalSetExternalUserId(Dictionary<string, object> results)
        {
            // The results will contain push and email success statuses
            Debug.WriteLine("External user id updated with results: " + Json.Serialize(results));
            // Push can be expected in almost every situation with a success status, but
            // as a pre-caution its good to verify it exists
            if (results.ContainsKey("push"))
            {
                Dictionary<string, object> pushStatusDict = results["push"] as Dictionary<string, object>;
                if (pushStatusDict.ContainsKey("success"))
                {
                    Debug.WriteLine("External user id updated for push with results: " + pushStatusDict["success"] as string);
                }
            }
            // Verify the email is set or check that the results have an email success status
            if (results.ContainsKey("email"))
            {
                Dictionary<string, object> emailStatusDict = results["email"] as Dictionary<string, object>;
                if (emailStatusDict.ContainsKey("success"))
                {
                    Debug.WriteLine("External user id updated for email with results: " + emailStatusDict["success"] as string);
                }
            }
        }
    }
}

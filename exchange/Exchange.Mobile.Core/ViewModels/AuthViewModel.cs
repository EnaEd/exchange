using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Navigation;
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

                //await _authService.UpdatePushIdIfNeededAsync(pushId);
                await _navigationService.Navigate<OfferViewModel>();
                return;
            }
            //var model = new PhoneRequestModel { PhoneNumber = number };
            await _navigationService.Navigate<RegistrationViewModel, string>(number);

        });
        }

    }
}

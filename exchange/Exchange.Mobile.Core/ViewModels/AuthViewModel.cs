using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.ViewModels
{
    public class AuthViewModel : MvxViewModel
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
            Device.InvokeOnMainThreadAsync(async () =>
            {
                if (await _authService.CheckUserPhone(number))
                {
                    await _navigationService.Navigate<OfferViewModel>();
                    return;
                }
                //var model = new PhoneRequestModel { PhoneNumber = number };
                await _navigationService.Navigate<RegistrationViewModel, string>(number);

            });
        }
    }
}

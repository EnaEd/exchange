using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Navigation;
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


            Device.InvokeOnMainThreadAsync(async () =>
            {

                try
                {
                    if (await _authService.CheckUserPhone(number))
                    {

                        await _authService.UpdatePushIdIfNeededAsync(number, pushId);
                        await _navigationService.Navigate<OfferViewModel>();
                        return;
                    }
                    //var model = new PhoneRequestModel { PhoneNumber = number };
                    await _navigationService.Navigate<RegistrationViewModel, string>(number);
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    //TODO EE:handle expetion
                }

            });
        }

    }
}

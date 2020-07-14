using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross.Navigation;
using System.Diagnostics;

namespace Exchange.Mobile.Core.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly IAuthService<User> _authService;
        private readonly IMvxNavigationService _navigationService;



        public AuthViewModel(IAuthService<User> authService, IMvxNavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
        }
        public async override void ViewAppearing()
        {
            PhoneNumber = DeviceInfoService.GetPhoneNumber();




            OneSignal.Current.IdsAvailable(new IdsAvailableCallback((id, token) =>
            {
                SignalId = id;
            }));




            try
            {
                if (await _authService.CheckUserPhone(PhoneNumber))
                {

                    await _authService.UpdatePushIdIfNeededAsync(PhoneNumber, SignalId);
                    await _navigationService.Navigate<MainTabbedViewModel>();
                    return;
                }

                await _navigationService.Navigate<RegistrationViewModel>();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //TODO EE:handle expetion
            }

            base.ViewAppearing();
        }

    }
}

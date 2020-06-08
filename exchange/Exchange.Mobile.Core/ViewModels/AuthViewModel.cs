using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross.Navigation;
using System.Diagnostics;
using Xamarin.Forms;

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

            string pushId = string.Empty;

            OneSignal.Current.IdsAvailable(new IdsAvailableCallback((id, token) =>
            {
                pushId = id;
            }));


            Device.InvokeOnMainThreadAsync(async () =>
            {

                try
                {
                    if (await _authService.CheckUserPhone(PhoneNumber))
                    {

                        await _authService.UpdatePushIdIfNeededAsync(PhoneNumber, pushId);
                        await _navigationService.Navigate<MainTabbedViewModel>();
                        return;
                    }
                    //var model = new PhoneRequestModel { PhoneNumber = number };
                    await _navigationService.Navigate<RegistrationViewModel, string>(PhoneNumber);
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

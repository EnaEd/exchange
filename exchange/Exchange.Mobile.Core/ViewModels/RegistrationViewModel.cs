using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.ViewModels
{
    public class RegistrationViewModel : MvxViewModel<string>
    {
        private readonly IAuthService<User> _authService;
        private readonly IMvxNavigationService _navigationService;
        private readonly IDisplayAlertService _displayAlertService;
        public RegistrationViewModel(IAuthService<User> authService, IMvxNavigationService navigationService, IDisplayAlertService displayAlertService)
        {
            _authService = authService;
            _navigationService = navigationService;
            _displayAlertService = displayAlertService;
        }

        #region Properties

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        private string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _country;
        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        private long _id;
        public long Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        #endregion Properties

        #region Commands
        IMvxAsyncCommand ConfirmRegistrationCommandAsync => new MvxAsyncCommand(ConfirmRegistrationAsync);

        private async Task ConfirmRegistrationAsync()
        {
            //TODO validate registration fields
            User user = new User();
            user.City = City;
            user.Country = Country;
            user.Email = Email;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Password = LastName;
            user.PhoneNumber = Phone;

            bool isRegistrationSuccess = await _authService.RegistrationAsync(user);
            if (isRegistrationSuccess)
            {
                await _navigationService.Navigate<OfferViewModel>();
                return;
            }
            _displayAlertService.ShowToast("registration not successfull");

        }

        public override void Prepare(string phoneNumber)
        {
            Phone = phoneNumber;
        }
        #endregion Commands
    }
}

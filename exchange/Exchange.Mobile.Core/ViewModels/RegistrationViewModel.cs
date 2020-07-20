using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Helpers.Interface;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Services.Interfaces;
using Exchange.Mobile.Core.Validations;
using FluentValidation.Results;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Exchange.Mobile.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly IAuthService<User> _authService;
        private readonly IMvxNavigationService _navigationService;
        private readonly IDisplayAlertService _displayAlertService;
        private readonly ILocationHelper _locationHelper;
        public RegistrationViewModel(IAuthService<User> authService, IMvxNavigationService navigationService,
            IDisplayAlertService displayAlertService, ILocationHelper locationHelper)
        {
            _authService = authService;
            _navigationService = navigationService;
            _displayAlertService = displayAlertService;
            _locationHelper = locationHelper;
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
        public IMvxAsyncCommand ConfirmRegistrationCommandAsync => new MvxAsyncCommand(ConfirmRegistrationAsync);


        #endregion Commands

        #region Functionality
        private async Task ConfirmRegistrationAsync()
        {

            await GetLocationData();

            User user = new User
            {
                City = City,
                Country = Country,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Phone = PhoneNumber,
                OneSignalId = SignalId
            };

            //validation before send request
            UserValidator uservalidator = new UserValidator();
            ValidationResult result = uservalidator.Validate(user);
            if (!result.IsValid)
            {
                string errors = result.Errors.First().ToString();
                _displayAlertService.ShowToast(errors);
                return;
            }

            if ((await _authService.RegistrationAsync(user)).Equals(Constant.Shared.REGISTRATION_SUCCESS))
            {
                await _navigationService.Navigate<MainTabbedViewModel>();
                return;
            }
            _displayAlertService.ShowToast(Constant.Shared.REGISTRATION_FAIL);

        }

        private async Task GetLocationData()
        {
            var position = await _locationHelper.GetPositionAsync(TimeSpan.FromMilliseconds(10000));
            if (position is null)
            {
                _displayAlertService.ShowToast(Constant.Shared.REQUEST_LOCATION_FAIL);
                return;
            }
            var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            if (placemark is null)
            {
                _displayAlertService.ShowToast(Constant.Shared.REQUEST_LOCATION_FAIL);
            }
            City = placemark.Locality;
            Country = placemark.CountryName;
        }

        #endregion Functionality
    }
}

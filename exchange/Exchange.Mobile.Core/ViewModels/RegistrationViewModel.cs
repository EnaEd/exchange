﻿using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Helpers.Interface;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.GooglesModels;
using Exchange.Mobile.Core.Models.ResponseModels;
using Exchange.Mobile.Core.Services.Interfaces;
using Exchange.Mobile.Core.Validations;
using FluentValidation.Results;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
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
        private readonly IAuthyService _authyService;
        private readonly IGoogleMapsApiService _googleMapsApiService;
        public RegistrationViewModel(IAuthService<User> authService, IMvxNavigationService navigationService,
            IDisplayAlertService displayAlertService, ILocationHelper locationHelper,
            IAuthyService authyService, IGoogleMapsApiService googleMapsApiService)
        {
            _authService = authService;
            _navigationService = navigationService;
            _displayAlertService = displayAlertService;
            _locationHelper = locationHelper;
            _authyService = authyService;
            _googleMapsApiService = googleMapsApiService;
        }

        #region Properties

        private bool _isAutoCompleteVisible;
        public bool IsAutoCompleteVisible
        {
            get => _isAutoCompleteVisible;
            set => SetProperty(ref _isAutoCompleteVisible, value);
        }

        public IList<GooglePlaceAutoCompletePrediction> Places { get; set; }

        private string _searchLocation;
        public string SearchLocation
        {
            get => _searchLocation;
            set
            {
                if (SetProperty(ref _searchLocation, value))
                {
                    GetPlacesCommandAsync.Execute(_searchLocation);
                }
            }
        }
        private GooglePlaceAutoCompletePrediction _currentSearchLocation;
        public GooglePlaceAutoCompletePrediction CurrentSearchLocation
        {
            get => _currentSearchLocation;
            set => SetProperty(ref _currentSearchLocation, value);
        }

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

        private long _authyId;
        public long AuthyId
        {
            get => _authyId;
            set => SetProperty(ref _authyId, value);
        }

        private long _token;
        public long Token
        {
            get => _token;
            set => SetProperty(ref _token, value);
        }

        private string _countryCode;
        public string CountryCode
        {
            get => _countryCode;
            set => SetProperty(ref _countryCode, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        #endregion Properties

        #region Commands
        public IMvxAsyncCommand ConfirmRegistrationCommandAsync => new MvxAsyncCommand(ConfirmRegistrationAsync);
        public IMvxAsyncCommand VerifyCodeCommandAsync => new MvxAsyncCommand(VerifyCodeAsync);
        public IMvxCommand GetPlacesCommandAsync => new MvxAsyncCommand<string>(GetPlacesAsync);




        #endregion Commands

        #region Functionality

        private async Task GetPlacesAsync(string place)
        {
            var places = await _googleMapsApiService.GetPlaces(place);
            var placesResult = places.AutoCompletePlaces;
            if (!(placesResult is null) && placesResult.Count > default(int))
            {
                Places = new List<GooglePlaceAutoCompletePrediction>(placesResult);
                await RaisePropertyChanged(nameof(Places));
                IsAutoCompleteVisible = true;
            }
        }

        private async Task VerifyCodeAsync()
        {
            AuthyVerifyResponse response = await _authyService.VerifyTokenAsync(Token, AuthyId);
            if (!bool.Parse(response.Success))
            {
                DisplayAlertService.ShowToast(response.Message);
                return;
            }

            var area = await _googleMapsApiService.GetPlaceDetails(CurrentSearchLocation.PlaceId);

            User user = new User
            {
                City = area.Addresses.FirstOrDefault(address => address.Types.Contains("locality")).LongName,
                Country = area.Addresses.FirstOrDefault(address => address.Types.Contains("country")).LongName,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Phone = PhoneNumber,
                OneSignalId = SignalId
            };

            UserValidator uservalidator = new UserValidator();
            ValidationResult result = uservalidator.Validate(user);
            if (!result.IsValid)
            {
                string errors = result.Errors.First().ToString();
                _displayAlertService.ShowToast(errors);
                return;
            }

            if (!(await _authService.RegistrationAsync(user)).Equals(Constant.Shared.REGISTRATION_SUCCESS))
            {
                _displayAlertService.ShowToast(Constant.Shared.REGISTRATION_FAIL);
                return;
            }

            try
            {
                await SecureStorage.SetAsync(Constant.SecureConstant.PHONE_FIELD, PhoneNumber);
            }
            catch (Exception ex)
            {
                _displayAlertService.ShowToast(Constant.SecureConstant.FAIL_SAVE_PHONE_NUMBER);
            }
            await SecureStorage.SetAsync(Constant.SecureConstant.IS_AUTH, Constant.SecureConstant.IS_AUTH);
            await _navigationService.Navigate<MainTabbedViewModel>();

        }

        private async Task ConfirmRegistrationAsync()
        {
            User user = new User();
            user.Email = Email;
            user.Phone = PhoneNumber;
            user.CountryCode = CountryCode;
            AuthyResponseModel response = await _authyService.AddUserAsync(user);
            if (!response.Success)
            {
                DisplayAlertService.ShowToast(response.Message);
                return;
            }
            AuthyId = response.User.Id;
            AuthyOTPResponseModel otpResponse = await _authyService.SendOTPAsync(AuthyId);

            DisplayAlertService.ShowToast(otpResponse.Message);
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

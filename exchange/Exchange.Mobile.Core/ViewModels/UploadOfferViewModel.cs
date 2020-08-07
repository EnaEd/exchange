using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.GooglesModels;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.ViewModels
{
    public class UploadOfferViewModel : BaseViewModel
    {

        private readonly IDisplayAlertService _displayAlertService;
        private readonly IOfferService _offerService;
        private readonly IAuthService<User> _authService;
        private readonly IGoogleMapsApiService _googleMapsApiService;

        public UploadOfferViewModel(IDisplayAlertService displayAlertService, IOfferService offerService, IAuthService<User> authService, IGoogleMapsApiService googleMapsApiService)
        {
            _displayAlertService = displayAlertService;
            _offerService = offerService;
            _authService = authService;

            InvokeOnMainThreadAsync(async () => await GetOfferCategories(offerService));
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

        private OfferCategory _selectedCategory;
        public OfferCategory SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        private string _uploadedImageBase64;
        public string UploadedImageBase64
        {
            get => _uploadedImageBase64;
            set => SetProperty(ref _uploadedImageBase64, value);
        }

        private ImageSource _uploadedImageSource;
        public ImageSource UploadedImageSource
        {
            get => _uploadedImageSource;
            set => SetProperty(ref _uploadedImageSource, value);
        }

        private string _offerDescription;
        public string OfferDescription
        {
            get => _offerDescription;
            set => SetProperty(ref _offerDescription, value);
        }
        #endregion Properties

        #region Commands
        public IMvxCommand UploadImageCommandAsync => new MvxAsyncCommand(UploadImageAsync);
        public IMvxCommand UploadOfferCommandAsync => new MvxAsyncCommand(UploadOfferAsync);
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

        private async Task UploadOfferAsync()
        {
            var area = await _googleMapsApiService.GetPlaceDetails(CurrentSearchLocation.PlaceId);
            var city = area.Addresses.FirstOrDefault(address => address.Types.Contains("locality")).LongName;
            var country = area.Addresses.FirstOrDefault(address => address.Types.Contains("country")).LongName;
            var userPhone = await Xamarin.Essentials.SecureStorage.GetAsync(Constant.SecureConstant.PHONE_FIELD);

            var requestModel = new UploadOfferRequestModel();
            requestModel.OfferPhoto = UploadedImageBase64;
            requestModel.OfferDescription = OfferDescription;
            requestModel.OfferOwner = new User { Phone = userPhone, City = city, Country = country };

            var result = await _offerService.UploadOfferAsync(requestModel);
            DisplayAlertService.ShowToast($"{Constant.Shared.RESULT_REQUEST_MESSAGE} {result}");
            ToDefaultData();

        }

        private void ToDefaultData()
        {
            SelectedCategory = null;
            UploadedImageBase64 = null;
            UploadedImageSource = null;
            OfferDescription = null;
        }

        private async Task UploadImageAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable && !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }
            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = Constant.Shared.MEDIA_DIRECTORY,
                Name = $"{DateTime.Now.ToString(Constant.Shared.IMAGE_SAVING_FORMAT)}{Constant.Shared.IMAGE_EXTENSION}"
            });

            if (!(file is null))
            {
                UploadedImageSource = ImageSource.FromFile(file.Path);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    _uploadedImageBase64 = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
        #endregion Functionality
    }
}

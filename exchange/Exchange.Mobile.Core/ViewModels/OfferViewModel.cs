using Com.OneSignal;
using Com.OneSignal.Abstractions;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.ViewModels
{
    public class OfferViewModel : BaseViewModel
    {

        private readonly IOfferService _offerService;
        private readonly IAuthService<User> _authService;
        private readonly IDiscussOfferService _discussOfferService;
        private readonly IGoogleMapsApiService _googleMapsApiService;
        private int _showedCount;


        public OfferViewModel(IOfferService offerService, IAuthService<User> authService,
            IDiscussOfferService discussOfferService, IGoogleMapsApiService googleMapsApiService)
        {
            _googleMapsApiService = googleMapsApiService;
            _discussOfferService = discussOfferService;
            _offerService = offerService;
            _authService = authService;
            _showedCount = default;
            Device.InvokeOnMainThreadAsync(async () =>
            {
                await GetOfferCategories(_offerService);

                //await ShowOfferAsync();
            });
        }



        #region Commands
        public IMvxCommand SelectedCommandAsync => new MvxAsyncCommand<object>(SelectedItem);

        public IMvxCommand SendOfferCommandAsync => new MvxAsyncCommand(SendOfferAsync);

        public IMvxCommand UploadImageCommandAsync => new MvxAsyncCommand(UploadImageAsync);

        public IMvxCommand RefreshOffersCommandAsync => new MvxAsyncCommand(RefreshOffersAsync);

        public IMvxCommand ChangeConditionCommand => new MvxCommand(ChangeCondition);



        public IMvxCommand GetPlacesCommandAsync => new MvxAsyncCommand<string>(GetPlacesAsync);



        #endregion Commands

        #region Functionality

        private void ChangeCondition()
        {
            CurrentCategory = null;
        }

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


        private async Task RefreshOffersAsync()
        {
            _showedCount = default;
            await ShowOfferAsync(CurrentCategory);
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
                UploadedImage = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }

        }


        public async Task SendOfferAsync()
        {
            //string number = _deviceInfoService.GetPhoneNumber();
            var user = await _authService.GetUserByIdAsync(CurrentOfferCard.OwnerId ?? default);

            DiscussOfferModel result = await CreateDiscussModel(user);

            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    var response = await _discussOfferService.CreateDiscussOfferAsync(result);
                }

                //create push 
                var notification = new Dictionary<string, object>
                {
                    ["contents"] = new Dictionary<string, string>() {
                    { "en", "You have a new exchange offer" }
                },
                    ["include_player_ids"] = new List<string>() { user.OneSignalId }
                };
                OneSignal.Current.PostNotification(notification,
                    (responseSuccess) => { DisplayAlertService.ShowToast("success"); },
                    (responseFailure) => { DisplayAlertService.ShowToast($"{Json.Serialize(responseFailure)}"); Debug.WriteLine(Json.Serialize(responseFailure)); });
                Conditions = default;
            }
            catch (Exception)
            {

                DisplayAlertService.ShowToast(Constant.Shared.FAIL_TO_CREATE_DISCUSS);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<DiscussOfferModel> CreateDiscussModel(User owner)
        {
            string offerImageBase64 = default;
            string partnerPhotoOfferBase64 = default;

            if (UploadedImage is StreamImageSource partnerOfferImage)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //get stream from imagesource and copy to memory stram
                    var stream = await partnerOfferImage.Stream(CancellationToken.None);
                    stream.CopyTo(memoryStream);
                    partnerPhotoOfferBase64 = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            if (CurrentOfferCard.OfferImage is StreamImageSource ownerOfferImage)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //get stream from imagesource and copy to memory stram
                    var stream = await ownerOfferImage.Stream(CancellationToken.None);
                    stream.CopyTo(memoryStream);
                    offerImageBase64 = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            var userPhone = await SecureStorage.GetAsync(Constant.SecureConstant.PHONE_FIELD);
            var parnter = await _authService.GetUserByPhoneAsync(new PhoneRequestModel { PhoneNumber = userPhone });

            DiscussOfferModel discussOfferModel = new DiscussOfferModel
            {
                Conditions = Conditions,
                OwnerId = CurrentOfferCard.OwnerId ?? default,
                OwnerPhotoOffer = offerImageBase64,
                OwnerPhoneNumber = owner.Phone,
                PartnerId = parnter.Id,
                PartnerPhoneNumber = userPhone,
                PartnerPhotoOffer = partnerPhotoOfferBase64
            };
            return discussOfferModel;
        }

        private async Task SelectedItem(object category)
        {
            await ShowOfferAsync(category);
        }



        public async Task ShowOfferAsync(object offerCategory = null)
        {
            CurrentCategory = offerCategory as OfferCategory;
            if (!IsBusy)
            {
                IsBusy = true;

                try
                {
                    var area = await _googleMapsApiService.GetPlaceDetails(CurrentSearchLocation.PlaceId);

                    FilterRequestModel model = new FilterRequestModel
                    {
                        City = area.Addresses.FirstOrDefault(address => address.Types.Contains("locality")).LongName,
                        Country = area.Addresses.FirstOrDefault(address => address.Types.Contains("country")).LongName,
                        CategoryId = (int)(CurrentCategory?.Id ?? null),
                        SkippedCount = _showedCount + Offers.Count
                    };
                    IEnumerable<Offer> response = await _offerService.ShowOfferAsync(model);
                    if (response is null)
                    {
                        IsBusy = false;
                        return;
                    }
                    Offers = response.Select(item => new OfferCardModel
                    {
                        Description = item.Description,
                        OfferImage = item.PhotoSource.StartsWith("https") ?
                        ImageSource.FromUri(new Uri(item.PhotoSource)) :
                        ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.PhotoSource))),
                        OwnerId = item.UserId
                    }).ToArray();

                    _showedCount += Offers.Count();
                    IsContentEmpty = Offers.Count() == default;
                    await RaisePropertyChanged(nameof(Offers));
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }



        public async Task<bool> CheckIsLastOffer(OfferCardModel offerCard)
        {
            return await Task.Run(() =>
            {
                var lastItem = Offers.LastOrDefault();
                return
                offerCard.Description.Equals(lastItem?.Description) &&
                offerCard.OfferImage.Equals(lastItem?.OfferImage) &&
                offerCard.OwnerId.Equals(lastItem?.OwnerId);
            });


        }

        #endregion Functionality

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

        private string _conditions;
        public string Conditions
        {
            get => _conditions;
            set => SetProperty(ref _conditions, value);
        }

        private bool _isContentEmpty;
        public bool IsContentEmpty
        {
            get => _isContentEmpty;
            set => SetProperty(ref _isContentEmpty, value);
        }

        public IList<OfferCardModel> Offers { get; set; } = new List<OfferCardModel>();

        private OfferCategory _currentCategory;
        public OfferCategory CurrentCategory
        {
            get => _currentCategory;
            set => SetProperty(ref _currentCategory, value);
        }

        private string _imageBase64;
        public string ImageBase64
        {
            get => _imageBase64;
            set
            {
                SetProperty(ref _imageBase64, value);
                Image = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(_imageBase64)));
            }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private OfferCardModel _currentOfferCard;
        public OfferCardModel CurrentOfferCard
        {
            get => _currentOfferCard;
            set => SetProperty(ref _currentOfferCard, value);
        }

        private ImageSource _uploadedImage;
        public ImageSource UploadedImage
        {
            get => _uploadedImage;
            set => SetProperty(ref _uploadedImage, value);
        }
        #endregion Properties
    }
}

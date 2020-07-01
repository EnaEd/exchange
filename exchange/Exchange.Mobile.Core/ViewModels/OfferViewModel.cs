using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.ViewModels
{
    public class OfferViewModel : BaseViewModel
    {

        private readonly IOfferService _offerService;
        private readonly IAuthService<User> _authService;
        private readonly IDiscussOfferService _discussOfferService;
        private int _showedCount;

        public OfferViewModel(IOfferService offerService, IAuthService<User> authService, IDiscussOfferService discussOfferService)
        {
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



        #endregion Commands

        #region Functionality

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

            DiscussOfferModel result = await CreateDiscussModel();

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
                    (responseFailure) => { DisplayAlertService.ShowToast($"{Json.Serialize(responseFailure)}"); });
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

        private async Task<DiscussOfferModel> CreateDiscussModel()
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

            var parnter = await _authService.GetUserByPhoneAsync(new PhoneRequestModel { PhoneNumber = PhoneNumber });

            DiscussOfferModel discussOfferModel = new DiscussOfferModel
            {
                Conditions = Conditions,
                OwnerId = CurrentOfferCard.OwnerId ?? default,
                OwnerPhotoOffer = offerImageBase64,
                PartnerId = parnter.Id,
                PartnerPhoneNumber = PhoneNumber,
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
                    await GetLocationDataAsync();
                    FilterRequestModel model = new FilterRequestModel
                    {
                        City = City,
                        Country = Country,
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
                        OfferImage = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.PhotoSource))),
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

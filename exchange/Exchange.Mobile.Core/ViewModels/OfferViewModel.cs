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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.ViewModels
{
    public class OfferViewModel : BaseViewModel
    {

        private readonly IOfferService _offerService;
        private readonly IAuthService<User> _authService;

        public OfferViewModel(IOfferService offerService, IAuthService<User> authService)
        {
            _offerService = offerService;
            _authService = authService;

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

        #endregion Commands

        #region Functionality

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
                UploadedImage = ImageSource.FromFile(file.Path);
            }

        }


        private async Task SendOfferAsync()
        {
            //string number = _deviceInfoService.GetPhoneNumber();
            var user = await _authService.GetUserByIdAsync(CurrentOfferCard.OwnerId ?? default);

            var notification = new Dictionary<string, object>
            {
                ["contents"] = new Dictionary<string, string>() { { "en", "Test message" } },
                ["include_player_ids"] = new List<string>() { user.OneSignalId }
            };
            OneSignal.Current.PostNotification(notification,
                (responseSuccess) => { Debug.WriteLine("success"); },
                (responseFailure) => { Debug.WriteLine($"{Json.Serialize(responseFailure)}"); });
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
                        CategoryId = (int)(CurrentCategory?.Id ?? null)
                    };
                    var response = await _offerService.ShowOfferAsync(model);

                    if (response is null)
                    {
                        IsBusy = false;
                        return;
                    }

                    Offers.Add(new OfferCardModel
                    {
                        Description = response.Description,
                        OfferImage = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(response.PhotoSource))),
                        OwnerId = response.UserId
                    });
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
            var lastItem = Offers.LastOrDefault();
            return
                offerCard.Description.Equals(lastItem?.Description) &&
                offerCard.OfferImage.Equals(lastItem?.OfferImage) &&
                offerCard.OwnerId.Equals(lastItem?.OwnerId);

        }

        #endregion Functionality

        #region Properties

        public ObservableCollection<OfferCardModel> Offers { get; set; } = new ObservableCollection<OfferCardModel>();

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

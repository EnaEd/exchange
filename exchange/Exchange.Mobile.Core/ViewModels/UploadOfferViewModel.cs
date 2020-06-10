using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.ViewModels
{
    public class UploadOfferViewModel : BaseViewModel
    {

        private readonly IDisplayAlertService _displayAlertService;
        private readonly IOfferService _offerService;
        private readonly IAuthService<User> _authService;

        public UploadOfferViewModel(IDisplayAlertService displayAlertService, IOfferService offerService, IAuthService<User> authService)
        {
            _displayAlertService = displayAlertService;
            _offerService = offerService;
            _authService = authService;

            InvokeOnMainThreadAsync(async () => await GetOfferCategories(offerService));

        }



        #region Properties

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


        #endregion Commands

        #region Functionality

        private async Task UploadOfferAsync()
        {

            UploadOfferRequestModel requestModel = new UploadOfferRequestModel();
            requestModel.OfferPhoto = UploadedImageBase64;
            requestModel.OfferDescription = OfferDescription;
            requestModel.OfferOwner = new User { PhoneNumber = PhoneNumber, City = City, Country = Country };

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

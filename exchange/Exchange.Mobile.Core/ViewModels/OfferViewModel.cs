﻿using Exchange.Mobile.Core.Helpers.Interface;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.ViewModels
{
    public class OfferViewModel : BaseViewModel
    {
        private readonly IDisplayAlertService _displayAlertService;
        private readonly IOfferService _offerService;
        //TODO EE: get location from auth view Model
        private readonly ILocationHelper _locationHelper;
        public OfferViewModel(IDisplayAlertService displayAlertService, IOfferService offerService, ILocationHelper locationHelper)
        {
            _displayAlertService = displayAlertService;
            _offerService = offerService;
            _locationHelper = locationHelper;

            Device.InvokeOnMainThreadAsync(async () =>
            {
                await GetOfferCategories();

                //await ShowOfferAsync();
            });
        }



        #region Commands
        public IMvxCommand SwipeRightCommandAsync => new MvxCommand(SwipeRigth);
        public IMvxCommand SwipeLeftCommandAsync => new MvxCommand(SwipeLeft);
        public IMvxCommand SwipedCommand => new MvxCommand(SwipeLeft);
        public IMvxCommand SelectedCommandAsync => new MvxAsyncCommand<object>(SelectedItem);

        public IMvxCommand SendOfferCommandAsync => new MvxAsyncCommand(SendOfferAsync);





        #endregion Commands

        #region Functionality

        private async Task SendOfferAsync()
        {
            throw new NotImplementedException();
        }

        private async Task SelectedItem(object category)
        {
            await ShowOfferAsync(category);
        }

        private async Task GetOfferCategories()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    OfferCategories = new ObservableCollection<OfferCategory>(await _offerService.GetOfferCategoryAsync());
                    await RaisePropertyChanged(nameof(OfferCategories));
                }
                finally
                {
                    IsBusy = false;
                }
            }


        }

        private void SwipeRigth()
        {
            _displayAlertService.ShowToast("swipe right");
        }

        private void SwipeLeft()
        {
            _displayAlertService.ShowToast("swipe left");
        }

        public async Task ShowOfferAsync(object offerCategory = null)
        {
            if (!IsBusy)
            {
                IsBusy = true;

                try
                {
                    await GetLocationDataAsync();
                    FilterRequestModel model = new FilterRequestModel();
                    model.City = City;
                    model.Country = Country;
                    if (offerCategory is OfferCategory category)
                    {
                        model.CategoryId = (int)category.Id;
                    }
                    var response = await _offerService.ShowOfferAsync(model);

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

        private async Task GetLocationDataAsync()
        {
            var position = await _locationHelper.GetPositionAsync(TimeSpan.FromMilliseconds(10000));
            var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            if (placemark is null)
            {
                _displayAlertService.ShowToast("location fail");
            }
            City = placemark.Locality;
            Country = placemark.CountryName;

        }

        #endregion Functionality

        #region Properties
        public ObservableCollection<OfferCategory> OfferCategories { get; set; } = new ObservableCollection<OfferCategory>();
        public ObservableCollection<OfferCardModel> Offers { get; set; } = new ObservableCollection<OfferCardModel>();

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

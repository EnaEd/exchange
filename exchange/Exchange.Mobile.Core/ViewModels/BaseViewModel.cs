using Exchange.Mobile.Core.Enums;
using Exchange.Mobile.Core.Helpers.Interface;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Exchange.Mobile.Core.ViewModels

{
    public class BaseViewModel : MvxViewModel
    {
        public readonly IMvxNavigationService NavigationService;
        public readonly IDisplayAlertService DisplayAlertService;
        private readonly IDeviceInfoService DeviceInfoService;
        //TODO EE: get location from auth view Model
        public readonly ILocationHelper LocationHelper;
        public BaseViewModel()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            LocationHelper = Mvx.IoCProvider.Resolve<ILocationHelper>();
            DisplayAlertService = Mvx.IoCProvider.Resolve<IDisplayAlertService>();
            DeviceInfoService = Mvx.IoCProvider.Resolve<IDeviceInfoService>();

            //PhoneNumber = DeviceInfoService.GetPhoneNumber();
            PhoneNumber = "0123456789";

        }


        #region Properties

        public ObservableCollection<OfferCategory> OfferCategories { get; set; } = new ObservableCollection<OfferCategory>();
        public string PhoneNumber { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        public string City { get; set; }
        public string Country { get; set; }
        #endregion Properties

        #region Functionality

        public async Task GetOfferCategories(IOfferService offerService)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    OfferCategories = new ObservableCollection<OfferCategory>((await offerService.GetOfferCategoryAsync()).
                        Where(x => x.Category != CategoryEnum.Money.ToString()));
                    await RaisePropertyChanged(nameof(OfferCategories));
                }
                finally
                {
                    IsBusy = false;
                }
            }


        }

        public async Task GetLocationDataAsync()
        {
            var position = await LocationHelper.GetPositionAsync(TimeSpan.FromMilliseconds(10000));

            var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            if (placemark is null)
            {
                DisplayAlertService.ShowToast("location fail");
            }
            City = placemark.Locality;
            Country = placemark.CountryName;
        }
        #endregion Functionality
    }
}

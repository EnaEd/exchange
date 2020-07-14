using Exchange.Mobile.Core.Helpers.Interface;
using MvvmCross;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.Helpers
{
    public class LocationHelper : ILocationHelper
    {
        private INativeLocationService _nativeLocationService;
        public async Task<Position> GetPositionAsync(TimeSpan timeout)
        {
            var locator = CrossGeolocator.Current;
            if (!locator.IsGeolocationEnabled)
            {
                _nativeLocationService = Mvx.IoCProvider.Resolve<INativeLocationService>();
                await Device.InvokeOnMainThreadAsync(async () => { await _nativeLocationService.TurnOnGPS(); });

            }
            //TODO EE: improve this
            if (!locator.IsGeolocationEnabled)
            {
                return null;
            }
            locator.DesiredAccuracy = 120;
            return await locator.GetPositionAsync(TimeSpan.FromSeconds(60));
        }
    }
}

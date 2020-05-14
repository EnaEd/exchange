using Exchange.Mobile.Core.Helpers.Interface;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Helpers
{
    public class LocationHelper : ILocationHelper
    {
        public async Task<Position> GetPositionAsync(TimeSpan timeout)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            return await locator.GetPositionAsync(timeout);
        }
    }
}

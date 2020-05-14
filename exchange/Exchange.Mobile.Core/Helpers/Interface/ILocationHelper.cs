using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Helpers.Interface
{
    public interface ILocationHelper
    {
        Task<Position> GetPositionAsync(TimeSpan timeout);
    }
}

using Exchange.Mobile.Core.Models.GooglesModels;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IGoogleMapsApiService
    {
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<GooglePlace> GetPlaceDetails(string placeId);
    }
}

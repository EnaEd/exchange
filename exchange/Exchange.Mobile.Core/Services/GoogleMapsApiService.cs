using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models.GooglesModels;
using Exchange.Mobile.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class GoogleMapsApiService : BaseApiService, IGoogleMapsApiService
    {
        public async Task<GooglePlace> GetPlaceDetails(string placeId)
        {
            GooglePlace result = default;

            var url = string.Format(
               Constant.GoogleConstant.GOOGLE_PLACES_API_DETAILS,
               placeId,
               Constant.GoogleConstant.GOOGLE_API_KEY);

            var response = await ExecuteGetAsync<GooglePlace>(url);
            if (!response.IsSuccessStatusCode)
            {
                result.Errors.Add(response.ReasonPhrase);
                return result;
            }
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json) || json.Equals(Constant.Shared.ERROR))
            {
                result.Errors.Add(Constant.Shared.FAIL_JSON_PARSE);
                return result;
            }
            result = JsonConvert.DeserializeObject<GooglePlace>(json);
            return result;
        }

        public async Task<GooglePlaceAutoCompleteResult> GetPlaces(string text)
        {
            GooglePlaceAutoCompleteResult results = default;
            var url = string.Format(
                Constant.GoogleConstant.GOOGLE_PLACES_API_AUTO_COMPLETE_PATH,
                Constant.GoogleConstant.GOOGLE_API_KEY,
                text);
            var response = await ExecuteGetAsync<GooglePlaceAutoCompleteResult>(url);

            if (!response.IsSuccessStatusCode)
            {
                results.Errors.Add(response.ReasonPhrase);
                return results;
            }
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json) || json.Equals(Constant.Shared.ERROR))
            {
                results.Errors.Add(Constant.Shared.FAIL_JSON_PARSE);
                return results;
            }
            results = JsonConvert.DeserializeObject<GooglePlaceAutoCompleteResult>(json);
            return results;
        }
    }
}

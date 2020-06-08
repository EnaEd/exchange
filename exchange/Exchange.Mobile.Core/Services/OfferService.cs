using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class OfferService : BaseApiService, IOfferService
    {
        public async Task<IEnumerable<OfferCategory>> GetOfferCategoryAsync()
        {
            var response = await ExecuteGetAsync<IEnumerable<OfferCategory>>($"{ApplicationConfig.BaseUrl}api/Exchange/getoffercategories");

            return JsonConvert.DeserializeObject<IEnumerable<OfferCategory>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Offer> ShowOfferAsync(FilterRequestModel model = null)
        {
            var response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}api/exchange/showoffer", model);
            return JsonConvert.DeserializeObject<Offer>(await response.Content.ReadAsStringAsync());

        }

        public async Task<string> UploadOfferAsync(UploadOfferRequestModel model)
        {
            var response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}api/exchange/uploadoffer", model);
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}

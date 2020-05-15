using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class OfferService : BaseApiService, IOfferService
    {

        public async Task<Offer> ShowOfferAsync(FilterRequestModel model = null)
        {
            var response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}api/exchange/showoffer", model);

            return JsonConvert.DeserializeObject<Offer>(await response.Content.ReadAsStringAsync());

        }
    }
}

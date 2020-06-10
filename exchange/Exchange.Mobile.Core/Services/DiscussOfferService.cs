using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class DiscussOfferService : BaseApiService, IDiscussOfferService
    {
        public async Task<DiscussOfferModel> CreateDiscussOffer(DiscussOfferModel model)
        {
            HttpResponseMessage response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}{Constant.Route.CREATE_DISCUSS_ROUTE}", model);
            DiscussOfferModel result = JsonConvert.DeserializeObject<DiscussOfferModel>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<string> DeleteDiscussOffer(DiscussOfferModel model)
        {
            HttpResponseMessage response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}{Constant.Route.DELETE_DISCUSS_ROUTE}", model);
            string result = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<IEnumerable<DiscussOfferModel>> GetUserDiscuss(DiscussOfferRequestModel model)
        {
            HttpResponseMessage response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}{Constant.Route.GET_USER_DISCUSS_ROUTE}", model);
            IEnumerable<DiscussOfferModel> result = JsonConvert.DeserializeObject<IEnumerable<DiscussOfferModel>>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<DiscussOfferModel> UpdateDiscussOffer(DiscussOfferModel model)
        {
            HttpResponseMessage response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}{Constant.Route.UPDATE_DISCUSS_ROUTE}", model);
            DiscussOfferModel result = JsonConvert.DeserializeObject<DiscussOfferModel>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}

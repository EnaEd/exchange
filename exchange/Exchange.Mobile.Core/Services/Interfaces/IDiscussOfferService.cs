using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IDiscussOfferService
    {
        Task<IEnumerable<DiscussOfferModel>> GetUserDiscuss(DiscussOfferRequestModel model);
        Task<DiscussOfferModel> CreateDiscussOffer(DiscussOfferModel model);
        Task<DiscussOfferModel> UpdateDiscussOffer(DiscussOfferModel model);
        Task<string> DeleteDiscussOffer(DiscussOfferModel model);
    }
}

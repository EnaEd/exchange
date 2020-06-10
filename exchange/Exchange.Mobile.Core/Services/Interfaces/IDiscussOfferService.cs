using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IDiscussOfferService
    {
        Task<IEnumerable<DiscussOfferModel>> GetUserDiscussAsync(DiscussOfferRequestModel model);
        Task<DiscussOfferModel> CreateDiscussOfferAsync(DiscussOfferModel model);
        Task<DiscussOfferModel> UpdateDiscussOfferAsync(DiscussOfferModel model);
        Task<string> DeleteDiscussOfferAsync(DiscussOfferModel model);
    }
}

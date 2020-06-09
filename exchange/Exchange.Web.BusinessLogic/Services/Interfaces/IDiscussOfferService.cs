using Exchange.Web.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IDiscussOfferService
    {
        Task<IEnumerable<DiscussOfferModel>> GetUserDiscussAsync(DiscussOfferRequestModel model);
    }
}

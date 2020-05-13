using Exchange.Web.BusinessLogic.Models;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IExchangeService
    {
        public Task UploadOfferAsync(OfferRequestModel model);
        public Task<PhotoModel> ShowOfferAsync(FilterRequestModel model);
    }
}

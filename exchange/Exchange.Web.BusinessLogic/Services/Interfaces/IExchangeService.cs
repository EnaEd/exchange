using Exchange.Web.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IExchangeService
    {
        public Task UploadOfferAsync(OfferRequestModel model);
        public Task<IEnumerable<PhotoModel>> ShowOfferAsync(FilterRequestModel model);
        public Task<IEnumerable<CategoryExchangeModel>> GetOfferCategoriesAsync();
    }
}

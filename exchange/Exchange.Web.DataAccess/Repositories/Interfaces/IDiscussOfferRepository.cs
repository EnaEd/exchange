using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories.Interfaces
{
    public interface IDiscussOfferRepository<T> : IBaseRepository<T> where T : class
    {
        Task<IEnumerable<DiscussOfferEntity>> GetUserDiscussAsync(long userId);
    }
}

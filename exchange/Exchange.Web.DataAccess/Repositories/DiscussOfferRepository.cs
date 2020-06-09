using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Base;
using Exchange.Web.DataAccess.Repositories.Interfaces;

namespace Exchange.Web.DataAccess.Repositories
{
    public class DiscussOfferRepository : BaseEFRepository<DiscussOfferEntity>, IDiscussOfferRepository<DiscussOfferEntity>
    {
        public DiscussOfferRepository(AppContextDb appContext) : base(appContext)
        {
        }
    }
}

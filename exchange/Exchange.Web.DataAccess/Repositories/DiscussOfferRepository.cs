using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Base;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories
{
    public class DiscussOfferRepository : BaseEFRepository<DiscussOfferEntity>, IDiscussOfferRepository<DiscussOfferEntity>
    {
        public DiscussOfferRepository(AppContextDb appContext) : base(appContext)
        {
        }

        public async Task<IEnumerable<DiscussOfferEntity>> GetUserDiscussAsync(long userId)
        {
            var result = await DbSet.Where(item => item.OwnerId == userId || item.PartnerId == userId).ToListAsync();
            return result;
        }
    }
}

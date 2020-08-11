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
    public class ChatPartyRepository : BaseEFRepository<ChatPartyEntity>, IChatPartyRepository<ChatPartyEntity>
    {
        public ChatPartyRepository(AppContextDb appContext) : base(appContext)
        {
        }

        public async Task<IEnumerable<ChatPartyEntity>> GetAllByUserIdAsync(long userId)
        {
            List<ChatPartyEntity> chatParties = await DbSet.Where(item => item.UserId == userId).ToListAsync();
            return chatParties;
        }
    }
}

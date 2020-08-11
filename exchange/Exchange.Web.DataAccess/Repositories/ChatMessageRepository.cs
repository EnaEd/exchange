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
    public class ChatMessageRepository : BaseEFRepository<ChatMessageEntity>, IChatMessageRepository<ChatMessageEntity>
    {
        public ChatMessageRepository(AppContextDb appContext) : base(appContext)
        {
        }

        public async Task<IEnumerable<ChatMessageEntity>> GetAllById(long id)
        {
            List<ChatMessageEntity> result = await DbSet.Where(item => item.ChatId == id).ToListAsync();
            return result;
        }
    }
}

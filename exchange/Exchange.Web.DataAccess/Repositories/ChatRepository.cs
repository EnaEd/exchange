using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Base;
using Exchange.Web.DataAccess.Repositories.Interfaces;

namespace Exchange.Web.DataAccess.Repositories
{
    public class ChatRepository : BaseEFRepository<ChatEntity>, IChatRepository<ChatEntity>
    {
        public ChatRepository(AppContextDb appContext) : base(appContext)
        {
        }
    }
}

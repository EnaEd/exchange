using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Base;
using Exchange.Web.DataAccess.Repositories.Interfaces;

namespace Exchange.Web.DataAccess.Repositories
{
    public class ChatMessageRepository : BaseEFRepository<ChatMessageEntity>, IChatMessageRepository<ChatMessageEntity>
    {
        public ChatMessageRepository(AppContextDb appContext) : base(appContext)
        {
        }
    }
}

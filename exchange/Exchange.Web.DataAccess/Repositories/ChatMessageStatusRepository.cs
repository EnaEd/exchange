using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Base;
using Exchange.Web.DataAccess.Repositories.Interfaces;

namespace Exchange.Web.DataAccess.Repositories
{
    public class ChatMessageStatusRepository : BaseEFRepository<ChatMessageStatusEntity>, IChatMessageStatusRepository<ChatMessageStatusEntity>
    {
        public ChatMessageStatusRepository(AppContextDb appContext) : base(appContext)
        {
        }
    }
}

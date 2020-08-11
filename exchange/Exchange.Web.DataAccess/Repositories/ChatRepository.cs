using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Base;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories
{
    public class ChatRepository : BaseEFRepository<ChatEntity>, IChatRepository<ChatEntity>
    {
        public ChatRepository(AppContextDb appContext) : base(appContext)
        {
        }

        public Task<IEnumerable<ChatEntity>> GetAllById(long id)
        {

            throw new System.NotImplementedException();
        }
    }
}

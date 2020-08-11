using Exchange.Web.DataAccess.Repositories.Interfaces.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories.Interfaces
{
    public interface IChatPartyRepository<T> : IBaseRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllByUserIdAsync(long userId);

    }
}

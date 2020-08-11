using Exchange.Web.DataAccess.Repositories.Interfaces.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories.Interfaces
{
    public interface IChatMessageRepository<T> : IBaseRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllById(long Id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories.Interfaces.Base
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetOneByIdAsync(long id);
        public Task<T> CreateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<T> UpdateAsync(T entity);
    }
}

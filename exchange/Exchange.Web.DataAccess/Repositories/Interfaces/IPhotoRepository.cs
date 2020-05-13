using Exchange.Web.DataAccess.Models;
using Exchange.Web.DataAccess.Repositories.Interfaces.Base;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories.Interfaces
{
    public interface IPhotoRepository<T> : IBaseRepository<T> where T : class
    {
        public Task<T> GetOneByFilter(FilterModel filterModel);
    }
}

using Exchange.Web.DataAccess.Repositories.Interfaces.Base;

namespace Exchange.Web.DataAccess.Repositories.Interfaces
{
    public interface IChatMessageRepository<T> : IBaseRepository<T> where T : class
    {
    }
}

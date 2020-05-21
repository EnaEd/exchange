using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Base;
using Exchange.Web.DataAccess.Repositories.Interfaces;

namespace Exchange.Web.DataAccess.Repositories
{
    public class CategoryRepository : BaseEFRepository<CategotyExchangeEntity>, ICategoryRepository<CategotyExchangeEntity>
    {
        public CategoryRepository(AppContextDb appContext) : base(appContext)
        {
        }
    }
}

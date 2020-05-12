using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories.Base
{
    public class BaseEFRepository<T> : IBaseRepository<T> where T : class
    {
        public AppContextDb Context { get; private set; }
        public DbSet<T> DbSet { get; private set; }
        public BaseEFRepository(AppContextDb appContext)
        {
            Context = appContext;
            DbSet = Context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetOneByIdAsync(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
    }
}

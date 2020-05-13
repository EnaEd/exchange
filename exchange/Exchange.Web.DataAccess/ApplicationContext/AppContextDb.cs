using Exchange.Web.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Exchange.Web.DataAccess.ApplicationContext
{
    public class AppContextDb : IdentityDbContext<UserEntity, IdentityRole<long>, long>
    {
        public DbSet<CategotyExchangeEntity> ExchangeCategories { get; set; }
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }
    }

    public class ApplicationContextFactory : IDesignTimeDbContextFactory<AppContextDb>
    {
        public AppContextDb CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppContextDb>();
            builder.UseSqlServer("Server=DESKTOP-0R8765L\\SQLEXPRESS;Database=ExchangeOKDB;Trusted_Connection=True;");
            var context = new AppContextDb(builder.Options);
            return context;
        }
    }
}

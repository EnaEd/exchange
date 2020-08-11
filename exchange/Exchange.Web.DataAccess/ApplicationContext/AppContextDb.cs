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
        public DbSet<DiscussOfferEntity> DiscussOfferEntities { get; set; }
        public DbSet<ChatEntity> ChatEntities { get; set; }
        public DbSet<ChatMessageEntity> ChatMessageEntities { get; set; }
        public DbSet<ChatPartyEntity> ChatPartyEntities { get; set; }
        public DbSet<ChatMessageStatusEntity> ChatMessageStatusEntities { get; set; }

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
            builder.UseSqlServer("Server=ExchangeOKDB.mssql.somee.com;Database=ExchangeOKDB;user id=edena_SQLLogin_1;Password=rzapcr5511;MultipleActiveResultSets=true;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");
            var context = new AppContextDb(builder.Options);
            return context;
        }
    }
}

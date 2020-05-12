using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Exchange.Web.Shared.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Exchange.Web.DataAccess
{
    public class Startup
    {
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppContextDb>(options =>
                options.UseSqlServer(configuration.GetConnectionString($"{nameof(ConnectionStringConfig.DefaultConnection)}")));

            services.AddIdentityCore<UserEntity>()
               .AddRoles<IdentityRole<long>>()
               .AddSignInManager()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<AppContextDb>();

            services.AddAuthentication().AddIdentityCookies();


            services.Scan(scan => scan
           .FromAssemblyOf<IUserRepository<UserEntity>>()
           .AddClasses()
           .UsingRegistrationStrategy(RegistrationStrategy.Skip)
           .AsMatchingInterface()
           .AsImplementedInterfaces()//if not math interface like Interface<T>
           .WithTransientLifetime());

            Initializer.DbInitializer.InitDb(services.BuildServiceProvider().GetRequiredService<IUserRepository<UserEntity>>(), configuration);
        }
    }
}

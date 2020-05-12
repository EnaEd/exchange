using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.Shared.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exchange.Web.DataAccess
{
    public class Startup
    {
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppContextDb>(options =>
                options.UseSqlServer(configuration.GetConnectionString($"{nameof(ConnectionStringConfig.DefaultConnection)}")));

            services.AddIdentityCore<User>()
               .AddRoles<IdentityRole<long>>()
               .AddSignInManager()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<AppContextDb>();

            services.AddAuthentication().AddIdentityCookies();
        }
    }
}

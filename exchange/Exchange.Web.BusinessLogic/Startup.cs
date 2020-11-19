using AutoMapper;
using Exchange.Web.BusinessLogic.Helpers.Interfaces;
using Exchange.Web.BusinessLogic.MapperProfiles;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Exchange.Web.BusinessLogic
{
    public class Startup
    {
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            DataAccess.Startup.Init(services, configuration);

            services.AddSignalRCore();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new UserProfile());
                config.AddProfile(new PhotoProfile());
                config.AddProfile(new FilterProfile());
                config.AddProfile(new CategoryExchangeProfile());
                config.AddProfile(new DiscussOfferProfile());
                config.AddProfile(new ChatProfile());
                config.AddProfile(new ChatMessageProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Scan(scan => scan
          .FromAssemblyOf<IUserService>()
          .AddClasses()
          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
          .AsMatchingInterface()
          .AsImplementedInterfaces()//if not match interface like Interface<T>
          .WithTransientLifetime());



            //  services.Scan(scan => scan
            //.FromAssemblyOf<IJwtProvider>()
            //.AddClasses()
            //.UsingRegistrationStrategy(RegistrationStrategy.Skip)
            //.AsMatchingInterface()
            //.AsImplementedInterfaces()//if not match interface like Interface<T>
            //.WithTransientLifetime());
        }
    }
}

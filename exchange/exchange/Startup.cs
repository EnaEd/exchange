using Exchange.Web.BusinessLogic.Helpers.Interfaces;
using Exchange.Web.BusinessLogic.Services;
using Exchange.Web.Presentation.Extensions;
using Exchange.Web.Shared.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Exchange.Web.Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            BusinessLogic.Startup.Init(services, Configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//.AddIdentityCookies();
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = Configuration[$"{nameof(JwtConfig)}:{nameof(JwtConfig.Issuer)}"],
                       ValidateAudience = true,
                       ValidAudience = Configuration[$"{nameof(JwtConfig)}:{nameof(JwtConfig.Audience)}"],
                       ValidateLifetime = true,
                       IssuerSigningKey = services.BuildServiceProvider().GetRequiredService<IJWTProvider>().GetSymmetricSecurityKey(),
                   }

               }
               )

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "MyApi", Version = "v1" }));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandler();
            //custom handler with logger
            //app.UseErrorHandler();

            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "My Api V1"));

            app.UseRouting();
            app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200"));

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHubService>("/chat");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}

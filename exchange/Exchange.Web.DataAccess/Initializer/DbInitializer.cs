using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Exchange.Web.Shared.Configs;
using Exchange.Web.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Exchange.Web.DataAccess.Initializer
{
    public class DbInitializer
    {
        public static async void InitDb(IUserRepository<UserEntity> userRepository, IConfiguration configuration)
        {
            if (userRepository.GetOneByEmailAsync(configuration[$"{nameof(AdminData)}:{nameof(AdminData.AdminEmail)}"]) is null)
            {
                return;
            }

            UserEntity admin = new UserEntity
            {
                Email = configuration[$"{nameof(AdminData)}:{nameof(AdminData.AdminEmail)}"],
                UserName = configuration[$"{nameof(AdminData)}:{nameof(AdminData.AdminEmail)}"],
                EmailConfirmed = true
            };
            IdentityResult result = await userRepository.CreateAsync(admin, configuration[$"{nameof(AdminData)}:{nameof(AdminData.Password)}"]);
            if (result.Succeeded)
            {
                await userRepository.AddToRoleAsync(admin, Enum.UserRole.Admin.ToString());
            }
        }
    }
}

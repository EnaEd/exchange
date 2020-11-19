using Exchange.Web.BusinessLogic.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Helpers.Interfaces
{
    public interface IJwtProvider
    {
        public SymmetricSecurityKey GetSymmetricSecurityKey();
        public Task<string> GetTokenAsync(UserModel model);
        public Task<ClaimsIdentity> GetIdentityAsync(string userName);
    }
}

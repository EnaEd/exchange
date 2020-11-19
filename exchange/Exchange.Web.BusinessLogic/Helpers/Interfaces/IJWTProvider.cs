using Exchange.Web.BusinessLogic.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Helpers.Interfaces
{
    public interface IJWTProvider
    {
        public SymmetricSecurityKey GetSymmetricSecurityKey();
        public Task<string> GetTokenAsync(UserModel model);
        public Task<ClaimsIdentity> GetIdentityAsync(string userName);
    }
}

using Exchange.Web.BusinessLogic.Helpers.Interfaces;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.Shared.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Helpers
{
    public class JwtProvider : IJWTProvider
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;

        public JwtProvider(IConfiguration configuration,UserManager<UserEntity> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<ClaimsIdentity> GetIdentityAsync(string userName)
        {
            var user =await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                throw new Exception();//TODO add custom exception
            }
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration[$"{nameof(JwtConfig)}:{nameof(JwtConfig.Key)}"]));
        }

        public async Task<string> GetTokenAsync(UserModel model)
        {
            var identity =await  GetIdentityAsync(model.Email);

            var now = DateTime.UtcNow;
            
            var jwt = new JwtSecurityToken(
                    issuer: _configuration[$"{nameof(JwtConfig)}:{nameof(JwtConfig.Issuer)}"],
                    audience: _configuration[$"{nameof(JwtConfig)}:{nameof(JwtConfig.Audience)}"],
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(int.Parse(_configuration[$"{nameof(JwtConfig)}:{nameof(JwtConfig.LifeTime)}"]))),
                    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

           
            return encodedJwt;
        }
    }
}

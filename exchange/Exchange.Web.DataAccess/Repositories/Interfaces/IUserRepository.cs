using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository<T> : IBaseRepository<T> where T : class
    {
        public Task<T> GetOneByEmailAsync(string mail);
        public Task<IdentityResult> CreateAsync(T entity, string password);
        public Task AddToRoleAsync(UserEntity admin, string role);
        public Task<T> GetOneByPhoneNumberAsync(string phoneNumber, string countryCode);
    }
}

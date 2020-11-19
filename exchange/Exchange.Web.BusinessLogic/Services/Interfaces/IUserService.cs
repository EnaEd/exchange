using Exchange.Web.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserModel>> GetAllAsync();
        public Task<UserModel> CreateUserAsync(UserModel userModel);
        public Task<UserModel> IsUserExists(string phoneNumber, string CountryCode);
        public Task<UserModel> GetOneAsync(string phoneNumber, string CountryCode);
        public Task<UserModel> GetOneAsync(long id);
        public Task<UserModel> UpdateUserAsync(UserModel userModel);
    }
}

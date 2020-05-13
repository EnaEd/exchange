using Exchange.Web.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserModel>> GetAllAsync();
        public Task<UserModel> CreateUserAsync(UserModel userModel);
        public Task<bool> IsUserExists(string phoneNumber);
    }
}

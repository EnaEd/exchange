using Exchange.Web.BusinessLogic.Models;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<UserModel> RegistrationAsync(UserModel model);
        public Task<UserModel> IsUserExistAsync(string phoneNumber);
        public Task<UserModel> UpdateUserIfNeeded(UserModel model);
        public Task<string> SignInUser(PhoneRequestModel model);
    }
}

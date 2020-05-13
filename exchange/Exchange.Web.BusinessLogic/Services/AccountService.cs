using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;

        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> IsUserExist(string phoneNumber)
        {
            return await _userService.IsUserExists(phoneNumber);
        }

        public async Task<UserModel> RegistrationAsync(UserModel model)
        {
            //TODO EE:validation model
            return await _userService.CreateUserAsync(model);
        }
    }
}

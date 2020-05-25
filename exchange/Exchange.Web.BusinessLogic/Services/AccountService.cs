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

        public async Task<UserModel> UpdateUserIfNeeded(UserModel model)
        {
            var user = await _userService.GetOneAsync(model.Phone);
            if (string.IsNullOrWhiteSpace(user.OneSignalId) ||
                !user.OneSignalId.Equals(model.OneSignalId))
            {
                user.OneSignalId = model.OneSignalId;
            }
            var test = await _userService.UpdateUserAsync(user);
            return await _userService.UpdateUserAsync(user);
        }
    }
}

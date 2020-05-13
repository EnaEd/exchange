using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using System;
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

        public Task<UserModel> Registration(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}

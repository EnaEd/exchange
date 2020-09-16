using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exchange.Web.Presentation.Controllers
{
    [Route(Constant.Route.MAIN_API_ROUTE)]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost(Constant.Route.REGISTRATION_ROUTE)]
        public async Task<IActionResult> Registration([FromBody] UserModel model)
        {
            await _accountService.RegistrationAsync(model);
            return Ok("registration success");
        }

        [HttpPost(Constant.Route.CHECK_USER_EXISTS_ROUTE)]
        public async Task<IActionResult> CheckExistsUser([FromBody] PhoneRequestModel requestModel)
        {
            var result = await _accountService.IsUserExistAsync(requestModel.PhoneNumber);
            return Ok(result);
        }

        [HttpPost(Constant.Route.SIGN_IN_USER_ROUTE)]
        public async Task<IActionResult> SignInUser([FromBody] PhoneRequestModel requestModel)
        {
            var result = await _accountService.SignInUser(requestModel);
            return Ok(result);
        }

        [HttpPost(Constant.Route.UPDATE_USER_ROUTE)]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel model)
        {
            return Ok(await _accountService.UpdateUserIfNeeded(model));
        }
    }
}

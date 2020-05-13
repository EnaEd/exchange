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
            return Ok(await _accountService.IsUserExist(requestModel.PhoneNumber));
        }
    }
}

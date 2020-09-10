using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exchange.Web.Presentation.Controllers
{
    [Route(Constant.Route.MAIN_API_ROUTE)]
    public class DiscussOfferController : Controller
    {
        private readonly IDiscussOfferService _discussOfferService;

        public DiscussOfferController(IDiscussOfferService discussOfferService)
        {
            _discussOfferService = discussOfferService;
        }

        [HttpPost(Constant.Route.GET_DISCUSS_BY_USER_ROUTE)]
        public async Task<IActionResult> GetUserDiscuss([FromBody] DiscussOfferRequestModel model)
        {
            return Ok(await _discussOfferService.GetUserDiscussAsync(model));
        }

        [HttpPost(Constant.Route.CREATE_DISCUSS_ROUTE)]
        public async Task<IActionResult> CreateDiscuss([FromBody] DiscussOfferModel model)
        {
            var response = await _discussOfferService.CreateDiscussAsync(model);

            return Ok(response);
        }

        [HttpPost(Constant.Route.UPDATE_DISCUSS_ROUTE)]
        public async Task<IActionResult> UpdateDiscuss([FromBody] DiscussOfferModel model)
        {
            return Ok(await _discussOfferService.UpdateDiscussAsync(model));
        }

        [HttpPost(Constant.Route.DELETE_DISCUSS_ROUTE)]
        public async Task<IActionResult> DeleteDiscuss([FromBody] DiscussOfferModel model)
        {

            return Ok(await _discussOfferService.DeleteDiscussAsync(model) ? Constant.Shared.REQUEST_SUCCESS_RESULT : Constant.Shared.REQUEST_FAIL_RESULT);
        }
    }
}

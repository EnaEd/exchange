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

        [HttpGet(Constant.Route.GET_DISCUSS_BY_USER_ROUTE)]
        public async Task<IActionResult> GetUserDiscuss([FromBody] DiscussOfferRequestModel model)
        {
            return Ok(await _discussOfferService.GetUserDiscussAsync(model));
        }
    }
}

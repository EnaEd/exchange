using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exchange.Web.Presentation.Controllers
{
    [Route(Constant.Route.MAIN_API_ROUTE)]
    public class ExchangeController : Controller
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpPost(Constant.Route.SHOW_OFFER_ROUTE)]
        public async Task<IActionResult> ShowOffer([FromBody]FilterRequestModel model)
        {

            return Ok(await _exchangeService.ShowOfferAsync(model));
        }

        [HttpPost(Constant.Route.UPLOAD_OFFER_ROUTE)]
        public async Task<IActionResult> UploadOffer([FromBody] OfferRequestModel model)
        {
            await _exchangeService.UploadOfferAsync(model);
            return Ok("success");
        }
    }
}

using Exchange.Web.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.Web.Presentation.Controllers
{
    [Route(Constant.Route.MAIN_API_ROUTE)]
    public class DiscussOfferController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Exchange.Web.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("u at home");
        }
    }
}

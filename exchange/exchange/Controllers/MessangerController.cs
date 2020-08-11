using Exchange.Web.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exchange.Web.Presentation.Controllers
{
    [Route(Constant.Route.MAIN_API_ROUTE)]
    public class MessangerController : Controller
    {
        public Task<IActionResult> CreateConversation([FromBody] ConversationRequestModel)
        {

        }
    }
}

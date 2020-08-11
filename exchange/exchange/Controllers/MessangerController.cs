using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exchange.Web.Presentation.Controllers
{
    [Route(Constant.Route.MAIN_API_ROUTE)]
    public class MessangerController : Controller
    {
        private readonly IChatService _chatService;

        public MessangerController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost(Constant.Route.MESSANGER_CREATE_CHAT)]
        public async Task<IActionResult> CreateConversation([FromBody] ChatRequestModel model)
        {
            var response = await _chatService.CreateChatAsync(model);
            return Ok(response);
        }

        [HttpPost(Constant.Route.MESSANGER_CREATE_MESSAGE)]
        public async Task<IActionResult> CreateMessage([FromBody] ChatMessageModel model)
        {
            var response = await _chatService.CreateMessageAsync(model);
            return Ok(response);
        }

        [HttpPost(Constant.Route.MESSANGER_GET_CHATS)]
        public async Task<IActionResult> GetChats([FromBody] string userId)
        {
            var response = await _chatService.GetChatsByUserAsync(long.Parse(userId));
            return Ok(response);
        }

        [HttpPost(Constant.Route.MESSANGER_GET_MESSAGES)]
        public async Task<IActionResult> GetMessages([FromBody] string chatId)
        {
            var response = await _chatService.GetMesssageByChatAsync(long.Parse(chatId));
            return Ok(response);
        }
    }
}

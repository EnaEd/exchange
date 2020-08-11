using Exchange.Web.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IChatService
    {
        public Task<ChatModel> CreateChatAsync(ChatRequestModel model);
        public Task<IEnumerable<ChatModel>> GetChatsByUserAsync(long userId);
        public Task<ChatMessageModel> CreateMessageAsync(ChatMessageModel message);
        public Task<IEnumerable<ChatMessageModel>> GetMesssageByChatAsync(long chatId);
    }
}

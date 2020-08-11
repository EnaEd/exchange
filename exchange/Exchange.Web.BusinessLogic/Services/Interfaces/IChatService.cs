using Exchange.Web.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IChatService
    {
        public Task<ChatModel> CreateChat(string chatName, long createrId);
        public Task<IEnumerable<ChatModel>> GetChatsByUser(long userId);
        public Task<ChatMessageModel> CreateMessage(ChatMessageModel message);
        public Task<IEnumerable<ChatMessageModel>> GetMesssageByChat(long chatId);
    }
}

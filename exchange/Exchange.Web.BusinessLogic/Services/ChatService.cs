using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository<ChatEntity> _chatRepository;
        private readonly IChatMessageRepository<ChatMessageEntity> _chatMessageRepository;
        public ChatService(IChatRepository<ChatEntity> chatRepository,
            IChatMessageRepository<ChatMessageEntity> chatMessageRepository)
        {
            _chatRepository = chatRepository;
            _chatMessageRepository = chatMessageRepository;
        }


        public Task<ChatModel> CreateChat(string chatName, long createrId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ChatMessageModel> CreateMessage(ChatMessageModel message)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ChatModel>> GetChatsByUser(long userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ChatMessageModel>> GetMesssageByChat(long chatId)
        {
            throw new System.NotImplementedException();
        }
    }
}

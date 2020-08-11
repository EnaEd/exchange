using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Exchange.Web.Shared.Common;
using Exchange.Web.Shared.Configs;
using Exchange.Web.Shared.Constants;
using Exchange.Web.Shared.Enums;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository<ChatEntity> _chatRepository;
        private readonly IChatMessageRepository<ChatMessageEntity> _chatMessageRepository;
        private readonly IChatPartyRepository<ChatPartyEntity> _chatPartyRepository;

        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ChatService(IChatRepository<ChatEntity> chatRepository,
            IChatMessageRepository<ChatMessageEntity> chatMessageRepository, IMapper mapper, IConfiguration configuration, IChatPartyRepository<ChatPartyEntity> chatPartyRepository)
        {
            _chatRepository = chatRepository;
            _chatMessageRepository = chatMessageRepository;
            _mapper = mapper;
            _configuration = configuration;
            _chatPartyRepository = chatPartyRepository;
        }


        public async Task<ChatModel> CreateChatAsync(ChatRequestModel model)
        {
            var mappedModel = _mapper.Map<ChatEntity>(model);

            ChatEntity response = await _chatRepository.CreateAsync(mappedModel);
            if (response is null)
            {
                throw new UserException(new List<string> { Constant.ErrorInfo.FAIL_CREATE_CHAT }, Enum.ErrorCode.BadRequest);
            }

            IEnumerable<ChatPartyEntity> chatPartyEntities = model.PrticipantIds
                .Select(item => new ChatPartyEntity { ChatId = response.Id, UserId = item })
                .ToList();


            IEnumerable<ChatPartyEntity> chatPartyResponse = await _chatPartyRepository.CreateRangeAsync(chatPartyEntities);
            if (chatPartyResponse is null)
            {
                throw new UserException(new List<string> { Constant.ErrorInfo.FAIL_CREATE_CHATPARTY }, Enum.ErrorCode.BadRequest);
            }

            return _mapper.Map<ChatModel>(response);

        }

        public async Task<ChatMessageModel> CreateMessageAsync(ChatMessageModel message)
        {
            var mappedModel = _mapper.Map<ChatMessageEntity>(message);
            ChatMessageEntity response = await _chatMessageRepository.CreateAsync(mappedModel);
            if (response is null)
            {
                throw new UserException(new List<string> { Constant.ErrorInfo.FAIL_CREATE_MESSAGE }, Enum.ErrorCode.BadRequest);
            }
            return _mapper.Map<ChatMessageModel>(response);
        }

        public async Task<IEnumerable<ChatModel>> GetChatsByUserAsync(long userId)
        {
            var chatParties = await _chatPartyRepository.GetAllByUserIdAsync(userId);
            var chatIds = chatParties.Select(item => item.ChatId).ToList();
            var chats = await _chatRepository.GetAllAsync();
            var userInChat = chats.Where(chat => chatIds.Contains(chat.Id));

            return _mapper.Map<IEnumerable<ChatModel>>(userInChat);

        }

        public async Task<IEnumerable<ChatMessageModel>> GetMesssageByChatAsync(long chatId)
        {
            var messages = await _chatMessageRepository.GetAllById(chatId);
            var count = int.Parse(_configuration[$"{nameof(MessangerConfig)}:{nameof(MessangerConfig.VisibleMessageCount)}"]);
            return _mapper.Map<IEnumerable<ChatMessageModel>>(messages.OrderByDescending(message => message.CreateDate)
                .Take(count));
        }
    }
}

﻿using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.DataAccess.Entities;

namespace Exchange.Web.BusinessLogic.MapperProfiles
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatEntity, ChatModel>();
            CreateMap<ChatModel, ChatEntity>();

            CreateMap<ChatRequestModel, ChatEntity>();
            CreateMap<ChatEntity, ChatRequestModel>().ForMember(x => x.PrticipantIds, option => option.Ignore());
        }
    }
}

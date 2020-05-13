using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.DataAccess.Entities;

namespace Exchange.Web.BusinessLogic.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserEntity>().ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.Email));
            CreateMap<UserEntity, UserModel>();
        }
    }
}

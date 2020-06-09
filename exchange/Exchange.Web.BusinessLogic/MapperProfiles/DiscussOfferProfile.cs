using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.DataAccess.Entities;

namespace Exchange.Web.BusinessLogic.MapperProfiles
{
    public class DiscussOfferProfile : Profile
    {
        public DiscussOfferProfile()
        {
            CreateMap<DiscussOfferEntity, DiscussOfferModel>();
            CreateMap<DiscussOfferModel, DiscussOfferEntity>();
        }
    }
}

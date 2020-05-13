using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.DataAccess.Entities;

namespace Exchange.Web.BusinessLogic.MapperProfiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoModel, PhotoEntity>();
            CreateMap<PhotoEntity, PhotoModel>();
            CreateMap<PhotoEntity, OfferRequestModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(source => source.CategoryId))
                .ForMember(dest => dest.OfferPhoto, opt => opt.MapFrom(source => source.PhotoSource))
                .ForMember(dest => dest.OfferDescription, opt => opt.MapFrom(source => source.Description));
            CreateMap<OfferRequestModel, PhotoEntity>()
                //.ForMember(dest => (int)dest.CategoryId, opt => opt.MapFrom(source => source.Category))
                .ForMember(dest => dest.PhotoSource, opt => opt.MapFrom(source => source.OfferPhoto))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(source => source.OfferDescription));

        }
    }
}

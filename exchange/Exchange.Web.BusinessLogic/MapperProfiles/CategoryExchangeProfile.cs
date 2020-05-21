using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.DataAccess.Entities;

namespace Exchange.Web.BusinessLogic.MapperProfiles
{
    public class CategoryExchangeProfile : Profile
    {
        public CategoryExchangeProfile()
        {
            CreateMap<CategoryExchangeModel, CategotyExchangeEntity>();
            CreateMap<CategotyExchangeEntity, CategoryExchangeModel>();
        }
    }
}

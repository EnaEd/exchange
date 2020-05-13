using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.DataAccess.Models;

namespace Exchange.Web.BusinessLogic.MapperProfiles
{
    public class FilterProfile : Profile
    {
        public FilterProfile()
        {
            CreateMap<FilterModel, FilterRequestModel>();
            CreateMap<FilterRequestModel, FilterModel>();
        }
    }
}

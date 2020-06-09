using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class DiscussOfferService : IDiscussOfferService
    {
        private readonly IDiscussOfferRepository<DiscussOfferEntity> _discussOfferRepository;
        private readonly IMapper _mapper;

        public DiscussOfferService(IDiscussOfferRepository<DiscussOfferEntity> discussOfferRepository, IMapper mapper)
        {
            _discussOfferRepository = discussOfferRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DiscussOfferModel>> GetUserDiscussAsync(DiscussOfferRequestModel model)
        {
            IEnumerable<DiscussOfferEntity> result = await _discussOfferRepository.GetUserDiscussAsync(model.UserId);
            return _mapper.Map<IEnumerable<DiscussOfferModel>>(result);
        }
    }
}

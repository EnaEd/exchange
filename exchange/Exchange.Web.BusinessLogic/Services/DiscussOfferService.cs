using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Exchange.Web.Shared.Common;
using Exchange.Web.Shared.Constants;
using Exchange.Web.Shared.Enums;
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

        public async Task<DiscussOfferModel> CreateDiscussAsync(DiscussOfferModel model)
        {
            model.ExpireDate = System.DateTime.Now.AddDays(Constant.Shared.DEY_IN_WEEK_COUNT);
            model.Status = Enum.DiscussStatus.Created.ToString();
            var result = await _discussOfferRepository.CreateAsync(_mapper.Map<DiscussOfferEntity>(model));
            return _mapper.Map<DiscussOfferModel>(result);
        }

        public async Task<bool> DeleteDiscussAsync(DiscussOfferModel model)
        {
            await _discussOfferRepository.DeleteAsync(_mapper.Map<DiscussOfferEntity>(model));
            return true;
        }

        public async Task<IEnumerable<DiscussOfferModel>> GetUserDiscussAsync(DiscussOfferRequestModel model)
        {
            IEnumerable<DiscussOfferEntity> result = await _discussOfferRepository.GetUserDiscussAsync(model.UserId);
            return _mapper.Map<IEnumerable<DiscussOfferModel>>(result);
        }

        public async Task<DiscussOfferModel> UpdateDiscussAsync(DiscussOfferModel model)
        {
            var result = await _discussOfferRepository.GetOneByIdAsync(model.Id);
            if (result is null)
            {
                throw new UserException(new List<string> { Constant.ErrorInfo.DISCUSS_NOT_FOUND }, Enum.ErrorCode.BadRequest);
            }
            result.Conditions = model.Conditions ?? default;
            result.ExpireDate = System.DateTime.Now.AddDays(Constant.Shared.DEY_IN_WEEK_COUNT);
            result.OwnerId = model.OwnerId;
            result.OwnerPhoneNumber = model.OwnerPhoneNumber ?? default;
            result.OwnerPhotoOffer = model.OwnerPhotoOffer ?? default;
            result.PartnerId = model.PartnerId;
            result.PartnerPhoneNumber = model.PartnerPhoneNumber ?? default;
            result.PartnerPhotoOffer = model.PartnerPhotoOffer ?? default;
            result.Status = model.Status ?? default;

            var updatedEntity = await _discussOfferRepository.UpdateAsync(result);
            return _mapper.Map<DiscussOfferModel>(updatedEntity);
        }
    }
}

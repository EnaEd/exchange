using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Models;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Exchange.Web.Shared.Common;
using Exchange.Web.Shared.Constants;
using System.Threading.Tasks;
using Enum = Exchange.Web.Shared.Enums.Enum;

namespace Exchange.Web.BusinessLogic.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IPhotoRepository<PhotoEntity> _photoRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ExchangeService(IPhotoRepository<PhotoEntity> photoRepository, IUserService userService, IMapper mapper)
        {
            _photoRepository = photoRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<PhotoModel> ShowOfferAsync(FilterRequestModel model)
        {
            return _mapper.Map<PhotoModel>(await _photoRepository.GetOneByFilter(_mapper.Map<FilterModel>(model)));
        }

        public async Task UploadOfferAsync(OfferRequestModel model)
        {
            var res = await _userService.GetOneAsync(model.OfferOwner.Phone);
            if (res is null)
            {
                throw new UserException(Constant.ErrorInfo.USER_NOT_FOUND, Enum.ErrorCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(model.OfferPhoto) ||
                string.IsNullOrWhiteSpace(model.OfferDescription))
            {
                throw new UserException(Constant.ErrorInfo.NO_DATA_FOR_UPLOAD, Enum.ErrorCode.BadRequest);
            }
            model.UserId = res.Id;


            await _photoRepository.CreateAsync(_mapper.Map<PhotoEntity>(model));
        }
    }
}

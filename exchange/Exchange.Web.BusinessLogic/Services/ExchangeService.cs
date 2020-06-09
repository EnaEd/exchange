using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Models;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Exchange.Web.Shared.Common;
using Exchange.Web.Shared.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enum = Exchange.Web.Shared.Enums.Enum;

namespace Exchange.Web.BusinessLogic.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IPhotoRepository<PhotoEntity> _photoRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository<CategotyExchangeEntity> _categoryRepository;

        public ExchangeService(IPhotoRepository<PhotoEntity> photoRepository, IUserService userService, IMapper mapper,
            ICategoryRepository<CategotyExchangeEntity> categoryRepository)
        {
            _photoRepository = photoRepository;
            _userService = userService;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryExchangeModel>> GetOfferCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<CategoryExchangeModel>>(await _categoryRepository.GetAllAsync());
        }

        public async Task<IEnumerable<PhotoModel>> ShowOfferAsync(FilterRequestModel model)
        {

            return _mapper.Map<IEnumerable<PhotoModel>>(await _photoRepository.GetOneByFilter(_mapper.Map<FilterModel>(model)));
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

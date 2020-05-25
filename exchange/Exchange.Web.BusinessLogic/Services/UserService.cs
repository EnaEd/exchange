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
    public class UserService : IUserService
    {
        private readonly IUserRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository<UserEntity> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> CreateUserAsync(UserModel userModel)
        {
            var result = await _userRepository.CreateAsync(_mapper.Map<UserEntity>(userModel), userModel.Password);
            if (!result.Succeeded)
            {
                throw new UserException(Constant.ErrorInfo.REGISTRATION_FAIL, Enum.ErrorCode.BadRequest);
            }

            return userModel;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<UserModel>>(await _userRepository.GetAllAsync());
        }

        public async Task<UserModel> GetOneAsync(string phoneNUmber)
        {
            return _mapper.Map<UserModel>(await _userRepository.GetOneByPhoneNumberAsync(phoneNUmber));
        }

        public async Task<UserModel> GetOneAsync(long id)
        {
            return _mapper.Map<UserModel>(await _userRepository.GetOneByIdAsync(id));
        }

        public async Task<bool> IsUserExists(string phoneNumber)
        {
            return !(await _userRepository.GetOneByPhoneNumberAsync(phoneNumber) is null);
        }

        public async Task<UserModel> UpdateUserAsync(UserModel userModel)
        {
            return _mapper.Map<UserModel>(await _userRepository.UpdateAsync(_mapper.Map<UserEntity>(userModel)));
        }
    }
}

using AutoMapper;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Exchange.Web.Shared.Common;
using Exchange.Web.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
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
            IdentityResult result = await _userRepository.CreateAsync(_mapper.Map<UserEntity>(userModel), userModel.Password);
            if (!result.Succeeded)
            {
                throw new UserException(result.Errors.Select(error => error.Description).ToList(), Enum.ErrorCode.BadRequest);
            }

            return userModel;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<UserModel>>(await _userRepository.GetAllAsync());
        }

        public async Task<UserModel> GetOneAsync(string phoneNumber,string countryCode)
        {
            return _mapper.Map<UserModel>(await _userRepository.GetOneByPhoneNumberAsync(phoneNumber,countryCode));
        }

        public async Task<UserModel> GetOneAsync(long id)
        {
            return _mapper.Map<UserModel>(await _userRepository.GetOneByIdAsync(id));
        }

        public async Task<UserModel> IsUserExists(string phoneNumber,string countryCode)
        {
            var user = await _userRepository.GetOneByPhoneNumberAsync(phoneNumber,countryCode);
            if (user is not null)
            {
                var userModel = _mapper.Map<UserModel>(user);
                return userModel;
            }
            return null;
        }

        public async Task<UserModel> UpdateUserAsync(UserModel userModel)
        {
            return _mapper.Map<UserModel>(await _userRepository.UpdateAsync(_mapper.Map<UserEntity>(userModel)));
        }
    }
}

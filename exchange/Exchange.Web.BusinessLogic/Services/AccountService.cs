using AutoMapper;
using Exchange.Web.BusinessLogic.Helpers.Interfaces;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Models.Authy;
using Exchange.Web.BusinessLogic.Models.Authy.RequestModel;
using Exchange.Web.BusinessLogic.Models.Authy.ResponseModel;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.Shared.Common;
using Exchange.Web.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthyService _authyService;
        private readonly IUserService _userService;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;

        public AccountService(IUserService userService, IAuthyService authyService, SignInManager<UserEntity> signInManager,
            IMapper mapper, UserManager<UserEntity> userManager, IJwtProvider jwtProvider)
        {
            _userService = userService;
            _authyService = authyService;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<UserModel> IsUserExistAsync(string phoneNumber, string countryCode)
        {
            var result = await _userService.IsUserExists(phoneNumber, countryCode);
            if (result is null)
            {
                return new UserModel
                {
                    Errors = new List<string> { Constant.ErrorInfo.USER_NOT_FOUND },
                    Code = Shared.Enums.Enum.ErrorCode.NotFound.ToString()
                };
            }
            return result;
        }

        public async Task<UserModel> RegistrationAsync(UserModel model)
        {
            //TODO EE:validation model
            return await _userService.CreateUserAsync(model);
        }

        public async Task<AuthyBaseModel> SignInUser(PhoneRequestModel model)
        {
            var result = await IsUserExistAsync(model.PhoneNumber, model.CountryCode);
            if (result is null)
            {
                throw new UserException(new List<string> { Constant.ErrorInfo.USER_NOT_FOUND }, Shared.Enums.Enum.ErrorCode.NotFound);
            }

            CreateUserRequestModel authyRequestModel = new CreateUserRequestModel
            {
                CountryCode = model.CountryCode,
                Email = result.Email,
                PhoneNumber = model.PhoneNumber
            };

            AuthyOTPCodeResponse authyUser = await _authyService.CreateAuthyUserAsync(authyRequestModel);
            if (!authyUser.Success)
            {
                throw new UserException(new List<string> { authyUser.Message }, Shared.Enums.Enum.ErrorCode.BadRequest);
            }


            var baseModel = await _authyService.SendOTPCodeAsync(authyUser.User.Id);
            baseModel.AuthyId = authyUser.User.Id;
            if (!baseModel.Success)
            {
                throw new UserException(new List<string> { baseModel.Message }, Shared.Enums.Enum.ErrorCode.BadRequest);
            }

            return baseModel;

        }

        public async Task<UserModel> UpdateUserIfNeeded(UserModel model)
        {
            var user = await _userService.GetOneAsync(model.Phone, model.CountryCode);
            if (string.IsNullOrWhiteSpace(user.OneSignalId) ||
                !user.OneSignalId.Equals(model.OneSignalId))
            {
                user.OneSignalId = model.OneSignalId;
            }
            var test = await _userService.UpdateUserAsync(user);
            return await _userService.UpdateUserAsync(user);
        }

        public async Task<AuthyVerifyCodeResponseModel> VerifyOtpCodeAsync(VerifyCodeRequestModel model)
        {
            if (model is null)
            {
                throw new UserException(new List<string> { Constant.ErrorInfo.AUTHY_FAIL_SEND_OTP }, Shared.Enums.Enum.ErrorCode.BadRequest);
            }
            AuthyVerifyCodeResponseModel result = await _authyService.VerifyOTPCodeAsync(model.AuthyId, model.Token);

            if (!result.Success)
            {
                throw new UserException(new List<string> { Constant.ErrorInfo.AUTHY_FAIL_VERIFY_OTP_CODE }, Shared.Enums.Enum.ErrorCode.BadRequest);
            }


            result.User = await _userService.GetOneAsync(model.Phone, model.CountryCode);
            if (result.User is null)
            {
                throw new UserException(new List<string> { Constant.ErrorInfo.USER_NOT_FOUND }, Shared.Enums.Enum.ErrorCode.BadRequest);
            }
            var userEntity = await _userManager.FindByNameAsync(result.User.Email);
            await _signInManager.SignInAsync(userEntity, false);

            var token = await _jwtProvider.GetTokenAsync(result.User);
            if (string.IsNullOrEmpty(token))
            {
                throw new UserException(new List<string> { Constant.ErrorInfo.FAIL_TOKEN_CREATE }, Shared.Enums.Enum.ErrorCode.BadRequest);
            }
            result.AccessToken = token;

            return result;
        }
    }

}

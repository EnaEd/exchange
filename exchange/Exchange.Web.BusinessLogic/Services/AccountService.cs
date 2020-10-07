using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Models.Authy;
using Exchange.Web.BusinessLogic.Models.Authy.RequestModel;
using Exchange.Web.BusinessLogic.Models.Authy.ResponseModel;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.Shared.Common;
using Exchange.Web.Shared.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthyService _authyService;
        private readonly IUserService _userService;

        public AccountService(IUserService userService, IAuthyService authyService)
        {
            _userService = userService;
            _authyService = authyService;
        }

        public async Task<UserModel> IsUserExistAsync(string phoneNumber)
        {
            var result = await _userService.IsUserExists(phoneNumber);
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
            var result = await IsUserExistAsync(model.PhoneNumber);
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
            var user = await _userService.GetOneAsync(model.Phone);
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
            return result;
        }
    }

}

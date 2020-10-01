using Exchange.Web.BusinessLogic.Models.Authy;
using Exchange.Web.BusinessLogic.Models.Authy.RequestModel;
using Exchange.Web.BusinessLogic.Models.Authy.ResponseModel;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.Shared.Common;
using Exchange.Web.Shared.Configs;
using Exchange.Web.Shared.Constants;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class AuthyService : IAuthyService
    {
        private readonly IConfigurationSection _authySection;
        public AuthyService(IConfiguration configuration)
        {
            _authySection = configuration.GetSection(nameof(AuthyConfig));

        }
        public async Task<AuthyOTPCodeResponse> CreateAuthyUserAsync(CreateUserRequestModel model)
        {

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_authySection[nameof(AuthyConfig.AuthyDefaultGuardHeader)],
                    _authySection[nameof(AuthyConfig.AuthyApiKey)]);

                var requestContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>(Constant.Authy.EMAIL,model.Email),
                    new KeyValuePair<string, string>(Constant.Authy.CELLPHONE,model.PhoneNumber),
                    new KeyValuePair<string, string>(Constant.Authy.COUNTRY_CODE,model.CountryCode)
                });

                HttpResponseMessage response =
                    await client.PostAsync($"{_authySection[nameof(AuthyConfig.AuthyBaseUrl)]}{_authySection[nameof(AuthyConfig.AuthyAddUserUrl)]}", requestContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new UserException(new List<string> { Constant.ErrorInfo.AUTHY_FAIL_CREATE_USER }, Shared.Enums.Enum.ErrorCode.BadRequest);
                }
                string result = await response.Content.ReadAsStringAsync();
                AuthyOTPCodeResponse deserializeResult = JsonConvert.DeserializeObject<AuthyOTPCodeResponse>(result);
                return deserializeResult;
            }
        }

        public async Task<AuthyBaseModel> SendOTPCodeAsync(int authyId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_authySection[nameof(AuthyConfig.AuthyDefaultGuardHeader)],
                   _authySection[nameof(AuthyConfig.AuthyApiKey)]);

                HttpResponseMessage response =
                    await client.GetAsync($"{_authySection[nameof(AuthyConfig.AuthyBaseUrl)]}{_authySection[nameof(AuthyConfig.AuthySendOtpUrl)]}/{authyId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new UserException(new List<string> { Constant.ErrorInfo.AUTHY_FAIL_SEND_OTP }, Shared.Enums.Enum.ErrorCode.BadRequest);
                }
                var result = await response.Content.ReadAsStringAsync();
                var deserializeResult = JsonConvert.DeserializeObject<AuthyBaseModel>(result);
                return deserializeResult;
            }

        }

        public async Task<AuthyVerifyCodeResponseModel> VerifyOTPCodeAsync(int authyId, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_authySection[nameof(AuthyConfig.AuthyDefaultGuardHeader)],
                  _authySection[nameof(AuthyConfig.AuthyApiKey)]);

                HttpResponseMessage response =
                    await client.GetAsync($"{_authySection[nameof(AuthyConfig.AuthyBaseUrl)]}{_authySection[nameof(AuthyConfig.AuthyVerifyTokenUrl)]}/{token}/{authyId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new UserException(new List<string> { Constant.ErrorInfo.AUTHY_FAIL_VERIFY_OTP_CODE },
                        Shared.Enums.Enum.ErrorCode.BadRequest);
                }
                string result = await response.Content.ReadAsStringAsync();
                var deserializeResult = JsonConvert.DeserializeObject<AuthyVerifyCodeResponseModel>(result);
                return deserializeResult;
            }
        }
    }
}

﻿namespace Exchange.Web.BusinessLogic.Models.Authy.ResponseModel
{
    public class AuthyVerifyCodeResponseModel : AuthyBaseModel
    {
        public string Token { get; set; }
        public DeviceResponseModel Device { get; set; }
    }
}

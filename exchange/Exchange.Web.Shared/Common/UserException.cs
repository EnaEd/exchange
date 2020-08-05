using System;
using System.Collections.Generic;
using static Exchange.Web.Shared.Enums.Enum;

namespace Exchange.Web.Shared.Common
{
    public class UserException : Exception
    {
        public ErrorCode Code { get; set; }
        public List<string> Errors { get; set; }
        public UserException(List<string> errors, ErrorCode errorCode = ErrorCode.InternalServerError)
        {
            Code = errorCode;
            Errors = errors;
        }
    }
}

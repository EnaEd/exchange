using System;
using static Exchange.Web.Shared.Enums.Enum;

namespace Exchange.Web.Shared.Common
{
    public class UserException : Exception
    {
        public ErrorCode Code { get; set; }
        public string Description { get; set; }
        public UserException(string description, ErrorCode errorCode = ErrorCode.InternalServerError)
        {
            Code = errorCode;
            Description = description;
        }
    }
}

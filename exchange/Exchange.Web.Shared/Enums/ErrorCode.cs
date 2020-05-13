using System;

namespace Exchange.Web.Shared.Enums
{
    public partial class Enum
    {

        public enum ErrorCode
        {
            [EnumDescriptor("Unknown code")]
            None = 0,

            [EnumDescriptor("Bad Request")]
            BadRequest = 400,

            [EnumDescriptor("Unauthorized")]
            Unauthorized = 401,

            [EnumDescriptor("Forbidden")]
            Forbidden = 403,

            [EnumDescriptor("Not Found")]
            NotFound = 404,

            [EnumDescriptor("Method Not Allowed")]
            MethodNotAllowed = 405,

            [EnumDescriptor("Request Timeout")]
            RequestTimeout = 408,

            [EnumDescriptor("Internal Server Error")]
            InternalServerError = 500
        }


    }
    public class EnumDescriptor : Attribute
    {
        public string Description { get; set; }

        public EnumDescriptor(string description = null)
        {
            Description = description is null ? string.Empty : description;
        }
    }

}

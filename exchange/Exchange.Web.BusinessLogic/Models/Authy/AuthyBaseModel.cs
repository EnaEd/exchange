using Exchange.Web.BusinessLogic.Models.Base;

namespace Exchange.Web.BusinessLogic.Models.Authy
{
    public class AuthyBaseModel : BaseModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string CellPhone { get; set; }

    }
}

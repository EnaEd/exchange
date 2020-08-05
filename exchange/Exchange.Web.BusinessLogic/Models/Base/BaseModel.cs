using System.Collections.Generic;

namespace Exchange.Web.BusinessLogic.Models.Base
{
    public class BaseModel
    {
        public long Id { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string Code { get; set; }
    }
}

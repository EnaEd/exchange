﻿using Exchange.Web.BusinessLogic.Models.Base;

namespace Exchange.Web.BusinessLogic.Models
{
    public class ChatModel : BaseModel
    {
        public string ChatName { get; set; }
        public long CreaterId { get; set; }
    }
}

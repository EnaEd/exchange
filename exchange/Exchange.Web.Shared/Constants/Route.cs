namespace Exchange.Web.Shared.Constants
{
    public partial class Constant
    {

        public static class Route
        {
            public const string MAIN_API_ROUTE = "api/[controller]";

            //user controller routes
            public const string GET_USERS_ROUTE = "getusers";
            public const string GET_USER_ROUTE = "getuser";
            public const string GET_USER_BY_ID_ROUTE = "{id}";

            //account controller routes
            public const string REGISTRATION_ROUTE = "registration";
            public const string CHECK_USER_EXISTS_ROUTE = "checkuserexists";
            public const string UPDATE_USER_ROUTE = "updateuser";
            public const string SIGN_IN_USER_ROUTE = "signin";

            //exchange controller routes
            public const string SHOW_OFFER_ROUTE = "showoffer";
            public const string UPLOAD_OFFER_ROUTE = "uploadoffer";
            public const string GET_OFFER_CATEGORIES_ROUTE = "getoffercategories";

            //discussoffer controller routes
            public const string GET_DISCUSS_BY_USER_ROUTE = "getuserdiscuss";
            public const string CREATE_DISCUSS_ROUTE = "creatediscuss";
            public const string UPDATE_DISCUSS_ROUTE = "updatediscuss";
            public const string DELETE_DISCUSS_ROUTE = "deletediscuss";

            //messangerController
            public const string MESSANGER_CREATE_CHAT = "createchat";
            public const string MESSANGER_GET_CHATS = "getchats";
            public const string MESSANGER_CREATE_MESSAGE = "createmessage";
            public const string MESSANGER_GET_MESSAGES = "getmessages";
        }
    }
}

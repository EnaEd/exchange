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

            //account controller routes
            public const string REGISTRATION_ROUTE = "registration";
            public const string CHECK_USER_EXISTS_ROUTE = "checkuserexists";
            public const string UPDATE_USER_ROUTE = "updateuser";

            //exchange controller routes
            public const string SHOW_OFFER_ROUTE = "showoffer";
            public const string UPLOAD_OFFER_ROUTE = "uploadoffer";
            public const string GET_OFFER_CATEGORIES_ROUTE = "getoffercategories";
        }
    }
}

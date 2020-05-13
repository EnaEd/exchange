namespace Exchange.Web.Shared.Constants
{
    public partial class Constant
    {

        public static class Route
        {
            public const string MAIN_API_ROUTE = "api/[controller]";

            //user controller routes
            public const string GET_USERS_ROUTE = "getusers";

            //account controller routes
            public const string REGISTRATION_ROUTE = "registration";
            public const string CHECK_USER_EXISTS_ROUTE = "checkuserexists";

            //exchange controller routes
            public const string SHOW_OFFER_ROUTE = "showoffer";
            public const string UPLOAD_OFFER_ROUTE = "uploadoffer";
        }
    }
}

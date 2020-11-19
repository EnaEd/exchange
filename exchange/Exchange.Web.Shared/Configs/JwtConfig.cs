namespace Exchange.Web.Shared.Configs
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public int LifeTime { get; set; }
    }
}

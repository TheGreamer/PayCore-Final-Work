namespace PayCoreFinalWork.Core.WebAPI.Concrete.Jwt
{
    // appsettings.json - Jwt konfigürasyon sınıfı
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiration { get; set; }
    }
}
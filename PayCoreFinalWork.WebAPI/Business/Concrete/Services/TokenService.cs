using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Business.Concrete.StaticServices;
using PayCoreFinalWork.Core.Constants;
using PayCoreFinalWork.Core.WebAPI.Concrete.Jwt;
using PayCoreFinalWork.Core.WebAPI.Concrete.Requests;
using PayCoreFinalWork.Core.WebAPI.Concrete.Responses;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayCoreFinalWork.Business.Concrete.Services
{
    // JSON Web Token (JWT) servisi.
    public class TokenService : ITokenService
    {
        private readonly IAccountSession _accountSession; // Kullanıcı işlemleriyle ilgili olan Session nesnesi.
        private readonly JwtConfig _jwtConfig; // appsettings.json'da yer alan konfigürasyona ait özelliklerin bulunduğu config sınıfı.

        // Servis veya session sınıflarında anlık kullanıcıya ulaşmak için yer alan benzersiz numara.
        // Yalnızca bu sınıfta bir değer ataması yapılabilir olarak ayarlanmıştır. Giriş yapan kullanıcının id'si bu bilgiyi güncelleyecektir.
        public static Guid CurrentUserId { get; private set; }

        // Session ve JwtConfig bağımlılıkları eklenir.
        public TokenService(IAccountSession accountSession, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            _accountSession = accountSession;
            _jwtConfig = jwtConfig.CurrentValue;
        }

        // Token oluşturma işlemi burada gerçekleşir.
        public async Task<CoreResponse<TokenResponse>> GenerateToken(TokenRequest tokenRequest)
        {
            try
            {
                // Eğer bir istek yollanmamışsa ilgili hata döndürülür.
                if (tokenRequest == null)
                    return new CoreResponse<TokenResponse>(SystemMessage.TOKEN_NULL_ERROR);

                // Giriş yapan kullanıcı e-posta adresi üzerinden veritabanında aranır.
                var account = await _accountSession.Get(a => a.Email.Equals(tokenRequest.Email));

                // Eğer böyle bir hesap kayıtlarda mevcut değilse ilgili hata döndürülür.
                if (account == null)
                    return new CoreResponse<TokenResponse>(SystemMessage.TOKEN_INFO_ERROR);

                // Girilen parola bilgisi veritabanında yer alan şifrelenmiş parola bilgisinden çözümlenme durumu tutulur.
                var verified = EncryptionService.VerifyHashedPassword(account.Password, tokenRequest.Password);

                // Eğer böyle bir hesap kayıtlarda mevcut değilse ilgili hata döndürülür.
                if (!verified)
                    return new CoreResponse<TokenResponse>(SystemMessage.TOKEN_INFO_ERROR);

                var now = DateTime.UtcNow;
                var token = GetToken(account, now); // Giriş yapmak isteyen kullanıcı için token oluşturulur.

                try
                {
                    account.LastActivity = now;
                    _accountSession.BeginTransaction();
                    await _accountSession.Update(account); // Giriş yapan kullanıcının son aktivite durumu giriş yaptığı zamana güncellenir.
                    await _accountSession.CommitTransaction();
                }
                catch (Exception exception)
                {
                    Log.Error($"{SystemMessage.TOKEN_LAST_ACTIVITY_ERROR} : {account.UserName}", exception.Message);
                    await _accountSession.RollbackTransaction();
                }
                finally
                {
                    _accountSession.CloseTransaction();
                }

                // İlgili bilgiler neticesinde kullanıcının alacağı token'a ait bilgilerin döndürülebilmesi için bir response oluşturulur.
                var tokenResponse = new TokenResponse
                {
                    AccessToken = token,
                    ExpireTime = now.AddMinutes(_jwtConfig.AccessTokenExpiration),
                    Role = account.Role,
                    UserName = account.UserName,
                    SessionTimeInSecond = _jwtConfig.AccessTokenExpiration * 60,
                    UserId = account.Id
                };

                // Giriş yapan kullanıcının id'si sistem tarafında tutulur.
                CurrentUserId = tokenResponse.UserId;

                // Token bilgileri döndürülür.
                return new CoreResponse<TokenResponse>(tokenResponse);
            }
            catch (Exception exception)
            {
                Log.Error($"{SystemMessage.TOKEN_GENERATION_ERROR} : ", exception.Message);
                return new CoreResponse<TokenResponse>(SystemMessage.TOKEN_GENERATION_ERROR);
            }
        }

        // İlgili nesneler aracılığıyla giriş yapacak kullanıcı için token oluşturulur.
        private string GetToken(Account account, DateTime date)
        {
            var claims = GetClaims(account);
            var secret = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken
            (
                _jwtConfig.Issuer,
               shouldAddAudienceClaim ? _jwtConfig.Audience : string.Empty,
               claims,
               expires: date.AddMinutes(_jwtConfig.AccessTokenExpiration),
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken; // Oluşturulan token döndürülür.
        }

        private Claim[] GetClaims(Account account)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role, account.Role),
                new Claim(ClaimTypes.Email, account.Email)
            };

            return claims;
        }
    }
}
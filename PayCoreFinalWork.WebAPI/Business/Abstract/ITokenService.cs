using PayCoreFinalWork.Core.WebAPI.Concrete.Requests;
using PayCoreFinalWork.Core.WebAPI.Concrete.Responses;

namespace PayCoreFinalWork.Business.Abstract
{
    // Jwt Token servis interface'i.
    public interface ITokenService
    {
        Task<CoreResponse<TokenResponse>> GenerateToken(TokenRequest tokenRequest); // Token oluşturma.
    }
}
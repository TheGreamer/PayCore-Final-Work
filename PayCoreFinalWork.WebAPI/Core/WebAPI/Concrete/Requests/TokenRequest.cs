namespace PayCoreFinalWork.Core.WebAPI.Concrete.Requests
{
    // Kullanıcı girişinde kullanılacak istek tipi.
    public class TokenRequest
    {
        public string Email { get; set; } // E-posta alanı. (Metinsel)
        public string Password { get; set; } // Şifre alanı. (Metinsel)
    }
}
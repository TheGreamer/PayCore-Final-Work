using System.ComponentModel.DataAnnotations;

namespace PayCoreFinalWork.Core.WebAPI.Concrete.Responses
{
    // Giriş yapan kullanıcıya gösterilecek dönüş tipi.
    // Elde edilen token bilgisi kullanıcıya döndürülür.
    public class TokenResponse
    {
        [Display(Name = "Expire Time")]
        public DateTime ExpireTime { get; set; } // Bitiş süresi.

        [Display(Name = "Access Token")]
        public string AccessToken { get; set; } // Token bilgisi.

        [Display(Name = "User Role")]
        public string Role { get; set; } // Kullanıcı rolü.

        [Display(Name = "User Name")]
        public string UserName { get; set; } // Kullanıcı adı.

        [Display(Name = "Session Time In Seconds")]
        public int SessionTimeInSecond { get; set; } // Token süresi.

        [Display(Name = "User Id")]
        public Guid UserId { get; set; } // Kullanıcı numarası.
    }
}
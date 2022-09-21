using PayCoreFinalWork.Core.Dto.Abstract;

namespace PayCoreFinalWork.Dto.Concrete
{
    // Account sınıfına ait veri transfer sınıfı.
    // Sistemin katmanlı ve generic bir yapıda kodlanabilmesi adına ortak bir dto interface'i olan ICoreDto implemente edildi.
    public class AccountDto : ICoreDto
    {
        public string FirstName { get; set; } // Ad alanı (Metinsel)
        public string LastName { get; set; } // Soyad alanı (Metinsel)
        public string UserName { get; set; } // Kullanıcı adı alanı (Metinsel)
        public string Email { get; set; } // E-posta alanı (Metinsel)
        public string Password { get; set; } // Şifre alanı (Metinsel)
        public string PhoneNumber { get; set; } // Telefon numarası alanı (Metinsel)
        public int Age { get; set; } // Yaş alanı (Tam sayı)
        public string Role { get; set; }  // Rol alanı (Metinsel)
    }
}
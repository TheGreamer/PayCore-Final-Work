using PayCoreFinalWork.Core.Entity.Concrete;

namespace PayCoreFinalWork.Entity.Concrete.Entities
{
    // Veritabanında "accounts" tablosuna karşılık gelecek hesap sınıfı.
    // Sistemin katmanlı ve generic bir yapıda kodlanabilmesi adına ortak bir entity sınıfı olan CoreEntity'den kalıtım alındı.
    public class Account : CoreEntity
    {
        public virtual string FirstName { get; set; } // Ad alanı (Metinsel)
        public virtual string LastName { get; set; } // Soyad alanı (Metinsel)
        public virtual string UserName { get; set; } // Kullanıcı adı alanı (Metinsel)
        public virtual string Email { get; set; } // E-posta alanı (Metinsel)
        public virtual string Password { get; set; } // Şifre alanı (Metinsel)
        public virtual string PhoneNumber { get; set; } // Telefon numarası alanı (Metinsel)
        public virtual int Age { get; set; } // Yaş alanı (Tam sayı)
        public virtual DateTime LastActivity { get; set; } // Son aktivite alanı (Tarihsel)
        public virtual string Role { get; set; } // Rol alanı (Metinsel)
    }
}
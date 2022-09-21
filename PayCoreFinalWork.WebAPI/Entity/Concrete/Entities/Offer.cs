using PayCoreFinalWork.Core.Entity.Concrete;

namespace PayCoreFinalWork.Entity.Concrete.Entities
{
    // Veritabanında "offers" tablosuna karşılık gelecek teklif sınıfı.
    // Sistemin katmanlı ve generic bir yapıda kodlanabilmesi adına ortak bir entity sınıfı olan CoreEntity'den kalıtım alındı.
    public class Offer : CoreEntity
    {
        public virtual Guid AccountId { get; set; } // Hesap numarası alanı (Benzersiz)
        public virtual Guid ProductId { get; set; } // Ürün numarası alanı (Benzersiz)
        public virtual Guid OfferedUserId { get; set; } // Teklif yapılan kullanıcının numara alanı (Benzersiz)
        public virtual double Price { get; set; } // Fiyat alanı (Ondalıklı sayı)
        public virtual string Comment { get; set; } // Yorum alanı (Metinsel)
        public virtual bool IsAccepted { get; set; } // Kabul edilme durumu alanı (Mantıksal)
    }
}
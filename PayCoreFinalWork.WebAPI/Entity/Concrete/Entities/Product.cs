using PayCoreFinalWork.Core.Entity.Concrete;

namespace PayCoreFinalWork.Entity.Concrete.Entities
{
    // Veritabanında "products" tablosuna karşılık gelecek ürün sınıfı.
    // Sistemin katmanlı ve generic bir yapıda kodlanabilmesi adına ortak bir entity sınıfı olan CoreEntity'den kalıtım alındı.
    public class Product : CoreEntity
    {
        public virtual string Name { get; set; } // Ad alanı (Metinsel)
        public virtual string Description { get; set; } // Açıklama alanı (Metinsel)
        public virtual Guid CategoryId { get; set; } // Kategori numarası alanı (Benzersiz)
        public virtual Guid AccountId { get; set; } // Hesap numarası alanı (Benzersiz)
        public virtual string Color { get; set; } // Renk alanı (Metinsel)
        public virtual string Brand { get; set; } // Marka alanı (Metinsel)
        public virtual double Price { get; set; } // Fiyat alanı (Ondalıklı sayı)
        public virtual bool IsSold { get; set; } // Satılma durumu alanı (Mantıksal)
        public virtual bool IsOfferable { get; set; } // Teklif verilebilir olma alanı (Mantıksal)
    }
}
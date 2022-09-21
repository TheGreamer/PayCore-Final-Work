using PayCoreFinalWork.Core.Entity.Concrete;

namespace PayCoreFinalWork.Entity.Concrete.Entities
{
    // Veritabanında "categories" tablosuna karşılık gelecek kategori sınıfı.
    // Sistemin katmanlı ve generic bir yapıda kodlanabilmesi adına ortak bir entity sınıfı olan CoreEntity'den kalıtım alındı.
    public class Category : CoreEntity
    {
        public virtual string Name { get; set; } // Ad alanı (Metinsel)
        public virtual string Description { get; set; } // Açıklama alanı (Metinsel)
    }
}
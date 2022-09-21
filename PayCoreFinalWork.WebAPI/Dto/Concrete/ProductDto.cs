using PayCoreFinalWork.Core.Dto.Abstract;

namespace PayCoreFinalWork.Dto.Concrete
{
    // Product sınıfına ait veri transfer sınıfı.
    // Sistemin katmanlı ve generic bir yapıda kodlanabilmesi adına ortak bir dto interface'i olan ICoreDto implemente edildi.
    public class ProductDto : ICoreDto
    {
        public string Name { get; set; } // Ad alanı (Metinsel)
        public string Description { get; set; } // Açıklama alanı (Metinsel)
        public Guid CategoryId { get; set; } // Kategori numarası alanı (Benzersiz)
        public string Color { get; set; } // Renk alanı (Metinsel)
        public string Brand { get; set; } // Marka alanı (Metinsel)
        public double Price { get; set; } // Fiyat alanı (Ondalıklı sayı)
        public bool IsSold { get; set; } // Satılma durumu alanı (Mantıksal)
        public bool IsOfferable { get; set; } // Teklif verilebilir olma alanı (Mantıksal)
    }
}
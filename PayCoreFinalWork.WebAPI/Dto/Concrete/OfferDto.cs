using PayCoreFinalWork.Core.Dto.Abstract;

namespace PayCoreFinalWork.Dto.Concrete
{
    // Offer sınıfına ait veri transfer sınıfı.
    // Sistemin katmanlı ve generic bir yapıda kodlanabilmesi adına ortak bir dto interface'i olan ICoreDto implemente edildi.
    public class OfferDto : ICoreDto
    {
        public Guid ProductId { get; set; } // Ürün numarası alanı (Benzersiz)
        public double Price { get; set; } // Fiyat alanı (Ondalıklı sayı)
        public string Comment { get; set; } // Yorum alanı (Metinsel)
    }
}
using PayCoreFinalWork.Core.Dto.Abstract;

namespace PayCoreFinalWork.Dto.Concrete
{
    // Category sınıfına ait veri transfer sınıfı.
    // Sistemin katmanlı ve generic bir yapıda kodlanabilmesi adına ortak bir dto interface'i olan ICoreDto implemente edildi.
    public class CategoryDto : ICoreDto
    {
        public string Name { get; set; } // Ad alanı (Metinsel)
        public string Description { get; set; } // Açıklama alanı (Metinsel)
    }
}
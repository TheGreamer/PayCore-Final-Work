namespace PayCoreFinalWork.Core.WebAPI.Concrete.Requests
{
    // Ürün satın alma sırasında kullanılacak istek tipi.
    public class BuyProductRequest
    {
        public Guid ProductId { get; set; } // Ürün numarası. (Benzersiz)
    }
}
using PayCoreFinalWork.Core.Business.Abstract;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Business.Abstract
{
    // Product sınıfına ait servis interface'i.
    // ICoreService'den gerekli ortak özelliklerin kalıtımı alınır.
    // İhtiyaç halinde yalnızca bu servis'a özel olacak metodlar burada belirlenir.
    public interface IProductService : ICoreService<Product, IProductSession>
    {
        Task<ApiResponse> BuyProduct(Product product); // Ürün satın alma işlemi.
    }
}
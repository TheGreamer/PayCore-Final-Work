using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Core.Business.Concrete;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Business.Concrete.Services
{
    // Product sınıfına ait servis sınıfı.
    // CoreService'den kalıtım alınır. Generic operatörü değer olarak Entity ve Session nesnesi bekler.
    // Ayriyeten eğer sadece bu sınıfta yer alacak Product servisleri yer alacaksa IProductService implemente edilir.
    public class ProductService : CoreService<Product, IProductSession>, IProductService
    {
        private readonly IProductSession _productSession;

        public ProductService(IProductSession productSession) : base(productSession)
        {
            _productSession = productSession;
        }

        // Ürün satın alma işlemi.
        // Satın alınacak ürünün satılmış olma durumu "IsSold" true olarak ve teklif verilebilir olma durumu "IsOfferable" false olarak belirlenir.
        // Eğer satın alınacak ürün bulunamadıysa işlem iptal edilir. Yanlış bir ürün numarası gönderim durumunda işlem sona erdirilir.
        // Eğer satın alınacak ürün zaten satılmış bir ürünse işlem iptal edilir ve satın alma sonlandırılır.
        // Bir aksaklık olma durumunda işlem iptal edilir.
        public async Task<ApiResponse> BuyProduct(Product product)
        {
            if (product == null)
                return ApiResponse.ProductBuyFailure;

            if (product.IsSold)
                return ApiResponse.ProductIsSold;

            bool state = true;

            try
            {
                _productSession.BeginTransaction();
                product.IsSold = true;
                product.IsOfferable = false;
                await _productSession.Update(product);
                await _productSession.CommitTransaction();
            }
            catch
            {
                state = false;
                await _productSession.RollbackTransaction();
            }
            finally
            {
                _productSession.CloseTransaction();
            }

            return state ? ApiResponse.ProductBuySuccess : ApiResponse.ProductBuyFailure;
        }
    }
}
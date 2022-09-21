using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Core.Business.Concrete;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Business.Concrete.Services
{
    // Category sınıfına ait servis sınıfı.
    // CoreService'den kalıtım alınır. Generic operatörü değer olarak Entity ve Session nesnesi bekler.
    // Ayriyeten eğer sadece bu sınıfta yer alacak Category servisleri yer alacaksa ICategoryService implemente edilir.
    public class CategoryService : CoreService<Category, ICategorySession>, ICategoryService
    {
        public CategoryService(ICategorySession categorySession) : base(categorySession)
        {
        }
    }
}
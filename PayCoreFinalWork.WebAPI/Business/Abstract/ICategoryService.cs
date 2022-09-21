using PayCoreFinalWork.Core.Business.Abstract;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Business.Abstract
{
    // Category sınıfına ait servis interface'i.
    // ICoreService'den gerekli ortak özelliklerin kalıtımı alınır.
    // İhtiyaç halinde yalnızca bu servis'a özel olacak metodlar burada belirlenir.
    public interface ICategoryService : ICoreService<Category, ICategorySession>
    {
    }
}
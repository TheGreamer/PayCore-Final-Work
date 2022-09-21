using PayCoreFinalWork.Core.Entity.Concrete;
using System.Linq.Expressions;

namespace PayCoreFinalWork.Core.DataAccess.Abstract
{
    // Session nesnelerinden bağımsız olarak tipe özel şekilde çalışabilecek yardımcı (helper) metodlar.
    // Tüm sessionlar kendi kullandığı nesneden bağımsız şekilde bu yardımcı metodlara ihtiyaç neticesinde erişebilirler.
    public interface IHelperSession
    {
        // Filtreye göre tüm kayıtları belirtilen nesneye göre listeleme.
        Task<List<Entity>> GetEntityListHelper<Entity>(Expression<Func<Entity, bool>> expression) where Entity : CoreEntity;

        // Filtreye göre tek bir kaydı belirtilen nesneye göre arama.
        Task<Entity> GetEntityHelper<Entity>(Expression<Func<Entity, bool>> expression) where Entity : CoreEntity;

        // Tek bir kaydı belirtilen nesneye göre silme
        Task DeleteHelper<Entity>(Entity entity) where Entity : CoreEntity;
    }
}
using PayCoreFinalWork.Core.Entity.Concrete;
using PayCoreFinalWork.Core.Enums;
using System.Linq.Expressions;

namespace PayCoreFinalWork.Core.DataAccess.Abstract
{
    // ISession nesnesi yerine kullanılacak olan base interface.
    // Buradan kalıtım alacak olan diğer interface'lerin sahip olacağı ortak özellikleri barındırır.
    // CoreSession sınıfı bu ortak özelliklerin ortak işlevlerini ve çalışma şekillerini bu interface'i implemente ederek belirler.
    // Generic operatörünün içerisinde belirlenecek tip ise yalnızca CoreEntity sınıfından kalıtım almış bir sınıf olmak zorundadır.
    // Uygun tüm metodlar asenkron olarak yer almıştır.
    public interface ICoreSession<TEntity> where TEntity : CoreEntity
    {
        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>>? expression = null); // Belli bir filtreye göre veya tüm kayıtları listeleme.
        Task<TEntity> Get(Expression<Func<TEntity, bool>> expression); // Belli bir filtreye göre tek kayıt arama.
        Task Add(TEntity entity); // Yeni bir kayıt ekleme.
        Task Update(TEntity entity); // Mevcut bir kaydı güncelleme.
        Task Delete(TEntity entity); // Mevcut bir kaydı silme.
        void BeginTransaction(); // Takipe alınacak bir işlem başlatma.
        Task CommitTransaction(); // Takipe alınan işlemi işleme.
        Task RollbackTransaction(); // Takipe alınan işlemi geriye sarma.
        void CloseTransaction(); // Takip işlemini sona erdirme.
        Task<ApiResponse> DbOperation(Operation operation, TEntity entity, TEntity? entityFromBody = null); // Bir işlemi takip ettirerek yaptırma.
    }
}
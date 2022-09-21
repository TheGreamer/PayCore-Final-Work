using NHibernate;
using NHibernate.Linq;
using PayCoreFinalWork.Core.DataAccess.Abstract;
using PayCoreFinalWork.Core.Entity.Concrete;
using PayCoreFinalWork.Core.Enums;
using System.Linq.Expressions;
using ISession = NHibernate.ISession;

namespace PayCoreFinalWork.Core.DataAccess.Concrete
{
    // Veritabanında yer alan tablolara ait sınıfların session sınıfları bu sınıftan kalıtım alarak ortak veritabanı işlemlerinin tekrar yazılmasına gerek kalmaz.
    // Bütün ortak veritabanı işlemleri ICoreSession interface'inden implemente edilerek yapacakları görevler belirlenir.
    // CoreSession'un generic operatörünün içerisinde generic bir tip belirlenir. Ancak bu tip yalnızca CoreEntity sınıfından kalıtım almış bir tip olmak zorundadır.
    // Böylelikle bu sınıftan kalıtım alan diğer Session sınıfları kendi veritabanı tablo sınıfı için bu işlemlerin tekrar yazılmasına gerek duymaz.
    // Bu sınıftan kalıtım alacak olan alt Session sınıfları bazı metodları değiştirme ihtiyacı duyabileceğinden dolayı gerekli metodlar virtual olarak belirlenmiştir.
    // IHelperSession, tüm nesnelerde ortak çalışacak generic bir database helper niteliği taşımaktadır.
    // Uygun tüm metodlar asenkron olarak yapılandırılmıştır.
    public class CoreSession<TEntity> : ICoreSession<TEntity>, IHelperSession
        where TEntity : CoreEntity
    {
        private readonly ISession _session; // NHibernate kütüphanesinden gelen ISession. Veritabanı işlemlerinin entegre edilmesinde kullanılır.
        private ITransaction _transaction; // NHibernate kütüphanesinden gelen ITransaction. Takip işlemlerinin entegre edilmesinde kullanılır.

        public CoreSession(ISession session)
        {
            _session = session;
        }

        // Belli bir filtreye göre veya tüm kayıtları listeleme.
        // Eğer parametrede bir filtre (LINQ sorgusu) belirlenmediyse o tabloya ait tüm kayıtlar listelenir.
        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression == null
                ? await _session.Query<TEntity>().ToListAsync()
                : await _session.Query<TEntity>().Where(expression).ToListAsync();
        }

        // Belli bir filtreye göre tek kayıt arama.
        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return await _session.Query<TEntity>().FirstOrDefaultAsync(expression);
        }

        // Yeni bir kayıt ekleme.
        public virtual async Task Add(TEntity entity)
        {
            await _session.SaveAsync(entity);
        }

        // Mevcut bir kaydı güncelleme.
        public virtual async Task Update(TEntity entity)
        {
            await _session.UpdateAsync(entity);
        }

        // Mevcut bir kaydı silme.
        public virtual async Task Delete(TEntity entity)
        {
            await _session.DeleteAsync(entity);
        }

        // Takipe alınacak bir işlem başlatma.
        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        // Takipe alınan işlemi işleme.
        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }

        // Takipe alınan işlemi geriye sarma.
        public async Task RollbackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        // Takip işlemini sona erdirme.
        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        // Bir işlemi/işlemleri takip ettirerek yaptırma.
        // Bu sınıftan kalıtım alan diğer Session sınıfları bu metodu kendi içindeki ihtiyaca göre değiştirebilir.
        // Veri erişim katmanının prensiplerini bozmadan çalışan bu metod servis tarafında çağırılır.
        // Yapılacak veritabanı işlemini belirli validasyonlar çerçevesinde takibe bağlı olarak gerçekleştirir.
        // Bütün takip aşamaları yerine getirilerek istenilen veritabanı işlemini gerçekleştirmeye yarar.
        // İlk parametresi, takibe alınacak olan işlemin ne tür bir veritabanı işlemi olduğudur.
        // İkinci parametresi, hangi verinin bu işlemde kullanılacağı bilgisidir.
        // Üçüncü parametre ise opsiyonel olan ve request body'den gelecek olan bilgidir. Güncelleme işleminde gerek duyulur.
        public virtual async Task<ApiResponse> DbOperation(Operation operation, TEntity entity, TEntity? entityFromBody = null)
        {
            return operation switch
            {
                Operation.Add => ApiResponse.Added,
                Operation.Update => ApiResponse.Updated,
                Operation.Delete => ApiResponse.Deleted,
                _ => ApiResponse.None,
            };
        }

        // IHelperSession - Session nesnelerinden bağımsız olarak tipe özel şekilde çalışabilecek yardımcı (helper) metodlar.
        // Filtreye göre tek bir kaydı belirtilen nesneye göre arama.
        public async Task<Entity> GetEntityHelper<Entity>(Expression<Func<Entity, bool>> expression) where Entity : CoreEntity
        {
            return await _session.Query<Entity>().FirstOrDefaultAsync(expression);
        }

        // Filtreye göre tüm kayıtları belirtilen nesneye göre listeleme.
        public async Task<List<Entity>> GetEntityListHelper<Entity>(Expression<Func<Entity, bool>> expression) where Entity : CoreEntity
        {
            return await _session.Query<Entity>().Where(expression).ToListAsync();
        }

        // Tek bir kaydı belirtilen nesneye göre silme.
        public async Task DeleteHelper<Entity>(Entity entity) where Entity : CoreEntity
        {
            await _session.DeleteAsync(entity);
        }
    }
}
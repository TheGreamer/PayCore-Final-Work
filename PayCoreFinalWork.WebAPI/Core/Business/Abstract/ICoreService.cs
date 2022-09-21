using PayCoreFinalWork.Core.DataAccess.Abstract;
using PayCoreFinalWork.Core.Entity.Concrete;

namespace PayCoreFinalWork.Core.Business.Abstract
{
    // Tüm diğer servis interface'leri buradan kalıtım alır.
    // TEntity, CoreEntity sınıfından kalıtım almış bir sınıf olmalıdır.
    // TSession, belirlenen TEntity nesnesine ait uygun session nesnesi olmalıdr. Aksi halde hata verir.
    public interface ICoreService<TEntity, TSession> : IService<TEntity>
        where TEntity : CoreEntity
        where TSession : ICoreSession<TEntity>
    {
    }
}
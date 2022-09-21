using PayCoreFinalWork.Core.Business.Abstract;
using PayCoreFinalWork.Core.DataAccess.Abstract;
using PayCoreFinalWork.Core.Entity.Concrete;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.Core.WebAPI.Concrete.Responses;
using System.Linq.Expressions;

namespace PayCoreFinalWork.Core.Business.Concrete
{
    // Bütün ortak veritabanı ve servis işlemlerinin yer aldığı en genel iş sınıfıdır.
    // TEntity, CoreEntity sınıfından kalıtım almış bir sınıf olmalıdır.
    // TSession, belirlenen TEntity nesnesine ait uygun session nesnesi olmalıdr. Aksi halde hata verir.
    // Bu sınıfta yer alan tüm metodlar generic olarak belirlenmiş session nesnesine ait metodlarla iletişime geçer.
    // Ve o metodlara ait tüm veri işlemleri servis bölümünde iş metodu olarak ekstra iş metodlarının yanında implemente edilir.
    // Tüm methodlar virtual olarak belirlenmiştir. İhtiyaç durumunda burada yer alan metodlar, alt servisler tarafından değiştirilebilir.
    public class CoreService<TEntity, TSession> : ICoreService<TEntity, TSession>
        where TEntity : CoreEntity
        where TSession : ICoreSession<TEntity>
    {
        private readonly TSession _session;

        public CoreService(TSession session)
        {
            _session = session;
        }

        // Tüm kayıtları ya da belli bir filtreye göre arama yapılmış kayıtları listeme.
        public virtual async Task<CoreResponse<List<TEntity>>> GetAll(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression == null
                ? new CoreResponse<List<TEntity>>(await _session.GetList())
                : new CoreResponse<List<TEntity>>(await _session.GetList(expression));
        }

        // ID'ye göre tek bir kaydı listeleme.
        public virtual async Task<CoreResponse<TEntity>> GetById(Guid id)
        {
            return new CoreResponse<TEntity>(await _session.Get(e => e.Id == id));
        }

        // İhtiyaç doğrultusunda gerekli validasyonlar neticesinde takibe alınarak bir veritabanı işlemini gerçekleştirme.
        public virtual async Task<ApiResponse> StartTransactionalOperation(Operation operation, TEntity entity, TEntity? entityFromBody = null)
        {
            if (entityFromBody == null)
                return await _session.DbOperation(operation, entity);
            else
                return await _session.DbOperation(operation, entity, entityFromBody);
        }
    }
}
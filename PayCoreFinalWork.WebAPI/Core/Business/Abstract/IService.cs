using PayCoreFinalWork.Core.Entity.Concrete;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.Core.WebAPI.Concrete.Responses;
using System.Linq.Expressions;

namespace PayCoreFinalWork.Core.Business.Abstract
{
    // İhtiyaçta bulunulacak tüm nesnelere hizmet edecek ortak servis metodları burada yer alır.
    // Bir servis nesnesine erişilerek o nesneye ait session sınıfındaki metodlara gerektiği durumlarda ekstra servislerle erişilmesi amaçlanmıştır.
    // TEntity, CoreEntity sınıfından kalıtım almış bir sınıf olmalıdır.
    public interface IService<TEntity> where TEntity : CoreEntity
    {
        Task<CoreResponse<List<TEntity>>> GetAll(Expression<Func<TEntity, bool>>? expression = null); // Tüm kayıtları ya da belli bir filtreye göre arama yapılmış kayıtları listeme.
        Task<CoreResponse<TEntity>> GetById(Guid id); // ID'ye göre tek bir kaydı listeleme.
        Task<ApiResponse> StartTransactionalOperation(Operation operation, TEntity entity, TEntity? entityFromBody = null); // İhtiyaç doğrultusunda gerekli validasyonlar neticesinde takibe alınarak bir veritabanı işlemini gerçekleştirme.
    }
}
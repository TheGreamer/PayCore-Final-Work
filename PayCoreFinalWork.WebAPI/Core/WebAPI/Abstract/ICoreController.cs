using Microsoft.AspNetCore.Mvc;
using PayCoreFinalWork.Core.Dto.Abstract;
using PayCoreFinalWork.Core.Entity.Concrete;
using PayCoreFinalWork.Core.WebAPI.Concrete.Responses;

namespace PayCoreFinalWork.Core.WebAPI.Abstract
{
    // CoreController bu interface'i implemente eder.
    // Ortak action metodların yer alacağı interface'dir.
    // Generic operatörünün içerisinde belirlenecek TEntity CoreEntity'den kalıtım almış bir tip olmak ve TDto ise ICoreDto interface'ni impelmente etmiş bir sınıf olmak zorundadır.
    public interface ICoreController<TEntity, TDto>
        where TEntity : CoreEntity
        where TDto : ICoreDto
    {
        Task<CoreResponse<List<TEntity>>> GetAll(); // Tüm kayıtları listeleme.
        Task<CoreResponse<TEntity>> GetById(Guid? id); // Id'ye göre tek bir kayıt getirme.
        Task<CoreResponse> Add([FromBody] TDto dto); // Yeni bir kayıt ekleme.
        Task<CoreResponse> Update(Guid? id, [FromBody] TDto dto); // Mevcut bir kaydı güncelleme.
        Task<CoreResponse> Delete(Guid? id); // Mevcut bir kaydı silme.
    }
}
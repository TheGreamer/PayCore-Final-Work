using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCoreFinalWork.Core.Business.Abstract;
using PayCoreFinalWork.Core.Constants;
using PayCoreFinalWork.Core.Dto.Abstract;
using PayCoreFinalWork.Core.Entity.Concrete;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.Core.WebAPI.Abstract;
using PayCoreFinalWork.Core.WebAPI.Concrete.Responses;

namespace PayCoreFinalWork.Core.WebAPI.Concrete.Controllers
{
    // Ortak görevlere ihtiyaç duyan controller'lar buradan kalıtım alır.
    // Action metodların tamamı asenkron olarak entegre edilmiştir.
    // Bu sınıftan kalıtım alacak diğer controller sınıflarının ihtiyacı olması durumu göz önünde bulundurularak tüm action metodlar virtual olarak belirlenmiştir.
    // TEntity, CoreEntity sınıfından kalıtım almış bir sınıf olmak zorundadır.
    // TDto, ICoreDto interface'ini implemente almış bir nesne olmak zorundadır.
    // TService, IService'ın generic operatörünün değerine yazılan tipten implemente etmiş bir sınıf olmak zorundadır.
    // Mapping işlemleri AutoMapper ile yapılmıştır.
    // Loglama işlemleri Serilog ile yapılmıştır.
    // GetAll ve GetById action metodları AllowAnonymous olarak işaretlenerek giriş yapma zorunluluğu olmadan çalışabilmektedir.
    public class CoreController<TEntity, TDto, TService> : ControllerBase, ICoreController<TEntity, TDto>
        where TEntity : CoreEntity
        where TDto : ICoreDto
        where TService : IService<TEntity>
    {
        private readonly TService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<CoreController<TEntity, TDto, TService>> _logger;
        private readonly string entityType;

        public CoreController(TService service, IMapper mapper, ILogger<CoreController<TEntity, TDto, TService>> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            entityType = typeof(TEntity).Name;
        }

        // Tüm kayıtları listeleme.
        // Çalışabilmesi için sisteme giriş yapma zorunluluğu bulunmaması adına AllowAnonymous olarak işaretlenir.
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public virtual async Task<CoreResponse<List<TEntity>>> GetAll()
        {
            var list = await _service.GetAll();
            _logger.LogInformation($"All {entityType.ToLower()}s listed.");
            return list;
        }

        // Id'ye göre tek bir kayıt getirme.
        // Çalışabilmesi için sisteme giriş yapma zorunluluğu bulunmaması adına AllowAnonymous olarak işaretlenir.
        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public virtual async Task<CoreResponse<TEntity>> GetById(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError($"{entityType} could not found because id was null.");
                return new CoreResponse<TEntity>(SystemMessage.API_ID_NULL);
            }

            var entity = await _service.GetById((Guid)id);

            if (entity.Response == null)
            {
                _logger.LogError($"No {entityType.ToLower()} found.");
                return new CoreResponse<TEntity>(SystemMessage.API_NOT_FOUND);
            }

            _logger.LogInformation($"{entityType} found with ID : {id}");
            return entity;
        }

        // Yeni bir kayıt ekleme.
        [HttpPost("Add")]
        public virtual async Task<CoreResponse> Add([FromBody] TDto dto)
        {
            var state = await _service.StartTransactionalOperation(Operation.Add, _mapper.Map<TEntity>(dto));

            switch (state)
            {
                case ApiResponse.Added:
                    _logger.LogInformation($"{entityType} added successfully.");
                    return new CoreResponse(SystemMessage.API_ADDED, true);
                default:
                    _logger.LogError($"{entityType} could not added. Same {entityType.ToLower()} can not be added.");
                    return new CoreResponse(SystemMessage.API_SAME_DATA_ERROR, false);
            }
        }

        // Mevcut bir kaydı güncelleme.
        [HttpPut("Update")]
        public virtual async Task<CoreResponse> Update(Guid? id, [FromBody] TDto dto)
        {
            if (id == null)
            {
                _logger.LogError($"{entityType} could not found because id was null.");
                return new CoreResponse(SystemMessage.API_ID_NULL, false);
            }

            var entity = await _service.GetById((Guid)id);

            if (entity.Response == null)
            {
                _logger.LogError($"No {entityType.ToLower()} found.");
                return new CoreResponse(SystemMessage.API_NOT_FOUND, false);
            }

            var mapped = _mapper.Map<TEntity>(dto);
            var state = await _service.StartTransactionalOperation(Operation.Update, entity.Response, mapped);

            switch (state)
            {
                case ApiResponse.Updated:
                    _logger.LogInformation($"{entityType} with ID ({id}) updated successfully.");
                    return new CoreResponse(SystemMessage.API_UPDATED, true);
                default:
                    _logger.LogError($"{entityType} with ID ({id}) could not updated.");
                    return new CoreResponse(SystemMessage.API_UPDATE_ERROR, false);
            }
        }

        // Mevcut bir kaydı silme.
        [HttpDelete("Delete/{id}")]
        public virtual async Task<CoreResponse> Delete(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError($"{entityType} could not found because id was null.");
                return new CoreResponse(SystemMessage.API_ID_NULL, false);
            }

            var entity = await _service.GetById((Guid)id);

            if (entity.Response == null)
            {
                _logger.LogError($"No {entityType.ToLower()} found.");
                return new CoreResponse(SystemMessage.API_NOT_FOUND, false);
            }

            var state = await _service.StartTransactionalOperation(Operation.Delete, entity.Response);

            switch (state)
            {
                case ApiResponse.Deleted:
                    _logger.LogInformation($"{entityType} with ID ({id}) deleted successfully.");
                    return new CoreResponse(SystemMessage.API_DELETED, true);
                default:
                    _logger.LogError($"{entityType} with ID ({id}) could not deleted.");
                    return new CoreResponse(SystemMessage.API_DELETE_ERROR, false);
            }
        }
    }
}
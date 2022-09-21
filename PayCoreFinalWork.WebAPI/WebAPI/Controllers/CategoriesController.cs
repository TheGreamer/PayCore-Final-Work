using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Core.WebAPI.Concrete.Controllers;
using PayCoreFinalWork.Dto.Concrete;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.WebAPI.Controllers
{
    // Sistemde yer alan tüm kategoriler için CRUD işlemlerinin yapılmasına olanak tanıyan controller.
    // GetAll ve GetById dışındaki actionlar authorize olmayı gerektirir.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : CoreController<Category, CategoryDto, ICategoryService>
    {
        public CategoriesController(ICategoryService categoryService, IMapper mapper, ILogger<CategoriesController> logger) : base(categoryService, mapper, logger)
        {
        }
    }
}
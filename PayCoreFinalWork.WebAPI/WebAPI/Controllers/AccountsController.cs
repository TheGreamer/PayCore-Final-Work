using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Core.WebAPI.Concrete.Controllers;
using PayCoreFinalWork.Dto.Concrete;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.WebAPI.Controllers
{
    // Admin rolüyle giriş yapan kullanıcılar için erişilmesi amaçlanan controller.
    // Sistemdeki kullanıcılar için CRUD işlemlerini yapmaya olanak sağlayan controller.
    // Farklı kullanıcılar tüm kullanıcıların hesaplarını veya tek bir hesabı görüntüleyebilir.
    [Route("api/Admin/[controller]")]
    [ApiController]
    [Authorize]
    // [Authorize(Roles = Core.Enums.Roles.Admin)] // 403
    public class AccountsController : CoreController<Account, AccountDto, IAccountService>
    {
        public AccountsController(IAccountService accountService, IMapper mapper, ILogger<AccountsController> logger) : base(accountService, mapper, logger)
        {
        }
    }
}
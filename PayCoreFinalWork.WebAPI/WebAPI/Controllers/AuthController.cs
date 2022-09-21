using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Core.Constants;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.Core.WebAPI.Concrete.Requests;
using PayCoreFinalWork.Core.WebAPI.Concrete.Responses;
using PayCoreFinalWork.Dto.Concrete;
using PayCoreFinalWork.Entity.Concrete.Entities;
using PayCoreFinalWork.Business.Concrete.StaticServices;

namespace PayCoreFinalWork.WebAPI.Controllers
{
    // Giriş ve kayıt işlemleri.
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ITokenService tokenService, IAccountService accountService, IMapper mapper, ILogger<AuthController> logger)
        {
            _tokenService = tokenService;
            _accountService = accountService;
            _mapper = mapper;
            _logger = logger;
        }

        // Sisteme giriş yapma işlemi.
        // Giriş yapan kullanıcının parolası veritabanında şifrelenmiş şekilde yer alan paroladan çözülerek kontrol edilir.
        // Eğer istekte bulunulan kullanıcı hesabı kayıtlarda mevcutsa kullanıcı için token oluşturulur.
        [HttpPost("Login")]
        public async Task<CoreResponse<TokenResponse>> Login([FromBody] TokenRequest request)
        {
            var response = await _tokenService.GenerateToken(request);
            _logger.LogInformation($"Login process for {request.Email} : {response.Message} ({response.Success})");
            return response;
        }

        // Kayıt olma işlemi.
        // Eğer kayıt sırasında sistemde kayıtlı bir e-posta adresi veya kullanıcı adı kullanılıyorsa gerekli hata bilgisi döndürülerek kayıt işlemi başarısız olur.
        // Gerekli kontrollerden geçmesi durumunda kayıt olan kullanıcıya hangfire tarafında background service olarak MailKit kütüphanesi ile SMTP protokolü kullanılarak e-posta gönderilir.
        // E-posta gönderme işlemi devam ederken kayıt işlemi kaldığı yerden devam eder ve e-posta gönderme işlemi kayıt olma işleminin hızını bozmaz.
        // E-posta gönderimi sırasında hata olurda 4 kere daha göndermeye çalışılır.
        [HttpPost("Register")]
        public async Task<CoreResponse> Register([FromBody] AccountDto accountDto)
        {
            var registerationState = await _accountService.StartTransactionalOperation(Operation.Add, _mapper.Map<Account>(accountDto));

            switch (registerationState)
            {
                case ApiResponse.EmailExists:
                    _logger.LogError($"Registration failure for {accountDto.Email} : {SystemMessage.USER_EMAIL_EXISTS}.");
                    return new CoreResponse(SystemMessage.USER_EMAIL_EXISTS, false);

                case ApiResponse.UserNameExists:
                    _logger.LogError($"Registration failure for {accountDto.UserName} : {SystemMessage.USER_NAME_EXISTS}.");
                    return new CoreResponse(SystemMessage.USER_NAME_EXISTS, false);

                default:
                    var email = new EmailDto()
                    {
                        To = accountDto.Email,
                        Subject = $"Welcome {accountDto.FirstName} {accountDto.LastName}",
                        Body = "<h2>You succesfully registered</h2><br />" +
                               $"Your user name: <b>{accountDto.UserName}</b><br />" +
                               $"Your password: <b>{accountDto.Password}</b><br />" +
                               $"Your phone number: <b>{accountDto.PhoneNumber}</b>"
                    };
                    var jobId = BackgroundJob.Enqueue(() => EmailBackgroundService.SendEmail(email));

                    _logger.LogInformation($"Registration is successful for (User Name :{accountDto.UserName}) - (E-Mail : {accountDto.Email}) - (Phone Number : {accountDto.PhoneNumber})");
                    return new CoreResponse(SystemMessage.USER_REGISTERED + $" (Job ID: {jobId})", true);

                    // /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
                    // Hangfire servisi dahil edilmeden önceki e-posta gönderme yöntemi.
                    // Eski servise dair sınıf ve interface mevcut ancak kullanım dışı.
                    // Background worker olarak Hangfire ile e-posta gönderimini tercih ettim.
                    // Böylelikle kayıt olurken mailin gönderme sürecinin sona ermesini beklemeye gerek kalmadan action metod sonlandırılabilmekte.
                    // Eski yöntem ile e-posta gönderiminde bu durumun tam tersi söz konusuydu. Maili göndermeden action return kısmına ulaşamıyordu.
                    // İncelemek isteyenler için örnek olması açısından IEmailService ve EmailService'yi kaldırmadım. Sadece devre dışı bıraktım.
                    // Bu işi yapan aracın kesinlikle bir background worker olması lazım. Eski yöntemi asla tavsiye etmiyorum.
                    #region Eski yöntem ile e-posta gönderimi
                    /*
                    var response = await _emailService.SendEmail(email); // NOT: Kullanmak için IEmailService kurucu metoddan dahil edilmeli.

                    if (response)
                    {
                        return new CoreResponse(SystemMessage.USER_REGISTERED, true);
                    }
                    else
                    {
                        var account = (await _accountService.GetAll()).Response.FirstOrDefault(a => a.Email == accountDto.Email);
                        await _accountService.StartTransactionalOperation(Operation.Delete, account);
                        return new CoreResponse(SystemMessage.EMAIL_SEND_ERROR, false);
                    }
                    */
                    #endregion
            }
        }
    }
}
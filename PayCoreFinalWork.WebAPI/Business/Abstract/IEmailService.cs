using PayCoreFinalWork.Dto.Concrete;

namespace PayCoreFinalWork.Business.Abstract
{
    // (Kullanım dışı) Örnek olması açısından incelemek isteyenler için kaldırılmadı.
    // Bunun yerine EmailBackgroundService kullanılmaktadır.
    // E-Posta gönderme işlemini yerine getiren servis.
    [Obsolete("This service is obsolete. Use the static service EmailBackgroundService instead.")]
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailDto request);
    }
}
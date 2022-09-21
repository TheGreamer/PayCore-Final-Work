using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using PayCoreFinalWork.Dto.Concrete;

namespace PayCoreFinalWork.Business.Concrete.StaticServices
{
    // E-Posta gönderme işlemini yerine getiren servis.
    // E-Posta gönderme işlemlerini yerine getirmek için sistem tarafında tutulan test e-postası yer almaktadır.
    // MailKit kütüphanesi aracılığıyla tüm işlemler yerine getirilmektedir.
    // SMTP client kullanılarak e-posta gönderim işlemi sağlanmaktadır.
    // Bu servis bir background worker olan Hangfire ile çalıştırılmak için kullanılmıştır.
    // E-Posta gönderme ihtiyacının bulunduğu yerlerde sistemin ilerleyişini yavaşlatmamak için arkaplan servisi olarak çalıştırılmaktadır.
    // E-Posta gönderme sırasında hata olması durumunda e-posta gönderme işlemi 4 kere daha tekrar edilir.
    // 5. denemede ise tekrar hata verirse gönderme işlemi sona erdirilir.
    // Varsayılan deneme sayısı 5 olarak ayarlanmıştır ve değiştirilebilir.
    public static class EmailBackgroundService
    {
        public static bool SendEmail(EmailDto request)
        {
            var emailHost = "smtp-mail.outlook.com";
            var emailPort = 587;
            var emailUserName = "cvpooldb_greamer@outlook.com";
            var emailPassword = "_5aXCvbh59";

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailUserName));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            int tryCount = request.TryCount;

        TryAgain:
            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(emailHost, emailPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(emailUserName, emailPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch
            {
                tryCount--;

                if (tryCount == 0)
                    return false;
                else
                    goto TryAgain;
            }

            return true;
        }
    }
}
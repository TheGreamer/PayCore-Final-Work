using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Dto.Concrete;

namespace PayCoreFinalWork.Business.Concrete.Services
{
    // (Kullanım dışı) Örnek olması açısından incelemek isteyenler için kaldırılmadı.
    // Bunun yerine EmailBackgroundService kullanılmaktadır.
    // E-Posta gönderme işlemini yerine getiren servis.
    [Obsolete("This service is obsolete. Use the static service EmailBackgroundService instead.")]
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmail(EmailDto request)
        {
            var emailHost = _config.GetSection("EmailHost").Value;
            var emailPort = int.Parse(_config.GetSection("EmailPort").Value);
            var emailUserName = _config.GetSection("EmailUsername").Value;
            var emailPassword = _config.GetSection("EmailPassword").Value;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailUserName));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            try
            {
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(emailHost, emailPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(emailUserName, emailPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
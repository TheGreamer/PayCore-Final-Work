using FluentValidation;
using PayCoreFinalWork.Core.Constants;
using PayCoreFinalWork.Core.WebAPI.Concrete.Requests;

namespace PayCoreFinalWork.Business.Concrete.ValidationRules
{
    // Giriş yaparken yer alacak validasyon.
    // TokenRequest sınıfına ait validasyon işlemleri.
    // FluentValidation kütüphanesinden gelmektedir.
    public class LoginValidator : AbstractValidator<TokenRequest>
    {
        public LoginValidator()
        {
            // E-posta alanı uygun bir e-posta adresi olmak zorundadır.
            RuleFor(s => s.Email)
                .EmailAddress().WithMessage(SystemMessage.USER_EMAIL_ERROR);

            // Şifre alanı boş bırakılamaz ve 8 ile 20 karakter arasında bir uzunlukta olmalıdır.
            RuleFor(t => t.Password)
                .NotEmpty().WithMessage(SystemMessage.USER_PASSWORD_EMPTY_ERROR)
                .Length(8, 20).WithMessage(SystemMessage.USER_PASSWORD_LENGTH_ERROR);
        }
    }
}
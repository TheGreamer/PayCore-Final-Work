using FluentValidation;
using PayCoreFinalWork.Core.Constants;
using PayCoreFinalWork.Dto.Concrete;
using System.Text.RegularExpressions;

namespace PayCoreFinalWork.Business.Concrete.ValidationRules
{
    // AccountDto sınıfına ait validasyon işlemleri.
    // FluentValidation kütüphanesinden gelmektedir.
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        public AccountValidator()
        {
            // Ad alanı boş bırakılamaz ve en fazla 40 karakter uzunluğunda olmalıdır.
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage(SystemMessage.USER_FIRST_NAME_EMPTY_ERROR)
                .MaximumLength(40).WithMessage(SystemMessage.USER_FIRST_NAME_LENGTH_ERROR);

            // Soyad alanı boş bırakılamaz ve en fazla 40 karakter uzunluğunda olmalıdır.
            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage(SystemMessage.USER_LAST_NAME_EMPTY_ERROR)
                .MaximumLength(40).WithMessage(SystemMessage.USER_LAST_NAME_LENGTH_ERROR);

            // Kullanıcı adı alanı boş bırakılamaz ve en fazla 25 karakter uzunluğunda olmalıdır.
            RuleFor(a => a.UserName)
                .NotEmpty().WithMessage(SystemMessage.USER_NAME_EMPTY_ERROR)
                .MaximumLength(25).WithMessage(SystemMessage.USER_NAME_LENGTH_ERROR);

            // E-posta alanı uygun bir e-posta adresi olmak zorundadır.
            RuleFor(a => a.Email)
                .EmailAddress().WithMessage(SystemMessage.USER_EMAIL_ERROR);

            // Şifre alanı boş bırakılamaz ve 8 ile 20 karakter arasında bir uzunlukta olmalıdır.
            // En az 1 sayı, 1 küçük harf ve 1 büyük harf içermelidir.
            RuleFor(a => a.Password)
                .NotEmpty().WithMessage(SystemMessage.USER_PASSWORD_EMPTY_ERROR)
                .Length(8, 20).WithMessage(SystemMessage.USER_PASSWORD_LENGTH_ERROR)
                .Must(ContainNumber).WithMessage(SystemMessage.USER_PASSWORD_NUMBER_ERROR)
                .Must(ContainUpperCaseLetter).WithMessage(SystemMessage.USER_PASSWORD_UPPER_CASE_ERROR)
                .Must(ContainLowerCaseLetter).WithMessage(SystemMessage.USER_PASSWORD_LOWER_CASE_ERROR);

            // Telefon numarası alanı boş bırakılamaz ve "+90 212 123 12 12" formatına uygun olmalıdır.
            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage(SystemMessage.USER_PHONE_EMPTY_ERROR)
                .Matches(new Regex(@"^\+(\d{2})\s(\d{3})\s(\d{3})\s(\d{2})\s(\d{2})")).WithMessage(SystemMessage.USER_PHONE_REGEX_ERROR);
        }

        // En az 1 sayı kontrolü.
        private bool ContainNumber(string password) => password.Any(char.IsDigit);

        // En az 1 büyük harf kontrolü.
        private bool ContainUpperCaseLetter(string password) => password.Any(char.IsUpper);

        // En az 1 küçük harf kontrolü.
        private bool ContainLowerCaseLetter(string password) => password.Any(char.IsLower);
    }
}
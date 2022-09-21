using FluentValidation;
using PayCoreFinalWork.Core.Constants;
using PayCoreFinalWork.Dto.Concrete;

namespace PayCoreFinalWork.Business.Concrete.ValidationRules
{
    // CategoryDto sınıfına ait validasyon işlemleri.
    // FluentValidation kütüphanesinden gelmektedir.
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            // Ad alanı boş bırakılamaz ve maksimum 50 karakter uzunluğunda olmalıdır.
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage(SystemMessage.CATEGORY_NAME_EMPTY_ERROR)
                .MaximumLength(50).WithMessage(SystemMessage.CATEGORY_NAME_LENGTH_ERROR);

            // Açıklama alanı boş olabilir ve maksimum 1000 karakter uzunluğunda olmalıdır.
            RuleFor(t => t.Description)
                .MaximumLength(1000).WithMessage(SystemMessage.CATEGORY_DESCRIPTION_LENGTH_ERROR);
        }
    }
}
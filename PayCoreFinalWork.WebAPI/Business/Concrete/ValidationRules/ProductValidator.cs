using FluentValidation;
using PayCoreFinalWork.Core.Constants;
using PayCoreFinalWork.Dto.Concrete;

namespace PayCoreFinalWork.Business.Concrete.ValidationRules
{
    // ProductDto sınıfına ait validasyon işlemleri.
    // FluentValidation kütüphanesinden gelmektedir.
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            // Ad alanı boş bırakılamaz ve en fazla 100 karakter uzunluğunda olmalıdır.
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(SystemMessage.PRODUCT_NAME_EMPTY_ERROR)
                .MaximumLength(100).WithMessage(SystemMessage.PRODUCT_NAME_LENGTH_ERROR);

            // Ad alanı boş bırakılamaz ve en fazla 500 karakter uzunluğunda olmalıdır.
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage(SystemMessage.PRODUCT_DESCRIPTION_EMPTY_ERROR)
                .MaximumLength(500).WithMessage(SystemMessage.PRODUCT_DESCRIPTION_LENGTH_ERROR);

            // Kategori numarası alanı boş bırakılamaz.
            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage(SystemMessage.PRODUCT_CATEGORYID_EMPTY_ERROR);

            // Renk alanı boş bırakılamaz.
            RuleFor(p => p.Color)
                .NotEmpty().WithMessage(SystemMessage.PRODUCT_COLOR_EMPTY_ERROR);

            // Marka alanı boş bırakılamaz.
            RuleFor(p => p.Brand)
                .NotEmpty().WithMessage(SystemMessage.PRODUCT_BRAND_EMPTY_ERROR);

            // Fiyat alanı boş bırakılamaz.
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage(SystemMessage.PRODUCT_PRICE_EMPTY_ERROR);

            // Teklif verilebilir olma alanı boş bırakılamaz.
            RuleFor(p => p.IsOfferable)
                .NotNull().WithMessage(SystemMessage.PRODUCT_IS_OFFERABLE_EMPTY_ERROR);
        }
    }
}
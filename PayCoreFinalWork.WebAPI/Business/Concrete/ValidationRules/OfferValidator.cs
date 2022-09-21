using FluentValidation;
using PayCoreFinalWork.Core.Constants;
using PayCoreFinalWork.Dto.Concrete;

namespace PayCoreFinalWork.Business.Concrete.ValidationRules
{
    // OfferDto sınıfına ait validasyon işlemleri.
    // FluentValidation kütüphanesinden gelmektedir.
    public class OfferValidator : AbstractValidator<OfferDto>
    {
        public OfferValidator()
        {
            // Ürün numarası alanı boş bırakılamaz.
            RuleFor(o => o.ProductId)
                .NotEmpty().WithMessage(SystemMessage.OFFER_PRODUCT_EMPTY_ERROR);

            // Fiyat alanı boş bırakılamaz ve 1 ile 50000 arasında olmak zorundadır.
            RuleFor(o => o.Price)
                .NotEmpty().WithMessage(SystemMessage.OFFER_PRICE_EMPTY_ERROR)
                .InclusiveBetween(1, 50000).WithMessage(SystemMessage.OFFER_PRICE_RANGE_ERROR);
        }
    }
}
using AutoMapper;
using PayCoreFinalWork.Dto.Concrete;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Business.Concrete.MappingProfiles
{
    // Cast işlemlerinin otomatik olarak gerçekleştirilebilmesi için gerekli sınıftır.
    // AutoMapper kütüphanesinden gelmektedir.
    // Yer alan tüm entity ve dto'ların mapping işlemi bu sınıfın kurucu metodu içerisinde gerçekleştirilir.
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<AccountDto, Account>().ReverseMap();
            CreateMap<OfferDto, Offer>().ReverseMap();
        }
    }
}
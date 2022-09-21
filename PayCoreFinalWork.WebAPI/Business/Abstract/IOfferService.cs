using PayCoreFinalWork.Core.Business.Abstract;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Business.Abstract
{
    // Offer sınıfına ait servis interface'i.
    // ICoreService'den gerekli ortak özelliklerin kalıtımı alınır.
    // İhtiyaç halinde yalnızca bu servis'a özel olacak metodlar burada belirlenir.
    public interface IOfferService : ICoreService<Offer, IOfferSession>
    {
        Task<ApiResponse> AnswerOffer(Offer offer, bool accepted); // Yapılan bir teklifin onaylanması veya reddedilmesi.
    }
}
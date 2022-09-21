using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Core.Business.Concrete;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Business.Concrete.Services
{
    // Offer sınıfına ait servis sınıfı.
    // CoreService'den kalıtım alınır. Generic operatörü değer olarak Entity ve Session nesnesi bekler.
    // Ayriyeten eğer sadece bu sınıfta yer alacak Offer servisleri yer alacaksa IOfferService implemente edilir.
    public class OfferService : CoreService<Offer, IOfferSession>, IOfferService
    {
        private readonly IOfferSession _offerSession;

        public OfferService(IOfferSession offerSession) : base(offerSession)
        {
            _offerSession = offerSession;
        }
        
        // Yapılan bir teklifin onaylanması veya reddedilmesi.
        // Belirtilen duruma göre teklif kabul veya reddedilir.
        // Hata durumunda işlem iptal edilir.
        public async Task<ApiResponse> AnswerOffer(Offer offer, bool accepted)
        {
            bool state = true;

            try
            {
                _offerSession.BeginTransaction();
                offer.IsAccepted = accepted;
                await _offerSession.Update(offer);
                await _offerSession.CommitTransaction();
            }
            catch
            {
                state = false;
                await _offerSession.RollbackTransaction();
            }
            finally
            {
                _offerSession.CloseTransaction();
            }

            if (state && accepted)
                return ApiResponse.OfferAccepted;
            else if (state && !accepted)
                return ApiResponse.OfferDenied;
            else
                return ApiResponse.OfferAnswerFailure;
        }
    }
}
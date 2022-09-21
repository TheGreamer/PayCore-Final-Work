using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Business.Concrete.StaticServices;
using PayCoreFinalWork.Core.Constants;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.Core.WebAPI.Concrete.Controllers;
using PayCoreFinalWork.Core.WebAPI.Concrete.Responses;
using PayCoreFinalWork.Dto.Concrete;
using PayCoreFinalWork.Entity.Concrete.Entities;
using System.Security.Claims;

namespace PayCoreFinalWork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OffersController : CoreController<Offer, OfferDto, IOfferService>
    {
        private readonly IOfferService _offerService;
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<OffersController> _logger;

        public OffersController(IOfferService offerService, IProductService productService, IAccountService accountService, IMapper mapper, ILogger<OffersController> logger) : base(offerService, mapper, logger)
        {
            _offerService = offerService;
            _productService = productService;
            _accountService = accountService;
            _mapper = mapper;
            _logger = logger;
        }

        // Kullanıcıların başka kullanıcıların ürünlerine yaptığı tekliflerin listelenmesi.
        [HttpGet("GetOffers")]
        public async Task<CoreResponse<List<Offer>>> GetOffers()
        {
            var currentUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUserName = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Name).Value;

            var userOffers = await _offerService.GetAll(o => o.AccountId.ToString() == currentUserId);
            _logger.LogInformation($"{currentUserName} listed their offers that has been made to other user's offers.");
            return userOffers;
        }

        // Kullanıcıların kendi ürünlerine başka kullanıcılar tarafından aldığı tekliflerin listelenmesi.
        [HttpGet("GetReceivedOffers")]
        public async Task<CoreResponse<List<Offer>>> GetReceivedOffers()
        {
            var currentUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUserName = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Name).Value;
            var receivedOffers = await _offerService.GetAll(o => o.OfferedUserId.ToString() == currentUserId);

            _logger.LogInformation($"{currentUserName} listed their own offers.");
            return receivedOffers;
        }

        // CoreController'dan gelen GetAll override edilerek bu controller'da NonAction olarak işaretlenir.
        // Böylece kullanıcılar sistemde yer alan tüm teklifleri listeleyemez.
        [NonAction]
        public override async Task<CoreResponse<List<Offer>>> GetAll() => null;

        // CoreController'dan gelen GetById override edilerek bu controller'da NonAction olarak işaretlenir.
        // Böylece kullanıcılar sistemde yer alan teklifleri ID'ye göre aratamaz.
        [NonAction]
        public override async Task<CoreResponse<Offer>> GetById(Guid? id) => null;

        // Teklif yapıldıktan sonra farklı hata durumlarında dönüş mesajlarının daha açıklayıcı olması için CoreController'dan override edilmiştir.
        // Teklif başarılı bir şekilde yapılırsa teklifi yapan ve teklifin yapıldığı kişilere bilgilendirme e-postası gönderilir.
        // Teklifin yapılamamasının nedenleri: yapılan teklifin zaten daha önceden yapılmış olması, teklif yapılan ürünün teklif yapılamaz durumda olması.
        [HttpPost("MakeOffer")]
        public override async Task<CoreResponse> Add([FromBody] OfferDto dto)
        {
            var state = await _offerService.StartTransactionalOperation(Operation.Add, _mapper.Map<Offer>(dto));

            return state switch
            {
                ApiResponse.OfferExists => new CoreResponse(SystemMessage.OFFER_EXISTS_ERROR, false),
                ApiResponse.IsNotOfferable => new CoreResponse(SystemMessage.OFFER_IS_NOT_OFFERABLE, false),
                _ => new CoreResponse(SystemMessage.OFFER_SUCCESSFULL, true),
            };
        }

        // Kullanıcıların yalnızca kendi yaptığı teklifleri güncelleyebilmeleri adına CoreController'dan override edilmiştir.
        [HttpPut("UpdateOffer")]
        public override async Task<CoreResponse> Update(Guid? id, [FromBody] OfferDto dto)
        {
            if (id == null)
            {
                _logger.LogError("Offer could not found because id was null.");
                return new CoreResponse(SystemMessage.API_ID_NULL, false);
            }

            var entity = await _offerService.GetById((Guid)id);

            if (entity.Response == null)
            {
                _logger.LogError("No offer found.");
                return new CoreResponse(SystemMessage.API_NOT_FOUND, false);
            }

            var currentUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (entity.Response.AccountId.ToString() != currentUserId)
            {
                _logger.LogError($"Offer with ID ({id}) could not updated. {SystemMessage.OFFER_IS_ANOTHER_USERS_ERROR}");
                return new CoreResponse(SystemMessage.OFFER_IS_ANOTHER_USERS_ERROR, false);
            }

            var mapped = _mapper.Map<Offer>(dto);
            var state = await _offerService.StartTransactionalOperation(Operation.Update, entity.Response, mapped);

            switch (state)
            {
                case ApiResponse.Updated:
                    _logger.LogInformation($"Offer with ID ({id}) updated successfully.");
                    return new CoreResponse(SystemMessage.API_UPDATED, true);
                default:
                    _logger.LogError($"Offer with ID ({id}) could not updated.");
                    return new CoreResponse(SystemMessage.OFFER_NOT_EXISTS_ERROR, false);
            }
        }

        // Kullanıcıların yalnızca kendi yaptığı teklifleri iptal edebilmeleri (silme) adına CoreController'dan override edilmiştir.
        [HttpDelete("CancelOffer")]
        public override async Task<CoreResponse> Delete(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError("Offer could not found because id was null.");
                return new CoreResponse(SystemMessage.API_ID_NULL, false);
            }

            var entity = await _offerService.GetById((Guid)id);

            if (entity.Response == null)
            {
                _logger.LogError("No offer found.");
                return new CoreResponse(SystemMessage.API_NOT_FOUND, false);
            }

            var currentUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (entity.Response.AccountId.ToString() != currentUserId)
            {
                _logger.LogError($"Offer with ID ({id}) could not deleted. {SystemMessage.OFFER_IS_ANOTHER_USERS_ERROR}");
                return new CoreResponse(SystemMessage.OFFER_IS_ANOTHER_USERS_ERROR, false);
            }

            var state = await _offerService.StartTransactionalOperation(Operation.Delete, entity.Response);

            switch (state)
            {
                case ApiResponse.Deleted:
                    _logger.LogInformation($"Offer with ID ({id}) deleted successfully.");
                    return new CoreResponse(SystemMessage.API_DELETED, true);
                default:
                    _logger.LogError($"Offer with ID ({id}) could not deleted.");
                    return new CoreResponse(SystemMessage.API_DELETE_ERROR, false);
            }
        }

        // Kullanıcıların ürünleri için aldığı teklifleri kabul veya reddedebilme işlemi.
        // Belirlenen teklifin yapıldığı kişi bilgisi giriş yapan kullanıcıya ait değilse işlem iptal edilir.
        // Belirtilen duruma göre teklif kabul veya reddedilir.
        // Teklifin red edilmesi durumunda işlem sona erdirilir. Ürün satışı yapılmaz ve teklifi yapan kullanıcıya detaylı bir bilgilendirme e-postası gönderilir.
        // Teklifin kabul edilmesi durumunda ürün satış işlemi başlar.
        // Satış başarılı şekilde yapıldıysa satılan ürünün satılmış olma durumu satılmış olarak ve teklif verilebilir olma durumu tekrar teklif verilemeyecek şekilde güncellenir.
        // Teklifin kabul edilmesiyle ve satışın başarılı şekilde sonuçlanmasıyla ürüne teklif gönderen kullanıcıya detaylı bir bilgilendirme e-postası gönderilir.
        // Zaten teklifi kabul edilmiş ürünlerin teklif değerlendirmesi tekrar yapılamaz. Ancak her ihtimale karşın bunun da kontrolü yapılır ve bu senaryoda satış daha önceden yapıldığı için iptal edilir.
        [HttpPut("AnswerOffer")]
        public async Task<CoreResponse> AnswerOffer(Guid? id, bool? accepted)
        {
            if (id == null || accepted == null)
            {
                _logger.LogError("Offer could not found because id or accept state was null.");
                return new CoreResponse(SystemMessage.API_ID_NULL, false);
            }

            var offer = (await _offerService.GetById((Guid)id)).Response;

            if (offer == null)
            {
                _logger.LogError("No offer found.");
                return new CoreResponse(SystemMessage.API_NOT_FOUND, false);
            }

            var currentUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (offer.OfferedUserId.ToString() != currentUserId)
            {
                _logger.LogError($"User ({currentUserId}) tried to answer an offer that is another user's.");
                return new CoreResponse(SystemMessage.OFFER_IS_ANOTHER_USERS_ERROR, false);
            }

            var sellerAccount = (await _accountService.GetById(new Guid(currentUserId))).Response;
            var productToBuy = (await _productService.GetById(offer.ProductId)).Response;
            var buyerAccount = (await _accountService.GetById(offer.AccountId)).Response;
            var state = await _offerService.AnswerOffer(offer, (bool)accepted);

            switch (state)
            {
                case ApiResponse.OfferAccepted:
                    var buyState = await _productService.BuyProduct(productToBuy);

                    if (buyState == ApiResponse.ProductBuySuccess)
                    {
                        var emailToBuyer = new EmailDto()
                        {
                            To = buyerAccount.Email,
                            Subject = "Offer accepted",
                            Body = "<h3>Your purchase has been successfully made</h3>" +
                                   "<h4 style='text-decoration: underline'>Seller Details</h4>" +
                                   $"Name: <b>{sellerAccount.FirstName} {sellerAccount.LastName}</b><br />" +
                                   $"User Name: <b>{sellerAccount.UserName}</b><br />" +
                                   $"E-Mail: <b>{sellerAccount.Email}</b><br />" +
                                   $"Phone Number: <b>{sellerAccount.PhoneNumber}</b><br />" +
                                   "<h4 style='text-decoration: underline'>Product Details</h4>" +
                                   $"ID: <b>{productToBuy.Id}</b><br />" +
                                   $"Name: <b>{productToBuy.Name}</b><br />" +
                                   $"Brand: <b>{productToBuy.Brand}</b><br />" +
                                   $"Color: <b>{productToBuy.Color}</b><br />" +
                                   $"First Price: <b>{productToBuy.Price}</b><br />" +
                                   $"Accepted Offer's Price: <b>{offer.Price}</b><br />" +
                                   $"Description: <b>{productToBuy.Description}</b>"
                        };
                        var buyerEmailJob = BackgroundJob.Enqueue(() => EmailBackgroundService.SendEmail(emailToBuyer));

                        _logger.LogInformation($"Product purchase for user ({buyerAccount.UserName}) from ({sellerAccount.UserName}) succeeded. Product : {productToBuy.Name} ({productToBuy.Id})");
                        return new CoreResponse(SystemMessage.OFFER_ACCEPTED + $" (Job ID: ({buyerEmailJob})", true);
                    }
                    else if (buyState == ApiResponse.ProductIsSold)
                    {
                        _logger.LogError($"User ({buyerAccount.UserName}) tried to buy a product that has been already sold. Product : {productToBuy.Name} ({productToBuy.Id})");
                        return new CoreResponse(SystemMessage.PRODUCT_IS_SOLD, false);
                    }
                    else
                    {
                        _logger.LogError($"Product purchase for user ({buyerAccount.UserName}) failed. Product : {productToBuy.Name} ({productToBuy.Id})");
                        return new CoreResponse(SystemMessage.PRODUCT_BUY_FAILURE, false);
                    }

                case ApiResponse.OfferDenied:
                    var denyEmailToBuyer = new EmailDto()
                    {
                        To = buyerAccount.Email,
                        Subject = "Offer denied",
                        Body = "<h3>The offer you made has been denied</h3>" +
                               "<h4 style='text-decoration: underline'>Seller Details</h4>" +
                               $"Name: <b>{sellerAccount.FirstName} {sellerAccount.LastName}</b><br />" +
                               $"User Name: <b>{sellerAccount.UserName}</b><br />" +
                               $"E-Mail: <b>{sellerAccount.Email}</b><br />" +
                               $"Phone Number: <b>{sellerAccount.PhoneNumber}</b><br />" +
                               "<h4 style='text-decoration: underline'>Product Details</h4>" +
                               $"ID: <b>{productToBuy.Id}</b><br />" +
                               $"Name: <b>{productToBuy.Name}</b><br />" +
                               $"Brand: <b>{productToBuy.Brand}</b><br />" +
                               $"Color: <b>{productToBuy.Color}</b><br />" +
                               $"First Price: <b>{productToBuy.Price}</b><br />" +
                               $"Denied Offer's Price: <b>{offer.Price}</b><br />" +
                               $"Description: <b>{productToBuy.Description}</b>"
                    };
                    var denyEmailJob = BackgroundJob.Enqueue(() => EmailBackgroundService.SendEmail(denyEmailToBuyer));

                    _logger.LogInformation($"Product purchase for user ({buyerAccount.UserName}) from ({sellerAccount.UserName}) succeeded. Product : {productToBuy.Name} ({productToBuy.Id})");
                    return new CoreResponse(SystemMessage.OFFER_DENIED + $" (Job ID: ({denyEmailJob})", true);

                default:
                    return new CoreResponse(SystemMessage.OFFER_ANSWER_FAILURE, false);
            }
        }
    }
}
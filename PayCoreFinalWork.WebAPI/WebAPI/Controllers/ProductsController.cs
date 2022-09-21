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
using PayCoreFinalWork.Core.WebAPI.Concrete.Requests;
using System.Security.Claims;

namespace PayCoreFinalWork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : CoreController<Product, ProductDto, IProductService>
    {
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, IAccountService accountService, IMapper mapper, ILogger<ProductsController> logger) : base(productService, mapper, logger)
        {
            _productService = productService;
            _accountService = accountService;
            _mapper = mapper;
            _logger = logger;
        }

        // Giriş yapan kullanıcıya ait ürünleri listeleme.
        // CoreController'dan gelen GetAll ile karıştırılmamalı. GetAll sistemde yer alan tüm ürünleri listeler.
        // Daha detaylı bir örnek sunmak gerekirse; OffersController'da GetAll override edilmiştir.
        // Sebebi ise giriş yapan kullanıcının sistemde yer alan tüm teklifleri değilde sadece kendi yaptığı teklifleri listeleyebilmesi sağlamaktır..
        [HttpGet("GetProducts")]
        public async Task<CoreResponse<List<Product>>> GetProducts()
        {
            var currentUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUserName = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Name).Value;

            var userProducts = await _productService.GetAll(p => p.AccountId.ToString() == currentUserId);
            _logger.LogInformation($"{currentUserName} listed their own products.");
            return userProducts;
        }

        // Ürünleri kategori numarasına göre listeleme.
        // Listelemek için giriş yapmaya gerek olmaması için AllowAnonymous olarak işaretlenmiştir.
        [HttpGet("GetProductsByCategoryId/{id}")]
        [AllowAnonymous]
        public async Task<CoreResponse<List<Product>>> GetProductsByCategoryId(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError("Products could not found because id was null.");
                return new CoreResponse<List<Product>>(null, SystemMessage.API_ID_NULL, false);
            }

            var products = await _productService.GetAll(p => p.CategoryId == id);

            if (products.Response == null)
            {
                _logger.LogError("No product found.");
                return new CoreResponse<List<Product>>(products.Response, SystemMessage.API_NOT_FOUND);
            }

            return products;
        }

        // Kullanıcıların yalnızca kendi eklediği ürünleri güncelleyebilmeleri adına CoreController'dan override edilmiştir.
        [HttpPut("Update")]
        public override async Task<CoreResponse> Update(Guid? id, [FromBody] ProductDto dto)
        {
            if (id == null)
            {
                _logger.LogError("Product could not found because id was null.");
                return new CoreResponse(SystemMessage.API_ID_NULL, false);
            }

            var entity = await _productService.GetById((Guid)id);

            if (entity.Response == null)
            {
                _logger.LogError("No product found.");
                return new CoreResponse(SystemMessage.API_NOT_FOUND, false);
            }

            var currentUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (entity.Response.AccountId.ToString() != currentUserId)
            {
                _logger.LogError($"Product with ID ({id}) could not updated. {SystemMessage.PRODUCT_IS_ANOTHER_USERS_ERROR}");
                return new CoreResponse(SystemMessage.PRODUCT_IS_ANOTHER_USERS_ERROR, false);
            }

            var mapped = _mapper.Map<Product>(dto);
            var state = await _productService.StartTransactionalOperation(Operation.Update, entity.Response, mapped);

            switch (state)
            {
                case ApiResponse.Updated:
                    _logger.LogInformation($"Product with ID ({id}) updated successfully.");
                    return new CoreResponse(SystemMessage.API_UPDATED, true);
                default:
                    _logger.LogError($"Product with ID ({id}) could not updated.");
                    return new CoreResponse(SystemMessage.API_UPDATE_ERROR, false);
            }
        }

        // Kullanıcıların yalnızca kendi eklediği ürünleri silebilmeleri adına CoreController'dan override edilmiştir.
        [HttpDelete("Delete/{id}")]
        public override async Task<CoreResponse> Delete(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError("Product could not found because id was null.");
                return new CoreResponse(SystemMessage.API_ID_NULL, false);
            }

            var entity = await _productService.GetById((Guid)id);

            if (entity.Response == null)
            {
                _logger.LogError("No product found.");
                return new CoreResponse(SystemMessage.API_NOT_FOUND, false);
            }

            var currentUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (entity.Response.AccountId.ToString() != currentUserId)
            {
                _logger.LogError($"Product with ID ({id}) could not deleted. {SystemMessage.PRODUCT_IS_ANOTHER_USERS_ERROR}");
                return new CoreResponse(SystemMessage.PRODUCT_IS_ANOTHER_USERS_ERROR, false);
            }

            var state = await _productService.StartTransactionalOperation(Operation.Delete, entity.Response);

            switch (state)
            {
                case ApiResponse.Deleted:
                    _logger.LogInformation($"Product with ID ({id}) deleted successfully.");
                    return new CoreResponse(SystemMessage.API_DELETED, true);
                default:
                    _logger.LogError($"Product with ID ({id}) could not deleted.");
                    return new CoreResponse(SystemMessage.API_DELETE_ERROR, false);
            }
        }

        // Ürün satın alma işlemi.
        // Satın alınan ürünün satılmış olma durumuna ait "IsSold" değeri true olarak ve teklif verilebilme durumuna ait "IsOfferable" değeri false olarak belirlenir.
        // Ürün başarılı bir şekilde sistemde aksaklık olmadan satın alındıysa satın alan kullanıcıya bilgilendirme e-postası gönderilir.
        // Aynı zamanda ürünün sahibinede ürününün satıldığına dair detaylı bir bilgilendirme e-postası gönderilir. Hangi ürünün satıldığı ve hangi kullanıcının satın aldığı bilgileri yer alır.
        [HttpPut("BuyProduct")]
        public async Task<CoreResponse> BuyProduct([FromBody] BuyProductRequest dto)
        {
            var currentUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var buyerAccount = (await _accountService.GetById(new Guid(currentUserId))).Response;
            var productToBuy = (await _productService.GetById(dto.ProductId)).Response;
            var state = await _productService.BuyProduct(productToBuy);

            if (state == ApiResponse.ProductBuySuccess)
            {
                var emailToBuyer = new EmailDto()
                {
                    To = buyerAccount.Email,
                    Subject = "Product Purchase",
                    Body = "<h3>Your purchase has been successfully made</h3>" +
                           "<h4 style='text-decoration: underline'>Product Details</h4>" +
                           $"ID: <b>{productToBuy.Id}</b><br />" +
                           $"Name: <b>{productToBuy.Name}</b><br />" +
                           $"Brand: <b>{productToBuy.Brand}</b><br />" +
                           $"Color: <b>{productToBuy.Color}</b><br />" +
                           $"Price: <b>{productToBuy.Price}</b><br />" +
                           $"Description: <b>{productToBuy.Description}</b>"
                };

                var productSeller = (await _productService.GetById(productToBuy.Id)).Response.AccountId;
                var sellerAccount = (await _accountService.GetById(productSeller)).Response;

                var emailToSeller = new EmailDto()
                {
                    To = sellerAccount.Email,
                    Subject = "Product Sale",
                    Body = "<h3>Your product has been successfully sold</h3>" +
                           "<h4 style='text-decoration: underline'>Product Details</h4>" +
                           $"ID: <b>{productToBuy.Id}</b><br />" +
                           $"Name: <b>{productToBuy.Name}</b><br />" +
                           $"Brand: <b>{productToBuy.Brand}</b><br />" +
                           $"Color: <b>{productToBuy.Color}</b><br />" +
                           $"Price: <b>{productToBuy.Price}</b><br />" +
                           $"Description: <b>{productToBuy.Description}</b>" +
                           "<h4 style='text-decoration: underline'>Buyer Details</h4>" +
                           $"Name: <b>{buyerAccount.FirstName} {buyerAccount.LastName}</b><br />" +
                           $"User Name: <b>{buyerAccount.UserName}</b><br />" +
                           $"E-Mail: <b>{buyerAccount.Email}</b><br />" +
                           $"Phone Number: <b>{buyerAccount.PhoneNumber}</b><br />"
                };

                var firstEmailJob = BackgroundJob.Enqueue(() => EmailBackgroundService.SendEmail(emailToBuyer));
                var secondEmailJob = BackgroundJob.ContinueJobWith(firstEmailJob, () => EmailBackgroundService.SendEmail(emailToSeller));

                _logger.LogInformation($"Product purchase for user ({buyerAccount.UserName}) from ({sellerAccount.UserName}) succeeded. Product : {productToBuy.Name} ({productToBuy.Id})");
                return new CoreResponse(SystemMessage.PRODUCT_BUY_SUCCESS + $" (Job ID: ({firstEmailJob}) ({secondEmailJob}))", true);
            }
            else if (state == ApiResponse.ProductIsSold)
            {
                _logger.LogError($"User ({buyerAccount.UserName}) tried to buy a product that has been already sold. Product : {productToBuy.Name} ({productToBuy.Id})");
                return new CoreResponse(SystemMessage.PRODUCT_IS_SOLD, false);
            }
            else
            {
                _logger.LogError($"Product purchase for user ({buyerAccount.UserName}) failed. Product : {productToBuy.Name} ({productToBuy.Id})");
                return new CoreResponse(SystemMessage.PRODUCT_BUY_FAILURE, false);
            }
        }
    }
}
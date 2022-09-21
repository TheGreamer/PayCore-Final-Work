using Hangfire;
using PayCoreFinalWork.Business.Concrete.Services;
using PayCoreFinalWork.Business.Concrete.StaticServices;
using PayCoreFinalWork.Core.DataAccess.Concrete;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Dto.Concrete;
using PayCoreFinalWork.Entity.Concrete.Entities;
using ISession = NHibernate.ISession;

namespace PayCoreFinalWork.DataAccess.Concrete
{
    // Offer sınıfına ait Session nesnesi
    public class OfferSession : CoreSession<Offer>, IOfferSession
    {
        public OfferSession(ISession session) : base(session)
        {
        }

        // Veri erişim katmanında yer alan metodların yapısını bozmadan ve yalnızca servisler tarafından çağırılan bu metod
        // ihtiyaç durumunda override edilerek gerekli kontrol işlemlerini transaction ile beraber yapmaktadır.
        // /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
        // Teklif yapma işleminde yapılan teklifin giriş yapan kullanıcıya ait olduğu ve teklif yapılan kullanıcının teklifin yapılacak olduğu ürünün sahibi olduğu belirlenir.
        // Aynı kullanıcı aynı ürüne tekrar teklif yapamaz. Ancak mevcut teklifini güncelleyebilir.
        // Teklif yapılmak istenen ürün eğer teklif yapılmaya kapalıysa işlem iptal edilir.
        // Teklifin başarıyla yapılma durumunda teklifi yapan ve teklifin yapıldığı kişilere bilgilendirme e-postası gönderilir.
        // Güncellenecek teklif bulunmadıysa işlem iptal edilir.
        public override async Task<ApiResponse> DbOperation(Operation operation, Offer entity, Offer? entityFromBody = null)
        {
            bool state = true;

            try
            {
                BeginTransaction();

                switch (operation)
                {
                    case Operation.Add:
                        entity.AccountId = TokenService.CurrentUserId;

                        var offer = await Get(o => o.AccountId == entity.AccountId && o.ProductId == entity.ProductId);
                        if (offer != null)
                        {
                            state = false;
                            CloseTransaction();
                            return ApiResponse.OfferExists;
                        }

                        var product = await GetEntityHelper<Product>(p => p.Id == entity.ProductId);
                        if (!product.IsOfferable)
                        {
                            state = false;
                            CloseTransaction();
                            return ApiResponse.IsNotOfferable;
                        }

                        entity.Id = Guid.NewGuid();
                        entity.OfferedUserId = product.AccountId;
                        entity.IsAccepted = false;
                        await Add(entity);
                        break;

                    case Operation.Update:
                        if (entityFromBody != null)
                        {
                            var offerAccountCheck = await Get(o => o.ProductId == entityFromBody.ProductId && o.AccountId == TokenService.CurrentUserId);

                            if (offerAccountCheck == null)
                            {
                                CloseTransaction();
                                return ApiResponse.OfferNotExists;
                            }

                            entity.Price = entityFromBody.Price;
                            entity.Comment = entityFromBody.Comment;
                            await Update(entity);
                        }
                        break;

                    case Operation.Delete:
                        await Delete(entity);
                        break;
                }

                await CommitTransaction();
            }
            catch
            {
                state = false;
                await RollbackTransaction();
            }
            finally
            {
                CloseTransaction();

                if (operation == Operation.Add && state)
                {
                    var offerMaker = await GetEntityHelper<Account>(a => a.Id == entity.AccountId);
                    var offerReceiver = await GetEntityHelper<Account>(a => a.Id == entity.OfferedUserId);
                    var offeredProduct = await GetEntityHelper<Product>(p => p.Id == entity.ProductId);

                    var emailToOfferMaker = new EmailDto()
                    {
                        To = offerMaker.Email,
                        Subject = "Product Offer Sent ->",
                        Body = "<h3>Your offer has been sent</h3>" +
                               "<h4 style='text-decoration: underline'>Product Details</h4>" +
                               $"Name: <b>{offeredProduct.Name}</b><br />" +
                               $"Description: <b>{offeredProduct.Description}</b><br />" +
                               $"Color: <b>{offeredProduct.Color}</b><br />" +
                               $"Brand: <b>{offeredProduct.Brand}</b><br />" +
                               $"Price: <b>{offeredProduct.Price}</b><br />" +
                               "<h4 style='text-decoration: underline'>Your Offer's Details</h4>" +
                               $"Price: <b>{entity.Price}</b><br />" +
                               $"Comment: <b>{entity.Comment}</b><br />" +
                               $"Receiver's Name: <b>{offerReceiver.FirstName} {offerReceiver.LastName}</b><br />" +
                               $"Receiver's User Name: <b>{offerReceiver.UserName}</b><br />" +
                               $"Receiver's E-Mail: <b>{offerReceiver.Email}</b><br />" +
                               $"Receiver's Phone Number: <b>{offerReceiver.PhoneNumber}</b>"
                    };

                    var emailToOfferReceiver = new EmailDto()
                    {
                        To = offerReceiver.Email,
                        Subject = "Product Offer Received <-",
                        Body = "<h3>You just got an offer</h3>" +
                               "<h4 style='text-decoration: underline'>Product Details</h4>" +
                               $"Name: <b>{offeredProduct.Name}</b><br />" +
                               $"Description: <b>{offeredProduct.Description}</b><br />" +
                               $"Color: <b>{offeredProduct.Color}</b><br />" +
                               $"Brand: <b>{offeredProduct.Brand}</b><br />" +
                               $"Price: <b>{offeredProduct.Price}</b><br />" +
                               "<h4 style='text-decoration: underline'>Offer's Details</h4>" +
                               $"Price: <b>{entity.Price}</b><br />" +
                               $"Comment: <b>{entity.Comment}</b><br />" +
                               $"Offer Maker's Name: <b>{offerMaker.FirstName} {offerMaker.LastName}</b><br />" +
                               $"Offer Maker's User Name: <b>{offerMaker.UserName}</b><br />" +
                               $"Offer Maker's E-Mail: <b>{offerMaker.Email}</b><br />" +
                               $"Offer Maker's Phone Number: <b>{offerMaker.PhoneNumber}</b>"
                    };

                    var firstMailJob = BackgroundJob.Enqueue(() => EmailBackgroundService.SendEmail(emailToOfferMaker));
                    var secondMailJob = BackgroundJob.ContinueJobWith(firstMailJob, () => EmailBackgroundService.SendEmail(emailToOfferReceiver));
                }
            }

            return await base.DbOperation(operation, entity, entityFromBody);
        }
    }
}
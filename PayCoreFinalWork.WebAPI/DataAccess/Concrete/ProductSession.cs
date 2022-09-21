using PayCoreFinalWork.Business.Concrete.Services;
using PayCoreFinalWork.Core.DataAccess.Concrete;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;
using ISession = NHibernate.ISession;

namespace PayCoreFinalWork.DataAccess.Concrete
{
    // Product sınıfına ait Session nesnesi
    public class ProductSession : CoreSession<Product>, IProductSession
    {
        public ProductSession(ISession session) : base(session)
        {
        }

        // Veri erişim katmanında yer alan metodların yapısını bozmadan ve yalnızca servisler tarafından çağırılan bu metod
        // ihtiyaç durumunda override edilerek gerekli kontrol işlemlerini transaction ile beraber yapmaktadır.
        // /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
        // Ürün ekleme işleminde eklenen ürünün giriş yapan kullanıcıya ait bir ürün olduğu belirlenir ve yeni eklenen bir ürün satılmış olamaz.
        // Ürün silme işleminde önce silinecek ürüne ait teklifler silinir ve sonrasında ürün silinir.
        public override async Task<ApiResponse> DbOperation(Operation operation, Product entity, Product? entityFromBody = null)
        {
            try
            {
                BeginTransaction();

                switch (operation)
                {
                    case Operation.Add:
                        entity.IsSold = false;
                        entity.AccountId = TokenService.CurrentUserId;
                        entity.Id = Guid.NewGuid();
                        await Add(entity);
                        break;

                    case Operation.Update:
                        if (entityFromBody != null)
                        {
                            entity.Name = entityFromBody.Name;
                            entity.Description = entityFromBody.Description;
                            entity.CategoryId = entityFromBody.CategoryId;
                            entity.Color = entityFromBody.Color;
                            entity.Brand = entityFromBody.Brand;
                            entity.Price = entityFromBody.Price;
                            entity.IsOfferable = entityFromBody.IsOfferable;
                            await Update(entity);
                        }
                        break;

                    case Operation.Delete:
                        var offers = await GetEntityListHelper<Offer>(o => o.ProductId == entity.Id);
                        for (int i = 0; i < offers.Count; i++)
                            await DeleteHelper(offers[i]);
                        await Delete(entity);
                        break;
                }

                await CommitTransaction();
            }
            catch
            {
                await RollbackTransaction();
            }
            finally
            {
                CloseTransaction();
            }

            return await base.DbOperation(operation, entity, entityFromBody);
        }
    }
}
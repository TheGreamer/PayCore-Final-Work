using PayCoreFinalWork.Core.DataAccess.Concrete;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;
using ISession = NHibernate.ISession;

namespace PayCoreFinalWork.DataAccess.Concrete
{
    // Category sınıfına ait Session nesnesi
    public class CategorySession : CoreSession<Category>, ICategorySession
    {
        public CategorySession(ISession session) : base(session)
        {
        }

        // Veri erişim katmanında yer alan metodların yapısını bozmadan ve yalnızca servisler tarafından çağırılan bu metod
        // ihtiyaç durumunda override edilerek gerekli kontrol işlemlerini transaction ile beraber yapmaktadır.
        // /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
        // Kategori ekleme işleminde aynı kategori tekrar eklenemez. Ancak mevcut kategori güncellenebilir.
        // Kategori silme işleminde eğer silinmek istenen kategoriye ait eklenmiş ürünler varsa bu işlem iptal edilir.
        public override async Task<ApiResponse> DbOperation(Operation operation, Category entity, Category? entityFromBody = null)
        {
            try
            {
                BeginTransaction();

                switch (operation)
                {
                    case Operation.Add:
                        var category = await Get(c => c.Name == entity.Name);
                        if (category != null)
                        {
                            CloseTransaction();
                            return ApiResponse.CategoryExists;
                        }
                        entity.Id = Guid.NewGuid();
                        await Add(entity);
                        break;

                    case Operation.Update:
                        if (entityFromBody != null)
                        {
                            entity.Name = entityFromBody.Name;
                            entity.Description = entityFromBody.Description;
                            await Update(entity);
                        }
                        break;

                    case Operation.Delete:
                        var productsInCategory = await GetEntityListHelper<Product>(o => o.CategoryId == entity.Id);
                        if (productsInCategory.Count > 0)
                        {
                            CloseTransaction();
                            return ApiResponse.HasItems;
                        }
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
using PayCoreFinalWork.Core.DataAccess.Concrete;
using PayCoreFinalWork.Core.Enums;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;
using PayCoreFinalWork.Business.Concrete.StaticServices;
using ISession = NHibernate.ISession;

namespace PayCoreFinalWork.DataAccess.Concrete
{
    // Account sınıfına ait Session nesnesi
    public class AccountSession : CoreSession<Account>, IAccountSession
    {
        public AccountSession(ISession session) : base(session)
        {
        }

        // Veri erişim katmanında yer alan metodların yapısını bozmadan ve yalnızca servisler tarafından çağırılan bu metod
        // ihtiyaç durumunda override edilerek gerekli kontrol işlemlerini transaction ile beraber yapmaktadır.
        // /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
        // Kullanıcı ekleme / Kayıt olma işlemlerinde eğer sisteme kayıtlı bir kullanıcı adı veya e-posta belirlenmişse işlem iptal edilir. Bu bilgiler benzersizdir.
        // Sisteme kayıt olan kullanıcılar için bu işlem kullanılır ve kayıt olan kullanıcının son aktivite tarihi kayıt olduğu an olarak belirlenir.
        // Kayıt olma sırasında kullanıcı rolü "user" olarak belirlenir.
        // Kayıt olan kullanıcıya ait parola veritabanında şifrelenmiş şekilde tutulur.
        // Kullanıcı silme işleminde eğer kullanıcıya ait sistemde yer alan ürün veya teklifler varsa işlem iptal edilir.
        public override async Task<ApiResponse> DbOperation(Operation operation, Account entity, Account? entityFromBody = null)
        {
            try
            {
                BeginTransaction();

                switch (operation)
                {
                    case Operation.Add:
                        var userNameCheck = await Get(a => a.UserName == entity.UserName);
                        if (userNameCheck != null)
                        {
                            CloseTransaction();
                            return ApiResponse.UserNameExists;
                        }

                        var emailCheck = await Get(a => a.Email == entity.Email);
                        if (emailCheck != null)
                        {
                            CloseTransaction();
                            return ApiResponse.EmailExists;
                        }

                        entity.Id = Guid.NewGuid();
                        entity.LastActivity = DateTime.UtcNow;
                        entity.Role = Roles.User;
                        entity.Password = EncryptionService.HashPassword(entity.Password);
                        await Add(entity);
                        break;

                    case Operation.Update:
                        if (entityFromBody != null)
                        {
                            entity.FirstName = entityFromBody.FirstName;
                            entity.LastName = entityFromBody.LastName;
                            entity.UserName = entityFromBody.UserName;
                            entity.Email = entityFromBody.Email;
                            entity.Password = EncryptionService.HashPassword(entityFromBody.Password);
                            entity.PhoneNumber = entityFromBody.PhoneNumber;
                            entity.Age = entityFromBody.Age;
                            entity.Role = entityFromBody.Role.ToLower();
                            await Update(entity);
                        }
                        break;

                    case Operation.Delete:
                        var productsInAccount = await GetEntityListHelper<Product>(p => p.AccountId == entity.Id);
                        var offersInAccount = await GetEntityListHelper<Offer>(o => o.AccountId == entity.Id);
                        if (productsInAccount.Count > 0 || offersInAccount.Count > 0)
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
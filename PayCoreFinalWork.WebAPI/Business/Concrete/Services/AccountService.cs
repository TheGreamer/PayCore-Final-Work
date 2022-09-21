using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Core.Business.Concrete;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Business.Concrete.Services
{
    // Account sınıfına ait servis sınıfı.
    // CoreService'den kalıtım alınır. Generic operatörü değer olarak Entity ve Session nesnesi bekler.
    // Ayriyeten eğer sadece bu sınıfta yer alacak Account servisleri yer alacaksa IAccountService implemente edilir.
    public class AccountService : CoreService<Account, IAccountSession>, IAccountService
    {
        public AccountService(IAccountSession accountSession) : base(accountSession)
        {
        }
    }
}
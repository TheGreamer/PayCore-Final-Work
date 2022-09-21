using PayCoreFinalWork.Core.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.DataAccess.Abstract
{
    // Account sınıfına ait session interface'i.
    // ICoreSession'dan gerekli ortak özelliklerin kalıtımı alınır.
    // İhtiyaç halinde yalnızca bu session'a özel olacak metodlar burada belirlenir.
    public interface IAccountSession : ICoreSession<Account>
    {
    }
}
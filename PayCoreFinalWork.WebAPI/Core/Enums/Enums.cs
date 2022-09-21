using System.ComponentModel;

namespace PayCoreFinalWork.Core.Enums
{
    // Takip işleminde ne tür bir veritabanı işleminin takip edileceğini belirlemek için kullanılacak olan enum.
    public enum Operation
    {
        Add,
        Update,
        Delete
    }

    // Sistem mesajlarının kullanıcıya daha doğru gösterilebilmesi adına kullanılacak olan enum.
    public enum ApiResponse
    {
        None,
        Added,
        Updated,
        Deleted,
        CategoryExists,
        UserNameExists,
        EmailExists,
        OfferExists,
        OfferNotExists,
        IsNotOfferable,
        OfferAccepted,
        OfferDenied,
        OfferAnswerFailure,
        HasItems,
        ProductBuySuccess,
        ProductBuyFailure,
        ProductIsSold
    }

    // Kullanıcı rolüne ait enum
    public enum Role
    {
        [Description(Roles.User)]
        User,

        [Description(Roles.Admin)]
        Admin,
    }

    // Rollere ait sabit değerleri barındıran sınıf.
    public class Roles
    {
        public const string User = "user";
        public const string Admin = "admin";
    }
}
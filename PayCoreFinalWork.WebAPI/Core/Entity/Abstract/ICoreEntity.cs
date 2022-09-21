namespace PayCoreFinalWork.Core.Entity.Abstract
{
    // CoreEntity sınıfı bu interface'i implemente eder.
    // Generic operatörü ile belirlenen tip aracılığıyla tüm sınıflar belirlenen tipe ait bir Id özelliğine sahip olur.
    // Generic operatörünün içerisinde belirlenecek tip yalnızca IFormattable özelliğine sahip bir tip olmalıdır. (int, long, Guid, ...)
    public interface ICoreEntity<TIdType> where TIdType : IFormattable
    {
        TIdType Id { get; set; } // İmplemente edildiği zaman generic operatörü aracılığıyla belirlenmiş tip bu özelliğe yansır.
    }
}
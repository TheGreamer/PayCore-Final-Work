using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Entity.Concrete.Mappings
{
    // Offer sınıfının, veritabanında yer alan "offers" tablosuyla olan eşleşme konfigürasyonunun yapıldığı sınıf.
    // Konfigürasyonun sağlanabilmesi için NHibernate kütüphanesine ait ClassMapping sınıfından kalıtım alınır.
    // Generic operatürünün içinde ise eşleştirme için kullanılacak sınıfın adı yazılır.
    public class OfferMap : ClassMapping<Offer>
    {
        public OfferMap()
        {
            // Hangi tabloya ait olduğu belirlenir.
            Table("offers");

            // Primary key özelliği taşıyan kolon ve o kolona ait veri tipi belirlenir.
            Id(p => p.Id, i =>
            {
                i.Column("id");
                i.Type(NHibernateUtil.Guid);
            });

            // Hesap numarası alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.AccountId, p =>
            {
                p.Column("accountid");
                p.Type(NHibernateUtil.Guid);
                p.NotNullable(true);
            });

            // Ürün numarası alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.ProductId, p =>
            {
                p.Column("productid");
                p.Type(NHibernateUtil.Guid);
                p.NotNullable(true);
            });

            // Teklifin yapılacağı ürün sahibinin numarasının alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.OfferedUserId, p =>
            {
                p.Column("offereduserid");
                p.Type(NHibernateUtil.Guid);
                p.NotNullable(true);
            });

            // Fiyat alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Price, p =>
            {
                p.Column("price");
                p.Type(NHibernateUtil.Double);
                p.NotNullable(true);
            });

            // Yorum alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Comment, p =>
            {
                p.Column("comment");
                p.Type(NHibernateUtil.String);
                p.NotNullable(false);
            });

            // Kabul edilme durumu alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.IsAccepted, p =>
            {
                p.Column("isaccepted");
                p.Type(NHibernateUtil.Boolean);
                p.NotNullable(true);
            });
        }
    }
}
using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Entity.Concrete.Mappings
{
    // Product sınıfının, veritabanında yer alan "products" tablosuyla olan eşleşme konfigürasyonunun yapıldığı sınıf.
    // Konfigürasyonun sağlanabilmesi için NHibernate kütüphanesine ait ClassMapping sınıfından kalıtım alınır.
    // Generic operatürünün içinde ise eşleştirme için kullanılacak sınıfın adı yazılır.
    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            // Hangi tabloya ait olduğu belirlenir.
            Table("products");

            // Primary key özelliği taşıyan kolon ve o kolona ait veri tipi belirlenir.
            Id(p => p.Id, i =>
            {
                i.Column("id");
                i.Type(NHibernateUtil.Guid);
            });

            // Ad alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Name, p =>
            {
                p.Column("name");
                p.Type(NHibernateUtil.String);
                p.Length(100);
                p.NotNullable(true);
            });

            // Açıklama alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Description, p =>
            {
                p.Column("description");
                p.Type(NHibernateUtil.String);
                p.Length(500);
                p.NotNullable(true);
            });

            // Kategori numarası alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.CategoryId, p =>
            {
                p.Column("categoryid");
                p.Type(NHibernateUtil.Guid);
                p.NotNullable(true);
            });

            // Hesap numarası alanına alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.AccountId, p =>
            {
                p.Column("accountid");
                p.Type(NHibernateUtil.Guid);
                p.NotNullable(true);
            });

            // Renk alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Color, p =>
            {
                p.Column("color");
                p.Type(NHibernateUtil.String);
                p.NotNullable(true);
            });

            // Marka alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Brand, p =>
            {
                p.Column("brand");
                p.Type(NHibernateUtil.String);
                p.NotNullable(true);
            });

            // Fiyat alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Price, p =>
            {
                p.Column("price");
                p.Type(NHibernateUtil.Double);
                p.NotNullable(true);
            });

            // Ürünün satılma durumu alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.IsSold, p =>
            {
                p.Column("issold");
                p.Type(NHibernateUtil.Boolean);
                p.NotNullable(true);
            });

            // Ürünün teklif verilebilir olma durumu alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.IsOfferable, p =>
            {
                p.Column("isofferable");
                p.Type(NHibernateUtil.Boolean);
                p.NotNullable(true);
            });
        }
    }
}
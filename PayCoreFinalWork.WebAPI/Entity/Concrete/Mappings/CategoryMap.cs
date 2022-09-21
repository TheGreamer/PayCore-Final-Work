using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Entity.Concrete.Mappings
{
    // Category sınıfının, veritabanında yer alan "categories" tablosuyla olan eşleşme konfigürasyonunun yapıldığı sınıf.
    // Konfigürasyonun sağlanabilmesi için NHibernate kütüphanesine ait ClassMapping sınıfından kalıtım alınır.
    // Generic operatürünün içinde ise eşleştirme için kullanılacak sınıfın adı yazılır.
    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            // Hangi tabloya ait olduğu belirlenir.
            Table("categories");

            // Primary key özelliği taşıyan kolon ve o kolona ait veri tipi belirlenir.
            Id(c => c.Id, i =>
            {
                i.Column("id");
                i.Type(NHibernateUtil.Guid);
            });

            // Ad alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(c => c.Name, c =>
            {
                c.Column("name");
                c.Type(NHibernateUtil.String);
                c.Length(50);
                c.NotNullable(true);
            });

            // Açıklama alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(c => c.Description, c =>
            {
                c.Column("description");
                c.Type(NHibernateUtil.String);
                c.Length(1000);
                c.NotNullable(false);
            });
        }
    }
}
using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using PayCoreFinalWork.Entity.Concrete.Entities;

namespace PayCoreFinalWork.Entity.Concrete.Mappings
{
    // Account sınıfının, veritabanında yer alan "accounts" tablosuyla olan eşleşme konfigürasyonunun yapıldığı sınıf.
    // Konfigürasyonun sağlanabilmesi için NHibernate kütüphanesine ait ClassMapping sınıfından kalıtım alınır.
    // Generic operatürünün içinde ise eşleştirme için kullanılacak sınıfın adı yazılır.
    public class AccountMap : ClassMapping<Account>
    {
        public AccountMap()
        {
            // Hangi tabloya ait olduğu belirlenir.
            Table("accounts");

            // Primary key özelliği taşıyan kolon ve o kolona ait veri tipi belirlenir.
            Id(p => p.Id, i =>
            {
                i.Column("id");
                i.Type(NHibernateUtil.Guid);
            });

            // Ad alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.FirstName, p =>
            {
                p.Column("firstname");
                p.Type(NHibernateUtil.String);
                p.Length(40);
                p.NotNullable(true);
            });

            // Soyad alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.LastName, p =>
            {
                p.Column("lastname");
                p.Type(NHibernateUtil.String);
                p.Length(40);
                p.NotNullable(true);
            });

            // Kullanıcı adı alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.UserName, p =>
            {
                p.Column("username");
                p.Type(NHibernateUtil.String);
                p.Length(25);
                p.NotNullable(true);
            });

            // E-posta alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Email, p =>
            {
                p.Column("email");
                p.Type(NHibernateUtil.String);
                p.NotNullable(true);
            });

            // Şifre alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Password, p =>
            {
                p.Column("password");
                p.Type(NHibernateUtil.String);
                p.Length(500);
                p.NotNullable(true);
            });

            // Telefon numarası alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.PhoneNumber, p =>
            {
                p.Column("phonenumber");
                p.Type(NHibernateUtil.String);
                p.Length(20);
                p.NotNullable(false);
            });

            // Yaş alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Age, p =>
            {
                p.Column("age");
                p.Type(NHibernateUtil.Int32);
                p.NotNullable(false);
            });

            // Son aktivite alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.LastActivity, p =>
            {
                p.Column("lastactivity");
                p.Type(NHibernateUtil.DateTime);
                p.NotNullable(false);
            });

            // Rol alanına ait veritabanındaki özelliklerin eşleştirmesi yapılır.
            Property(p => p.Role, p =>
            {
                p.Column("role");
                p.Type(NHibernateUtil.String);
                p.NotNullable(false);
            });
        }
    }
}
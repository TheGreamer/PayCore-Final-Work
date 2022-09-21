using PayCoreFinalWork.Core.Entity.Abstract;

namespace PayCoreFinalWork.Core.Entity.Concrete
{
    // Veritabanındaki tabloları temsil edecek olan sınıflar buradan kalıtım alır.
    // Tüm diğer sınıflarda yer alacak ortak özelliklerin bulunabileceği sınıftır.
    // Generic olan ICoreEntity interface'ini implemente ederek Id kolonuna generic operatör aracılığıyla tip belirlenir.
    public class CoreEntity : ICoreEntity<Guid>
    {
        public virtual Guid Id { get; set; } // İmplementasyon yoluyla elde edilen Id alanı (Guid - Benzersiz)
    }
}
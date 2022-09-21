namespace PayCoreFinalWork.Dto.Concrete
{
    // E-Posta bilgisi.
    public class EmailDto
    {
        public string To { get; set; } = string.Empty; // Gönderilecek e-posta adresi.
        public string Subject { get; set; } = string.Empty; // E-Posta konu başlığı.
        public string Body { get; set; } = string.Empty; // E-Posta mesajı.
        public int TryCount { get; set; } = 5; // E-Posta gönderme denemesi sayısı. Varsayılan 5'tir ve e-mail gönderilecek yerlerde değiştirilebilir.
    }
}
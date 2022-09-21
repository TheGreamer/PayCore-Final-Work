using System.Text.Json;
using System.Text.Json.Serialization;

namespace PayCoreFinalWork.Core.WebAPI.Concrete.Utilities
{
    // Bu sınıf action metodların çalıştırılması anında gözüken DateTime tipindeki Json'un format biçimini değiştirmeye yarar.
    // Startup.cs içerisinde konfigürasyonu yapılmıştır.
    // Kalıtım alınan generic bir yapıya sahip olan JsonConverter sınıfının mevcutta yer alan soyut metodları biçimlendirilerek format ayarları uygulanmıştır.
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToLocalTime().ToString("yyyy-MM-dd (HH:mm:ss)")); // Tarih tipinin JSON çıktılarındaki format biçimi : 2022-09-18 (17:30:45)
        }
    }
}
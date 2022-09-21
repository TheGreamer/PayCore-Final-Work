namespace PayCoreFinalWork.Core.WebAPI.Concrete.Responses
{
    // Bir veri dizini olmadan kullanılacak olan ortak dönüş tipi.
    public class CoreResponse
    {
        public string Message { get; private set; } // Mesaj.
        public bool Success { get; private set; } // Başarı durumu.

        public CoreResponse(string message, bool success)
        {
            Message = message;
            Success = success;

            if (!string.IsNullOrWhiteSpace(message)) Message = message;
        }
    }

    // Bir veri diziniyle beraber kullanılacak olan ortak dönüş tipi.
    public class CoreResponse<TResponseType>
    {
        public string Message { get; private set; } // Mesaj.
        public bool Success { get; private set; } // Başarı durumu.
        public TResponseType Response { get; private set; } // Veri dizini.

        public CoreResponse(TResponseType resource, string message = "Success", bool success = true)
        {
            Success = success;
            Message = message;
            Response = resource;
        }

        public CoreResponse(string message)
        {
            Success = false;
            Response = default;

            if (!string.IsNullOrWhiteSpace(message)) Message = message;
        }
    }
}
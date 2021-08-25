using Newtonsoft.Json;

namespace KiteLibrary.Models
{
    public class LoginResponse : BaseMessage
    {
        [JsonProperty("data")]
        public LoginData Data { get; set; }
    }
}

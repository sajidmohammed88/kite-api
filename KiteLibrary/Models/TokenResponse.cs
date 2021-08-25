using Newtonsoft.Json;

namespace KiteLibrary.Models
{
    public class TokenResponse : BaseMessage
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}

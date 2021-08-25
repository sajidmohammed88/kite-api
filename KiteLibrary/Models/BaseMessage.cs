using Newtonsoft.Json;

namespace KiteLibrary.Models
{
    public class BaseMessage
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

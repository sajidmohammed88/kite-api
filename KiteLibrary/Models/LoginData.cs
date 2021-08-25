using Newtonsoft.Json;

namespace KiteLibrary.Models
{
    public class LoginData
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("twofa_type")]
        public string TwofaType { get; set; }

        [JsonProperty("twofa_status")]
        public string TwofaStatus { get; set; }
    }
}

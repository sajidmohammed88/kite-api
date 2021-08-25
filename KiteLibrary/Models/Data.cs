using System.Collections.Generic;
using Newtonsoft.Json;

namespace KiteLibrary.Models
{
    public class Data
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("user_shortname")]
        public string UserShortname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("user_type")]
        public string UserType { get; set; }

        [JsonProperty("broker")]
        public string Broker { get; set; }

        [JsonProperty("exchanges")]
        public List<string> Exchanges { get; set; }

        [JsonProperty("products")]
        public List<string> Products { get; set; }

        [JsonProperty("order_types")]
        public List<string> OrderTypes { get; set; }

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("public_token")]
        public string PublicToken { get; set; }

        [JsonProperty("refresh_token")]
        public object RefreshToken { get; set; }

        [JsonProperty("login_time")]
        public string LoginTime { get; set; }

        [JsonProperty("avatar_url")]
        public object AvatarUrl { get; set; }
    }
}
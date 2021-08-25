namespace KiteLibrary.Models
{
    public class TokenRequest
    {
        public string UserId { get; set; }
        
        public string Password { get; set; }
        
        public string Pin { get; set; }
        
        public string ApiKey { get; set; }

        public string ApiSecret { get; set; }
    }
}

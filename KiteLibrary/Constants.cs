namespace KiteLibrary
{
    internal class Constants
    {
        internal const string BaseUrl = "https://kite.zerodha.com";
        internal const string ConnectUrl = "connect/login?v=3&api_key={0}";
        internal const string LoginUrl = "api/login"; 
        internal const string TwoFaUrl = "api/twofa";
        internal const string FinishUrl = "connect/finish?api_key={0}&sess_id={1}";
        internal const string TokenUrl = "https://api.kite.trade/session/token";
    }
}

using KiteLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KiteLibrary
{
    public class TokenGenerator
    {
        public async Task<TokenResponse> LoginAndGetAccessTokenAsync(TokenRequest tokenRequest)
        {
            if (tokenRequest == null)
            {
                return null;
            }

            using (var httpClient = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                string sessionId = await GetSessionId(httpClient, tokenRequest.ApiKey).ConfigureAwait(false);
                string requestId = await GetRequestId(httpClient, tokenRequest).ConfigureAwait(false);
                string requestToken = await GetRequestToken(httpClient, tokenRequest, requestId, sessionId).ConfigureAwait(false);
                return await GetToken(tokenRequest, requestToken, httpClient).ConfigureAwait(false);
            }
        }

        private static async Task<string> GetSessionId(HttpClient httpClient, string apiKey)
        {
            HttpResponseMessage connectResult = await httpClient.GetAsync(string.Format(Constants.ConnectUrl, apiKey)).ConfigureAwait(false);
            await CheckAndThrowException(connectResult).ConfigureAwait(false);

            return GetQueryValueByName(connectResult.RequestMessage.RequestUri.Query, "sess_id");
        }

        private static async Task<string> GetRequestId(HttpClient httpClient, TokenRequest tokenRequest)
        {
            HttpResponseMessage loginResult = await httpClient.PostAsync(Constants.LoginUrl,
                new StringContent($"user_id={tokenRequest.UserId}&password={tokenRequest.Password}", Encoding.UTF8, "application/x-www-form-urlencoded")
            ).ConfigureAwait(false);
            await CheckAndThrowException(loginResult).ConfigureAwait(false);

            return (JsonConvert.DeserializeObject<LoginResponse>(await loginResult.Content.ReadAsStringAsync().ConfigureAwait(false)))?.Data.RequestId;
        }

        private static async Task<string> GetRequestToken(HttpClient httpClient, TokenRequest tokenRequest, string requestId, string sessionId)
        {
            HttpResponseMessage twoFaResult = await httpClient.PostAsync(Constants.TwoFaUrl,
                new StringContent($"user_id={tokenRequest.UserId}&request_id={requestId}&twofa_value={tokenRequest.Pin}&skip_session=true",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded")
            ).ConfigureAwait(false);
            await CheckAndThrowException(twoFaResult).ConfigureAwait(false);

            HttpResponseMessage finishResult = await httpClient.GetAsync(string.Format(Constants.FinishUrl, tokenRequest.ApiKey, sessionId)).ConfigureAwait(false);

            return GetQueryValueByName(finishResult.RequestMessage.RequestUri.Query, "request_token");
        }

        private static async Task<TokenResponse> GetToken(TokenRequest tokenRequest, string requestToken, HttpClient httpClient)
        {
            string checksum;
            using (var sha256 = SHA256.Create())
            {
                checksum = BitConverter
                    .ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(tokenRequest.ApiKey, requestToken, tokenRequest.ApiSecret))))
                    .Replace("-", "")
                    .ToLowerInvariant();
            }

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Kite-Version", "3");
            HttpResponseMessage tokenResult = await httpClient.PostAsync(Constants.TokenUrl,
                new StringContent($"api_key={tokenRequest.ApiKey}&request_token={requestToken}&checksum={checksum}", Encoding.UTF8, "application/x-www-form-urlencoded")
                ).ConfigureAwait(false);

            await CheckAndThrowException(tokenResult).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<TokenResponse>(await tokenResult.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        private static string GetQueryValueByName(string query, string key) => HttpUtility.ParseQueryString(query).Get(key);

        private static async Task CheckAndThrowException(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                BaseMessage baseMessage = JsonConvert.DeserializeObject<BaseMessage>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
                throw new HttpRequestException($"Error occurred when calling {responseMessage.RequestMessage.RequestUri} => " +
                                               $"status : {baseMessage?.Status}, message : {baseMessage?.Message}");
            }
        }
    }
}

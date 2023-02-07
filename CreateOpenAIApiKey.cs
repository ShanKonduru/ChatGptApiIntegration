using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace OpenAI_ChatGPT_Integration
{
    public class CreateOpenAIApiKey
    {
        private readonly HttpClient _client;
        private readonly string _authenticationToken;
        private readonly string _apiEndpoint = "https://beta.openai.com/docs/api-reference/authentication/create-api-key";

        public CreateOpenAIApiKey(string authenticationToken)
        {
            _client = new HttpClient();
            _authenticationToken = authenticationToken;
        }

        public string CreateAPIKey(string name, string scope, DateTime expiresAt)
        {
            var requestBody = new
            {
                name = name,
                scope = scope,
                expires_at = expiresAt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            };

            var requestJson = JsonConvert.SerializeObject(requestBody);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_apiEndpoint),
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json"),
            };
            request.Headers.Add("Authorization", $"Bearer {_authenticationToken}");

            var response = _client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseJson = response.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseJson);
                return responseObject.key;
            }
            else
            {
                throw new Exception("API request failed");
            }
        }
    }
}

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demos.Azure.CognitiveServices
{
    public abstract class BaseCognitiveServicesApiHandler
    {
        protected readonly string Region;
        protected string ApiEndPoint;
        private readonly HttpClient _httpClient;

        protected BaseCognitiveServicesApiHandler(
            string region,
            string apiKey)
        {
            Region = region;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{ apiKey }");

        }

        public async Task<JToken> AnalyzeUri(Uri imageUri)
        {
            var content = new { url = imageUri.AbsoluteUri };
            var serializedContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(ApiEndPoint, serializedContent);
            Task<string> stringResponse = response.Content.ReadAsStringAsync();

            return JToken.Parse(stringResponse.Result);
        }
    }
}

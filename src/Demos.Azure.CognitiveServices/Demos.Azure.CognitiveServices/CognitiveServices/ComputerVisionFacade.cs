using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Demos.Azure.CognitiveServices
{
    public class ComputerVisionFacade
    {
        private const string REGION = "westeurope";
        private const string API_KEY = "";
        private string _region;
        private string _apiKey;
        private HttpClient _httpClient;
        private string _computerVisionUrl;

        public ComputerVisionFacade() : this(REGION, API_KEY)
        {
        }

        public ComputerVisionFacade(string region, string apiKey)
        {
            _region = region;
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{ _apiKey }");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["visualFeatures"] = "Description, Tags, Color, Faces, ImageType";
            queryString["language"] = "en";
            _computerVisionUrl = $"https://{ _region }.api.cognitive.microsoft.com/vision/v1.0/analyze?{ queryString }";
        }

        public async Task<JToken> Analyze(Uri imageUri)
        {
            var content = new { url = imageUri.AbsoluteUri };
            var serializedContent = new StringContent(JsonConvert.SerializeObject(content),Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_computerVisionUrl, serializedContent);
            Task<string> stringResponse = response.Content.ReadAsStringAsync();

            return JToken.Parse(stringResponse.Result);
        }
    }
}

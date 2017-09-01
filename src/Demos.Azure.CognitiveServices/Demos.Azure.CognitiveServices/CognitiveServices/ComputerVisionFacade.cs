using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public ComputerVisionFacade() : this(REGION, API_KEY)
        {
        }

        public ComputerVisionFacade(string region, string apiKey)
        {
            _region = region;
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{ _apiKey }");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<JToken> Analyze(Uri imageUri)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["visualFeatures"] = "Description, Tags, Color, Faces, ImageType";
            queryString["language"] = "en";
            var uri = $"https://{ _region }.api.cognitive.microsoft.com/vision/v1.0/analyze?" + queryString;

            var content = new { url = imageUri.AbsoluteUri };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(content),Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(uri, jsonContent);
            Task<string> stringResponse = response.Content.ReadAsStringAsync();

            return JToken.Parse(stringResponse.Result);
        }
    }
}

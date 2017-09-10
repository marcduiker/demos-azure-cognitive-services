using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demos.Azure.CognitiveServices.Facades
{
    public class ComputerVisionFacade
    {
        private const string RegionPrefix = "westeurope";
        private const string ApiKey = "";
        private const string BaseCognitiveServicesVisionUrlV1 = ".api.cognitive.microsoft.com/vision/v1.0/";
        private readonly string _region;
        private readonly HttpClient _httpClient;
        private string _computerVisionUrl;

        public ComputerVisionFacade() : this(RegionPrefix, ApiKey)
        {
        }

        public ComputerVisionFacade(string region, string apiKey)
        {
            _region = region;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{ apiKey }");
        }

        public async Task<JToken> ImageAnalysis(Uri imageUri)
        {
            SetUrlForAnalyze();
            
            return await AnalyzeUri(imageUri);
        }
        public async Task<JToken> OcrAnalysis(Uri imageUri)
        {
            SetUrlForOcr();

            return await AnalyzeUri(imageUri);
        }

        private async Task<JToken> AnalyzeUri(Uri imageUri)
        {
            var content = new { url = imageUri.AbsoluteUri };
            var serializedContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_computerVisionUrl, serializedContent);
            Task<string> stringResponse = response.Content.ReadAsStringAsync();

            return JToken.Parse(stringResponse.Result);
        }

        private void SetUrlForAnalyze()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["visualFeatures"] = "Description, Tags, Color, Faces, ImageType";
            queryString["language"] = "en";
            _computerVisionUrl = $"https://{ _region }{ BaseCognitiveServicesVisionUrlV1 }analyze?{ queryString }";
        }

        private void SetUrlForOcr()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["language"] = "en";
            _computerVisionUrl = $"https://{ _region }{ BaseCognitiveServicesVisionUrlV1 }ocr?{ queryString }";
        }
    }
}

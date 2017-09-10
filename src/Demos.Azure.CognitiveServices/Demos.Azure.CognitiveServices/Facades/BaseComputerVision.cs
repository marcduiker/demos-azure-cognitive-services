using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demos.Azure.CognitiveServices.Facades
{
    
    public abstract class BaseComputerVision
    {
        private const string RegionPrefix = "westeurope";
        private const string ApiKey = "";
        protected const string BaseCognitiveServicesVisionUrlV1 = ".api.cognitive.microsoft.com/vision/v1.0/";
        protected readonly string Region;
        protected readonly HttpClient HttpClient;
        protected string ComputerVisionUrl;

        protected BaseComputerVision() : this(RegionPrefix, ApiKey)
        {
        }

        protected BaseComputerVision(string region, string apiKey)
        {
            Region = region;
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{ apiKey }");
        }

        public async Task<JToken> AnalyzeUri(Uri imageUri)
        {
            var content = new { url = imageUri.AbsoluteUri };
            var serializedContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PostAsync(ComputerVisionUrl, serializedContent);
            Task<string> stringResponse = response.Content.ReadAsStringAsync();

            return JToken.Parse(stringResponse.Result);
        }
    }
}

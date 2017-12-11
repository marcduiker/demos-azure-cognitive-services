using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Demos.Azure.CognitiveServices.HelloCV
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Analyze an image using the Azure Computer Vision API:");
            Console.Write("Enter the path to an image you wish to analyze: ");
            string imageFilePath = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(imageFilePath) && File.Exists(imageFilePath))
            {
                Analyze(imageFilePath).Wait();
            }

            Console.WriteLine("Press R to retry or Enter to exit.");
            var keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.R)
            {
                Console.Clear();
                Main();
            }
        }


        private static async Task Analyze(string imageFilePath)
        {
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "__COMPUTER_VISION_API_KEY__");

                string uriBase = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/analyze";
                string requestParameters = "visualFeatures=Description,Tags,Color,Faces,ImageType&language=en";
                string analyzeUri = $"{uriBase}?{requestParameters}";

                HttpResponseMessage response = await httpClient.PostAsync(analyzeUri, content);
                string analysisResult = await response.Content.ReadAsStringAsync();

                Console.WriteLine("\nResponse:\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(JToken.Parse(analysisResult));
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            var fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            var binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
}

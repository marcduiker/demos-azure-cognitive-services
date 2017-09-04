using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demos.Azure.CognitiveServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string website = args.Length > 0 ? args[0] : string.Empty;
            Run(website);
        }

        private static void Run(string website)
        {
            if (string.IsNullOrWhiteSpace(website))
            {
                WriteInfo("Enter a valid website url:");
                website = Console.ReadLine();
            }

            if (WebsiteUrlParser.TryParse(website, out Uri websiteUri))
            {
                var websiteScraper = new WebsiteScraper(websiteUri);
                var imageUriValues = websiteScraper.GetImageUrlsFromWebsite();
                if (imageUriValues.Any())
                {
                    var imageResultCollection = AnalyzeImages(imageUriValues).Result;
                    HtmlFileWriter.WriteMultiple(imageResultCollection, websiteUri);
                }
                else
                {
                    WriteError("No valid image sources found.");
                }
            }
            else
            {
                WriteError($"{ website } is not a valid url.");
            }

            WriteInfo("Press R to retry or Enter to exit.");
            var keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.R)
            {
                Console.Clear();
                Run(string.Empty);
            }
        }

        private static async Task<Dictionary<Uri, JToken>> AnalyzeImages(IEnumerable<Uri> imageUriValues)
        {
            var computerVision = new ComputerVisionFacade();
            var imageResultCollection = new Dictionary<Uri, JToken>();

            foreach (var imageUri in imageUriValues)
            {
                WriteResult(imageUri.AbsoluteUri);
                var cvResult = await computerVision.Analyze(imageUri);
                WriteInfo("Computer Vision result:");
                WriteResult(cvResult.ToString(Newtonsoft.Json.Formatting.Indented));
                imageResultCollection.Add(imageUri, cvResult);
                await Task.Delay(250);
            }

            return imageResultCollection;
        }

        private static void WriteInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        private static void WriteResult(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }

        private static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }
    }
}

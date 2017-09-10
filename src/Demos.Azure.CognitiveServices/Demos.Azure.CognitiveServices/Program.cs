using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demos.Azure.CognitiveServices.Facades;

namespace Demos.Azure.CognitiveServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string website = args.Length > 0 ? args[0] : string.Empty;
            int analysisType = args.Length > 1 ? int.Parse(args[1]) : 0;
            Run(website, analysisType);
        }

        private static void Run(string website, int analysisType)
        {
            if (string.IsNullOrWhiteSpace(website))
            {
                WriteInfo("Enter a website url:");
                website = Console.ReadLine();
            }

            if (analysisType == 0)
            {
                WriteInfo("Perform General Image analysis [1], Landmark analysis [2] or OCR [3]?:");
                analysisType = int.Parse(Console.ReadLine());
            }

            if (WebsiteUrlParser.TryParse(website, out Uri websiteUri))
            {
                var websiteScraper = new WebsiteScraper(websiteUri);
                var imageUriValues = websiteScraper.GetImageUrlsFromWebsite().ToList();
                if (imageUriValues.Any())
                {
                    var imageResultCollection = AnalyzeImages(imageUriValues, analysisType).Result;
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
                Run(string.Empty, 0);
            }
        }

        private static async Task<Dictionary<Uri, JToken>> AnalyzeImages(
            IEnumerable<Uri> imageUriValues, 
            int analysisType)
        {
            var imageResultCollection = new Dictionary<Uri, JToken>();
            BaseComputerVision computerVision;
            switch (analysisType)
            {
                case 2:
                    computerVision = new LandmarkAnalysis();
                    break;
                case 3:
                    computerVision = new OcrAnalysis();
                    break;
                default:
                    computerVision = new GeneralAnalysis();
                    break;
            }

            foreach (var imageUri in imageUriValues)
            {
                WriteResult(imageUri.AbsoluteUri);
                var cvResult = await computerVision.AnalyzeUri(imageUri);

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

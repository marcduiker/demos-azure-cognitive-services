using System;
using System.Collections.Generic;
using System.Linq;

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
                Console.WriteLine("Enter a valid website url:");
                website = Console.ReadLine();
            }

            if (WebsiteUrlParser.TryParse(website, out Uri websiteUri))
            {
                var websiteScraper = new WebsiteScraper(websiteUri);
                var imageUriValues = websiteScraper.GetImageUrlsFromWebsite();
                if (imageUriValues.Any())
                {
                    PrintImageSources(imageUriValues);

                    var computerVision = new ComputerVisionFacade();
                    var cvResult = computerVision.Analyze(imageUriValues.First()).Result;

                    Console.WriteLine(cvResult.ToString(Newtonsoft.Json.Formatting.Indented));
                    HtmlFileWriter.WriteSingle(imageUriValues.First(), cvResult);
                }
                else
                {
                    Console.WriteLine("No valid image sources found.");
                }
            }

            Console.WriteLine("Press R to retry or Enter to exit.");
            var keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.R)
            {
                Console.Clear();
                Run(string.Empty);
            }
        }

        private static void PrintImageSources(IEnumerable<Uri> imageUriValues)
        {
            foreach (var imageUri in imageUriValues)
            {
                Console.WriteLine(imageUri);
            }
        }
    }
}

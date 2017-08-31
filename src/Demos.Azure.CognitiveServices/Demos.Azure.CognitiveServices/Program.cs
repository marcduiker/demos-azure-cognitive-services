using System;
using System.Collections.Generic;

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
                PrintImageSources(imageUriValues);
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

using System;

namespace Demos.Azure.CognitiveServices
{
    public class WebsiteUrlParser
    {
        public static bool TryParse(string websiteUrl, out Uri websiteUriResult)
        {
            websiteUriResult = null;
            bool isValidResult = false;

            if (string.IsNullOrWhiteSpace(websiteUrl))
            {
                Console.WriteLine("Please provide a website url as argument.");
            }

            if (!websiteUrl.StartsWith(Uri.UriSchemeHttp))
            {
                websiteUrl = $"{ Uri.UriSchemeHttp }://{ websiteUrl }";
            }

            isValidResult = Uri.TryCreate(websiteUrl, UriKind.Absolute, out websiteUriResult)
                 && (websiteUriResult.Scheme == Uri.UriSchemeHttp || websiteUriResult.Scheme == Uri.UriSchemeHttps);
            if (!isValidResult)
            {
                Console.WriteLine($"{ websiteUrl } is not a valid url.");
            }

            return isValidResult;
        }
    }
}

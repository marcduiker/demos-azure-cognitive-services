using System;

namespace Demos.Azure.CognitiveServices
{
    public class WebsiteUrlParser
    {
        public static bool TryParse(string websiteUrl, out Uri websiteUriResult)
        {
            if (!string.IsNullOrWhiteSpace(websiteUrl) && !websiteUrl.StartsWith(Uri.UriSchemeHttp))
            {
                websiteUrl = $"{ Uri.UriSchemeHttp }://{ websiteUrl }";
            }

            return Uri.TryCreate(websiteUrl, UriKind.Absolute, out websiteUriResult)
                 && (websiteUriResult.Scheme == Uri.UriSchemeHttp || websiteUriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}

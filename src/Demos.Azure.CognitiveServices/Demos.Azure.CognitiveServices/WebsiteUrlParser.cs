using System;

namespace Demos.Azure.CognitiveServices
{
    public class WebsiteUrlParser
    {
        public static bool TryParse(string websiteUrl, out Uri websiteUriResult)
        {
            websiteUriResult = null;
            bool isValidResult = false;

            if (!string.IsNullOrWhiteSpace(websiteUrl) && !websiteUrl.StartsWith(Uri.UriSchemeHttp))
            {
                websiteUrl = $"{ Uri.UriSchemeHttp }://{ websiteUrl }";
            }

            isValidResult = Uri.TryCreate(websiteUrl, UriKind.Absolute, out websiteUriResult)
                 && (websiteUriResult.Scheme == Uri.UriSchemeHttp || websiteUriResult.Scheme == Uri.UriSchemeHttps);

            return isValidResult;
        }
    }
}

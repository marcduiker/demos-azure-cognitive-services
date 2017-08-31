using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Demos.Azure.CognitiveServices
{
    public class WebsiteScraper
    {
        private readonly Uri _website;
        private readonly HtmlDocument _htmlDocument;
        private const string IMG_TAG = @"//img";
        private const string SRC_ATTRIBUTE = @"src";
        private const string DATA_ORIGINAL_ATTRIBUTE = @"data-original";
        private const string IMG_EXTENSION_REGEX = "(.jpg)|(.png)$";
        private const int MAX_IMG_URLS = 10;

        public WebsiteScraper(Uri website)
        {
            _website = website;
            HtmlWeb web = new HtmlWeb();
            _htmlDocument = web.Load(_website);
        }

        public IEnumerable<Uri> GetImageUrlsFromWebsite()
        {
            var imageUrls = new List<Uri>();

            IEnumerable<string> imageSourceValues = GetImageSourcesForAttribute(SRC_ATTRIBUTE);
            IEnumerable<string> imageDataOriginalValues = GetImageSourcesForAttribute(DATA_ORIGINAL_ATTRIBUTE);

            List<string> allImageSourceValues = new List<string>();
            allImageSourceValues.AddRange(imageSourceValues);
            allImageSourceValues.AddRange(imageDataOriginalValues);
            imageUrls = allImageSourceValues.Select(source => CreateUri(source)).Take(MAX_IMG_URLS).ToList();

            return imageUrls;
        }

        private IEnumerable<string> GetImageSourcesForAttribute(string attribute)
        {
            return _htmlDocument.DocumentNode
                .SelectNodes($"{ IMG_TAG }/@{ attribute }")
                ?.Where(img => Regex.IsMatch(img.Attributes[attribute].Value, IMG_EXTENSION_REGEX))
                ?.Select(img => img.Attributes[attribute].Value) ?? new List<string>();
        }

        private Uri CreateUri(string imageSource)
        {
            Uri.TryCreate(imageSource, UriKind.RelativeOrAbsolute, out Uri uriResult);
            if (!uriResult.IsAbsoluteUri)
            {
                Uri.TryCreate(_website, uriResult, out uriResult);
            }
            return uriResult;
        }
    }
}

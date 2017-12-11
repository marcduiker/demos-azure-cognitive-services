using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Demos.Azure.CognitiveServices
{
    public class WebsiteScraper
    {
        private readonly Uri _website;
        private readonly HtmlDocument _htmlDocument;
        private readonly int _maxImagesToScrape;
        private const string ImgTag = @"//img";
        private const string SrcAttribute = @"src";
        private const string DataOriginalAttribute = @"data-original";
        private const string ImgExtensionRegex = "(.jpg)|(.jpeg)|(.png)$";
        private const int MaxImages = 7;

        public WebsiteScraper(Uri website, int maxImagesToScrape = MaxImages)
        {
            _website = website;
            HtmlWeb web = new HtmlWeb();
            _htmlDocument = web.Load(_website);
            _maxImagesToScrape = maxImagesToScrape;
        }

        public IEnumerable<Uri> GetImageUrlsFromWebsite()
        {
            IEnumerable<string> imageSourceValues = GetImageSourcesForAttribute(SrcAttribute);
            IEnumerable<string> imageDataOriginalValues = GetImageSourcesForAttribute(DataOriginalAttribute);

            List<string> allImageSourceValues = new List<string>();
            allImageSourceValues.AddRange(imageSourceValues);
            allImageSourceValues.AddRange(imageDataOriginalValues);

            return  allImageSourceValues.Select(CreateUri).Distinct().Where(uri => IsImageLargerThanMinimum(uri.AbsoluteUri, 250)).Take(_maxImagesToScrape).ToList();
        }

        private IEnumerable<string> GetImageSourcesForAttribute(string attribute)
        {
            return _htmlDocument.DocumentNode
                .SelectNodes($"{ ImgTag }/@{ attribute }")
                ?.Where(img => Regex.IsMatch(img.Attributes[attribute].Value, ImgExtensionRegex))
                .Select(img => img.Attributes[attribute].Value) ?? new List<string>();
        }

        private static bool IsImageLargerThanMinimum(string imageSource, int minimumDimension)
        {
            var client= new WebClient();
            int height;
            using (var imageStream = client.OpenRead(imageSource))
            {
                var image = Image.FromStream(imageStream);
                height = image.Height;
            }
        
            return height >= minimumDimension;
        }

        private Uri CreateUri(string imageSource)
        {
            Uri.TryCreate(imageSource, UriKind.RelativeOrAbsolute, out Uri uriResult);

            // Ensure absolute image url
            if (!uriResult.IsAbsoluteUri)
            {
                Uri.TryCreate(_website, uriResult, out uriResult);
            }

            // Strip any querystrings from image url
            if (uriResult.AbsoluteUri.Contains("?"))
            {
                Uri.TryCreate(uriResult.AbsoluteUri.Split('?')[0], UriKind.RelativeOrAbsolute, out uriResult);
            }

            return uriResult;
        }
    }
}

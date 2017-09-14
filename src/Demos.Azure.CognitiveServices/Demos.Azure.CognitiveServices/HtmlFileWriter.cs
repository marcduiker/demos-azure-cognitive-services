using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Demos.Azure.CognitiveServices
{
    public class HtmlFileWriter
    {
        private const string DefaultFolder = @"C:\temp\cv-output";

        public static void WriteSingle(Uri imageSource, JToken cvResult, string analysisOption, string folder = DefaultFolder)
        {
            var doc = new HtmlDocument();
            var node = HtmlNode.CreateNode(
                StartDocument(analysisOption) +
                GetImageAndCvResult(imageSource, cvResult) +
                EndDocument());
            doc.DocumentNode.AppendChild(node);
            var fileName = $"{ DateTime.Now :yyyyMMdd-HHmmss}-{ Path.GetFileName(imageSource.AbsolutePath) }.html";
            SaveToDisk(doc, fileName, folder);
        }

        public static void WriteMultiple(Dictionary<Uri, JToken> imagesAndCvResults, Uri websiteUri, string analysisOption, string folder = DefaultFolder)
        {
            var doc = new HtmlDocument();
            var htmlOutline = HtmlNode.CreateNode(StartDocument(analysisOption) + EndDocument());
            doc.DocumentNode.AppendChild(htmlOutline);
            HtmlNode parentNode = doc.DocumentNode.SelectSingleNode("//body");
            HtmlNodeCollection nodeCollection = new HtmlNodeCollection(parentNode);
            foreach (var imageAndCvResult in imagesAndCvResults)
            {
                HtmlNode imageNode = HtmlNode.CreateNode(
                    GetImageAndCvResult(
                        imageAndCvResult.Key, 
                        imageAndCvResult.Value)
                    );
                nodeCollection.Add(imageNode);
            }

            doc.DocumentNode.AppendChildren(nodeCollection);
            var fileName = $"{ DateTime.Now :yyyyMMdd-HHmmss}-{ websiteUri.Host }.html";
            SaveToDisk(doc, fileName, folder);
        }

        private static string StartDocument(string title)
        {
            return $"<html><head /><body><h1>Azure Cognitive Services Results: { title } </h1>";
        }

        private static string GetImageAndCvResult(Uri imageSource, JToken cvResult)
        {
            return  $"<div><h2>{ GetCaption(cvResult) }</h2>" +
                    $"<h3>{ imageSource.AbsoluteUri } </h3>" +
                    $"<img src='{ imageSource.AbsoluteUri }'>" +
                    @"<pre>" +
                    $"{ cvResult.ToString(Formatting.Indented) }" +
                    @"</pre></div>";
        }

        private static string EndDocument()
        {
            return "</body></html>";
        }

        private static string GetCaption(JToken cvResult)
        {
            return (string)cvResult.SelectToken("description.captions[0].text");
        }

        private static void SaveToDisk(HtmlDocument doc, string fileName, string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var filePath = Path.Combine(folder, fileName);
            doc.Save(filePath);
        }
    }
}

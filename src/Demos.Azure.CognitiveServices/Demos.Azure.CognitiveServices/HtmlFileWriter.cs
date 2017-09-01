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
        private const string DEFAULT_FOLDER = @"C:\temp\cv-output";

        public static void WriteSingle(Uri imageSource, JToken cvResult, string folder = DEFAULT_FOLDER)
        {
            var caption = (string)cvResult.SelectToken("description.captions[0].text");
            var doc = new HtmlDocument();
            var node = HtmlNode.CreateNode(
            StartDocument() +
        $"<h1>{ caption }</h1>" +
        $"<img src='{ imageSource.AbsoluteUri }'>" + 
        @"<pre>" +
        $"{ cvResult.ToString(Formatting.Indented) }" +
        @"</pre>" +
        EndDocument()
    );
            doc.DocumentNode.AppendChild(node);
            var fileName = $"{ DateTime.Now.ToString("yyyyMMdd-HHmmss") }-{ Path.GetFileName(imageSource.AbsolutePath) }.html";
            SaveToDisk(doc, fileName, folder);
        }

        public static void WriteMultiple(Dictionary<Uri, JToken> imagesAndCvResults, string folder = DEFAULT_FOLDER)
        {
            throw new NotImplementedException();
        }

        private static string StartDocument()
        {
            return "<html><head /><body>";
        }

        private static string EndDocument()
        {
            return "</body></html>";
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

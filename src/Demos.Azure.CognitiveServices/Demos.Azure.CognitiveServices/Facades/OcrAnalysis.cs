using System.Web;

namespace Demos.Azure.CognitiveServices.Facades
{
    public class OcrAnalysis : BaseComputerVision
    {
        public OcrAnalysis()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["language"] = "en";
            ComputerVisionUrl = $"https://{ Region }{ BaseCognitiveServicesVisionUrlV1 }ocr?{ queryString }";
        }
    }
}

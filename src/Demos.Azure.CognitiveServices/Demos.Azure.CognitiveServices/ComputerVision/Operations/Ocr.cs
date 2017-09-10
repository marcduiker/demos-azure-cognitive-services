using System.Web;

namespace Demos.Azure.CognitiveServices.ComputerVision.Operations
{
    public class Ocr : BaseComputerVisionApiHandler
    {
        public Ocr()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["language"] = "en";
            ApiEndPoint = $"https://{ Region }{ BaseCognitiveServicesVisionUrlV1 }ocr?{ queryString }";
        }
    }
}

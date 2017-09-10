using System.Web;

namespace Demos.Azure.CognitiveServices.ComputerVision.Operations
{
    public class Analyze : BaseComputerVisionApiHandler
    {
        public Analyze()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["visualFeatures"] = "Description, Tags, Color, Faces, ImageType";
            queryString["language"] = "en";
            ApiEndPoint = $"https://{ Region }{ BaseCognitiveServicesVisionUrlV1 }analyze?{ queryString }";
        }
    }
}

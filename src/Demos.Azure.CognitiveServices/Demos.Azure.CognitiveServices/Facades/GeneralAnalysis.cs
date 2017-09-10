using System.Web;

namespace Demos.Azure.CognitiveServices.Facades
{
    public class GeneralAnalysis : BaseComputerVision
    {
        public GeneralAnalysis()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["visualFeatures"] = "Description, Tags, Color, Faces, ImageType";
            queryString["language"] = "en";
            ComputerVisionUrl = $"https://{ Region }{ BaseCognitiveServicesVisionUrlV1 }analyze?{ queryString }";
        }
    }
}

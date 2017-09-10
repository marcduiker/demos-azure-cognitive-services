using System.Web;

namespace Demos.Azure.CognitiveServices.Facades
{
    public class LandmarkAnalysis : BaseComputerVision
    {
        public LandmarkAnalysis()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["language"] = "en";
            ComputerVisionUrl = $"https://{ Region }{ BaseCognitiveServicesVisionUrlV1 }/models/landmarks/analyze?{ queryString }";
        }
    }
}

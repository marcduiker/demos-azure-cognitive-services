using System.Web;

namespace Demos.Azure.CognitiveServices.ComputerVision.Operations
{
    public class Landmark : BaseComputerVisionApiHandler
    {
        public Landmark()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["language"] = "en";
            ApiEndPoint = $"https://{ Region }{ BaseCognitiveServicesVisionUrlV1 }/models/landmarks/analyze?{ queryString }";
        }
    }
}

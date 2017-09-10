using System.Web;

namespace Demos.Azure.CognitiveServices.ComputerVision.Operations
{
    public class Describe : BaseComputerVisionApiHandler
    {
        public Describe()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["maxCandidates"] = "3";
            ApiEndPoint = $"https://{ Region }{ BaseCognitiveServicesVisionUrlV1 }describe?{ queryString }";
        }
    }
}

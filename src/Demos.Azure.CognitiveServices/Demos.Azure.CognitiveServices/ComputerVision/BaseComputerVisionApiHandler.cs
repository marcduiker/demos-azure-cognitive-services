namespace Demos.Azure.CognitiveServices.ComputerVision
{
    public abstract class BaseComputerVisionApiHandler : BaseCognitiveServicesApiHandler
    {
        private const string RegionPrefix = "westeurope";
        private const string ApiKey = "__COMPUTER_VISION_API_KEY__";
        protected const string BaseCognitiveServicesVisionUrlV1 = ".api.cognitive.microsoft.com/vision/v1.0/";

        protected string ComputerVisionUrl;

        protected BaseComputerVisionApiHandler() : this(RegionPrefix, ApiKey)
        {
        }

        protected BaseComputerVisionApiHandler(string region, string apiKey) 
            : base(region, apiKey)
        {
        }
    }
}

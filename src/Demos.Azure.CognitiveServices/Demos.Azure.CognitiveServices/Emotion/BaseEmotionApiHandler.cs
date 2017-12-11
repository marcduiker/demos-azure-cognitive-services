namespace Demos.Azure.CognitiveServices.Emotion
{
    public abstract class BaseEmotionApiHandler : BaseCognitiveServicesApiHandler
    {
        private const string RegionPrefix = "westus"; // Only available as preview in westus 
        private const string ApiKey = "__EMOTION_API_KEY__";
        protected const string BaseEmotionUrlV1 = ".api.cognitive.microsoft.com/emotion/v1.0/";
        protected string EmotionUrl;

        protected BaseEmotionApiHandler() : this(RegionPrefix, ApiKey)
        {
        }

        protected BaseEmotionApiHandler(string region, string apiKey) 
            : base(region, apiKey)
        {
        }
    }
}

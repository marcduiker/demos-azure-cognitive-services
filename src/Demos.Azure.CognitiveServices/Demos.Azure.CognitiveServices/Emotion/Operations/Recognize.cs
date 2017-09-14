using System.Web;

namespace Demos.Azure.CognitiveServices.Emotion.Operations
{
    public class Recognize : BaseEmotionApiHandler
    {
        public Recognize()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            ApiEndPoint = $"https://{ Region }{ BaseEmotionUrlV1 }recognize?{ queryString }";
        }
    }
}

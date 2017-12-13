using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace Demos.Azure.CognitiveServices.HelloCV
{
    /// <summary>
    /// The CVServiceClient class shows how to use the VisionServiceClient API that is available
    /// by referencing the MicrosoftProjectOxford.Vision.DotNetCore NuGet package.
    /// </summary>
    public class CVServiceClientExample
    {
        private const string ApiKey = "__COMPUTER_VISION_API_KEY__";

        public async Task<AnalysisResult> Analyze(string imageFilePath)
        {
            var fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            
            var client = new VisionServiceClient(ApiKey);
            var visualFeatures = new List<VisualFeature>
            {
              VisualFeature.Description,
              VisualFeature.Tags,
              VisualFeature.Faces
            };
            
            return await client.AnalyzeImageAsync(fileStream, visualFeatures);
        }
    }
}

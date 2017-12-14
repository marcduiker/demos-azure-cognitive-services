# demos-azure-cognitive-services
Repository with demo code for Azure Cognitive Services

## Cognitive Services http requests

The `test` folder contains two `.http` files and several images which can be used to make calls to the Cognitive Services APIs. The requests can be executed using VS Code with the Rest Client extension.

- `computervision.http` contains requests to Computer Vision and Emotion API's. You need to update the subscription keys with your own.
- `customvision.http` contains some links to images which can be used to test the Ferrari or Aston Martin project (CustomVision service). Since this is not a publicly accessible project this http file is not suitable for re-use.

## Demos.Azure.CognitiveServices.HelloCV console app

This app can be used to upload a local image and analyze it using the Custom Vision API. You need to update the subscription key in the `Ocp-Apim-Subscription-Key` header (`Program.cs`) with your own subscription key.

The CVServiceClientExample.cs is not used by the app, it just shows how to use the `VisionServiceClient` and use strongly typed Computer Vision objects.

## Demos.Azure.CognitiveServices console app

This app can be used to perform various Computer Vision and Emotion analyses on images found in a webpage.
You need to update the subscription keys (and possibly the region) in the `BaseComputerVisionApiHandler.cs` and `BaseEmotionApiHandler.cs`.

__Note that none of this is production code! Never put subscription keys and uris in source code!__ 
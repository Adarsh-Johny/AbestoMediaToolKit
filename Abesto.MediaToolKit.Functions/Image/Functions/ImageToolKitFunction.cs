using Abesto.MediaToolKit.Functions.Cloud;
using Amazon.S3.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Abesto.MediaToolKit.Functions.Image.Functions
{
    public class ImageToolKitFunction(ILoggerFactory loggerFactory, IConfiguration configuration, ICloudManager cloudManager)
        : BaseImageProcessorFunction(configuration, cloudManager)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<ImageToolKitFunction>();
        private readonly string _orchestrationFunctionName = "ImageProcessingOrchestrator";
        private readonly int _maxFilesPerAPI = configuration.GetValue<int>("maxFilesPerAPI");

        //TODO Add Authorization layer
        [Function("process-image")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData request,
             [DurableClient] DurableTaskClient client, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" *** Started process-image Job *** ");

            string requestBody = await new StreamReader(request.Body).ReadToEndAsync(cancellationToken);
            var imageConfig = JsonConvert.DeserializeObject<ImageConfiguration>(requestBody);

            var listObjectsRequest = ImageProcessorFunctionExtensions.CreateAmazonS3Request(_containerName, _maxFilesPerAPI);

            while (true)
            {
                string instanceId = string.Empty;
                var responseList = await GetImagesAsync(listObjectsRequest, cancellationToken);
                var images = responseList?.S3Objects?.FindImages();

                if (images.Any())
                {
                    instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                           _orchestrationFunctionName, images.CreateImageProcessRequest(imageConfig)
                       );
                }

                if (!string.IsNullOrWhiteSpace(responseList?.NextContinuationToken))
                {
                    listObjectsRequest.ContinuationToken = responseList.NextContinuationToken;
                }
                else
                {
                    break;
                }

                _logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);
            }

            var response = request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            _logger.LogInformation(" *** Ended process-image Job *** ");

            return response;
        }

        private async Task<ListObjectsV2Response?> GetImagesAsync(ListObjectsV2Request listObjectsRequest, CancellationToken cancellationToken)
        {
            var imageResponse = await _cloudStorageClient.GetPaginatedResultsAsync<ListObjectsV2Request>(listObjectsRequest, cancellationToken);
            return imageResponse as ListObjectsV2Response;
        }

    }
}

using Abesto.MediaToolKit.Functions.Cloud;
using Abesto.MediaToolKit.Functions.Cloud.Amazon;
using Abesto.MediaToolKit.Functions.Utilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Abesto.MediaToolKit.Functions.ImageProcessor.Functions
{
    public class ProcessImageActivityFunction(ILoggerFactory loggerFactory, IConfiguration configuration, ICloudManager cloudManager)
        : BaseImageProcessorFunction(configuration, cloudManager)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<ProcessImageActivityFunction>();
        private readonly string _basePath = configuration.GetValue<string>("ImageLocalFilePath") ?? Directory.GetCurrentDirectory();

        [Function("ProcessImageActivityFunction")]
        public async Task RunAsync([ActivityTrigger] ImageProcessRequest request, CancellationToken cancellationToken)
        {
            var imageInfo = request.Images.First();

            _logger.LogInformation($" *** Activity Execution Started for {imageInfo.Key} *** ");

            var localFilePath = DirectoryUtilities.GenerateFileLocation(imageInfo.Key, _basePath);

            var imageDownloader = new AmazonS3FileDownLoader(_cloudStorageClient, _logger);
            await imageDownloader.DownloadPAndSaveFilesAsync(imageInfo.BucketName, imageInfo.Key, localFilePath, cancellationToken);

            var outputFilePath = DirectoryUtilities.GenerateFileLocation(imageInfo.Key, _basePath,request.Configuration.FileSuffix);

            await ImageModifier.ModifyAsync(request.Configuration, localFilePath, outputFilePath, _logger);

            _logger.LogInformation($" *** Activity Execution Completed for {imageInfo.Key} *** \n");

        }
    }
}

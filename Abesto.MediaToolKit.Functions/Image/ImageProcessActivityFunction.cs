using Abesto.MediaToolKit.Functions.Amazon;
using Abesto.MediaToolKit.Functions.Cloud;
using Abesto.MediaToolKit.Functions.Utilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Abesto.MediaToolKit.Functions.Image
{
    public class ProcessImageActivityFunction(ILoggerFactory loggerFactory, IConfiguration configuration, ICloudManager cloudManager)
        : BaseImageProcessorFunction(configuration, cloudManager)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<ProcessImageActivityFunction>();
        private readonly string _basePath = configuration.GetValue<string>("ImageLocalFilePath") ?? Environment.CurrentDirectory;

        [Function("ProcessImageActivityFunction")]
        public async Task RunAsync([ActivityTrigger] ImageProcessRequest request, CancellationToken cancellationToken)
        {
            var imageInfo = request.Images.First();

            _logger.LogInformation($" *** Activity Execution Started for {imageInfo.Key} --- ");

            var localFilePath = DirectoryUtilities.GenerateFileLocation(imageInfo.Key, _basePath);

            var imageDownloader = new AmazonS3FileDownLoader(_cloudStorageClient);
            await imageDownloader.DownloadPAndSaveFilesAsync(imageInfo.BucketName, imageInfo.Key, localFilePath, cancellationToken);

            _logger.LogInformation($" *** Activity Execution Completed for {imageInfo.Key} --- ");

        }
    }
}

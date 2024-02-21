using Abesto.MediaToolKit.Functions.Utilities;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Abesto.MediaToolKit.Functions.Cloud.Amazon
{
    public class AmazonS3FileDownLoader(ICloudStorageClient cloudStorageClient, ILogger logger)
    {
        private readonly ICloudStorageClient _cloudStorageClient = cloudStorageClient;
        private readonly ILogger _logger = logger;

        private async Task DownloadAndSaveImageAsync(string containerName, string key, string localFilePath, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started downloading file - {key}");
            var response = await _cloudStorageClient.GetAsync(containerName, key, cancellationToken);

            var imageResponse = (GetObjectResponse)response;

            if (imageResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                using Stream responseStream = imageResponse.ResponseStream;

                //Re generate new file path if there are duplicate files
                localFilePath = DirectoryUtilities.GenerateFilePath(localFilePath);

                //TODO Check file is actual image or not before saving
                using FileStream fileStream = File.Create(localFilePath);
                await responseStream.CopyToAsync(fileStream, cancellationToken);

                _logger.LogInformation($"Completed downloading and saving the file - {key}");
            }
            else
            {
                _logger.LogError($"Downloading Image failed - {key}");
            }
        }


        public async Task DownloadPAndSaveFilesAsync(string containerName, string key, string localFolderPath, CancellationToken cancellationToken)
        {
            try
            {
                await DownloadAndSaveImageAsync(containerName, key, localFolderPath, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Downloading Image failed - {key}", ex);

            }
        }
    }
}

using Abesto.MediaToolKit.Functions.Cloud;
using Amazon.S3;
using Amazon.S3.Model;

namespace Abesto.MediaToolKit.Functions.Amazon
{
    public class AmazonStorageClient(IAmazonS3 s3Client) : ICloudStorageClient
    {
        private readonly IAmazonS3 _s3Client = s3Client;

        public async Task<object> GetAllAsync(string container, IDictionary<string, object> properties)
        {
            return await _s3Client.GetAllObjectKeysAsync(container, "", properties);
        }

        public async Task<object> GetAsync(string container, string key, CancellationToken cancellationToken)
        {
            return await _s3Client.GetObjectAsync(container, key, cancellationToken);
        }

        //TODO Use Generics instead of object
        public async Task<object> GetPaginatedResultsAsync<TRequest>(object request, CancellationToken cancellationToken)
        {
            return await _s3Client.ListObjectsV2Async(request as ListObjectsV2Request, cancellationToken);
        }
    }
}

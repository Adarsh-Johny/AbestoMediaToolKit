using Amazon.S3.Model;

namespace Abesto.MediaToolKit.Functions.ImageProcessor
{
    public static class ImageProcessorFunctionExtensions
    {
        public static ListObjectsV2Request CreateAmazonS3Request(string containerName,int maxFilesPerAPI)
        {
            return new ListObjectsV2Request
            {
                BucketName = containerName,
                ContinuationToken = null,
                MaxKeys = maxFilesPerAPI
            };
        }

        public static ImageProcessRequest CreateImageProcessRequest(this IEnumerable<S3Object> images,
            ImageConfiguration imageConfiguration)
            => new()
            {
                Configuration = imageConfiguration,
                Images = images.ToList(),
            };

        public static ImageProcessRequest CreateImageProcessRequest(this S3Object image, ImageProcessRequest request)
            => new()
            {
                Images = [image],
                Configuration = request.Configuration,
            };
    }
}

using Amazon.S3.Model;

namespace Abesto.MediaToolKit.Functions.Utilities
{
    public static class ImageIdentifier
    {
        //TODO move this to config
        private static string[] _imageFormats = { ".bmp", ".gif", ".jpeg", ".jpg", ".png" };
        public static IEnumerable<S3Object> FindImages(this List<S3Object>? s3Objects)
        {
            if (s3Objects == null || s3Objects.Count == 0)
            {
                yield break;
            }
            foreach (var image in s3Objects.Where(x => x.Size > 0))
            {
                var fileNameParts = image.Key.Trim('/').Split('/');
                var extention = Path.GetExtension(fileNameParts.Last());
                if (_imageFormats.Contains(extention))
                {
                    yield return image;
                }
            }
        }
    }
}

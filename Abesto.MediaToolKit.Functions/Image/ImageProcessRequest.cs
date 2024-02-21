using Amazon.S3.Model;

namespace Abesto.MediaToolKit.Functions.Image
{
    public class ImageProcessRequest
    {
        public ImageProcessRequest() => Images = [];
        public List<S3Object> Images { get; set; }
        public ImageConfiguration Configuration { get; set; }
    }
}

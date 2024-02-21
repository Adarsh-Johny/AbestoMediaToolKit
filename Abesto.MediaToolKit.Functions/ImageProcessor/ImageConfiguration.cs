namespace Abesto.MediaToolKit.Functions.ImageProcessor
{
    public class ImageConfiguration
    {
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public bool MaintainAspectRatio { get; set; }
        public bool CanCrop { get; set; }
        public string FileSuffix { get; set; } = "_thumb";
    }
}

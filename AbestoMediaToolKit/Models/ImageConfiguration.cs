namespace Abesto.MediaToolKit.API.Models
{
    public class ImageConfiguration
    {
        public MediaType MediaType { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public bool MaintainAspectRatio { get; set; } = true;
        public bool CanCrop { get; set; } = true;
        public string FileSuffix { get; set; } = "_thumb";
    }
}

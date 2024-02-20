namespace Abesto.MediaToolKit.API.Models
{
    public class ImageConfiguration
    {
        public MediaType MediaType { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public bool MaintainAspectRatio { get; set; } = false;
        public bool CanCrop { get; set; } = false;
        public string FilePrefix { get; set; } = "_thumb";
    }
}

namespace Abesto.MediaToolKit.Functions.Image
{
    public class ImageConfiguration
    {
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public bool MaintainAspectRatio { get; set; }
        public bool CanCrop { get; set; }
        public string FilePrefix { get; set; }
    }
}

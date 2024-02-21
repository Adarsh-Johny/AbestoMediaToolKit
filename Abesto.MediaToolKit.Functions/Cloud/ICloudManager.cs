namespace Abesto.MediaToolKit.Functions.Cloud
{
    public interface ICloudManager
    {
        ICloudStorageClient GetCloudClient(ResourceLocationType locationType);
    }
}

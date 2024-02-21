namespace Abesto.MediaToolKit.Functions.Cloud
{
    public class CloudManager(IDictionary<ResourceLocationType, Func<ICloudStorageClient>> cloudTypeFactories) : ICloudManager
    {
        private readonly IDictionary<ResourceLocationType, Func<ICloudStorageClient>> _cloudTypeFactories = cloudTypeFactories ?? throw new ArgumentNullException(nameof(cloudTypeFactories));

        public ICloudStorageClient GetCloudClient(ResourceLocationType locationType)
        {
            try
            {
                return _cloudTypeFactories[locationType].Invoke();
            }
            catch (KeyNotFoundException)
            {
                throw new InvalidOperationException($"Factory for {locationType} not found.");
            }
            catch
            {
                throw;
            }
        }
    }
}

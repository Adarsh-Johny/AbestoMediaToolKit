using Abesto.MediaToolKit.Functions.Cloud;
using Microsoft.Extensions.Configuration;

namespace Abesto.MediaToolKit.Functions.ImageProcessor.Functions
{
    public abstract class BaseImageProcessorFunction
    {
        protected readonly ICloudStorageClient _cloudStorageClient;
        protected readonly string ResourceLocationTypeKey = "ResourceLocationType";
        protected readonly string ContainerNameKey = "ContainerName";
        protected readonly string _containerName;

        protected BaseImageProcessorFunction(IConfiguration configuration,
            ICloudManager cloudManager)
        {
            _cloudStorageClient = cloudManager.GetCloudClient(configuration.GetValue<ResourceLocationType>(ResourceLocationTypeKey));
            _containerName = configuration.GetValue<string>(ContainerNameKey);
        }
    }

}

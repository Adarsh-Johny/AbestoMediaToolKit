using Abesto.MediaToolKit.Functions.Amazon;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;

namespace Abesto.MediaToolKit.Functions.Cloud
{
    public static  class CloudConfigurationService
    {
        public static void ConfigureCloudServices(this IServiceCollection services)
        {
            services.ConfigureAmazonService();

            services.AddTransient<ICloudStorageClient, AmazonStorageClient>();
            services.AddTransient<ICloudManager, CloudManager>(serviceProvider =>
            {
                var factories = new Dictionary<ResourceLocationType, Func<ICloudStorageClient>>
            {
                { ResourceLocationType.AmazonS3, () => new AmazonStorageClient(serviceProvider.GetRequiredService<IAmazonS3>()) }
            };
                return new CloudManager(factories);
            });
        }

    }
}

using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Abesto.MediaToolKit.Functions.Cloud.Amazon
{
    public static class AmazonConfigurationService
    {
        public static void ConfigureAmazonService(this IServiceCollection services)
        {
            services.AddTransient<IAmazonS3>((serviceProvider) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var accessKey = configuration.GetValue<string>("AWS_Accesskey");
                var secret = configuration.GetValue<string>("AWS_Secret_Key");
                var additionalConfig = new AmazonS3Config() { RegionEndpoint = RegionEndpoint.EUCentral1 }; //TODO Move this to config
                return new AmazonS3Client(accessKey, secret, additionalConfig);
            });
        }
    }
}

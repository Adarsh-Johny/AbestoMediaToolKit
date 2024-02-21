using Abesto.MediaToolKit.Functions.Cloud;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(),"logs.txt"), rollingInterval: RollingInterval.Day) //TODO Make it from configuration
            .CreateLogger();

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

        services.ConfigureCloudServices();
    })
    .Build();

host.Run();


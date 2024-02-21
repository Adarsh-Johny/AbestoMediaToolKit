using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace Abesto.MediaToolKit.Functions.Image.Functions
{
    public class ImageProcessingOrchestratorFunction(ILoggerFactory loggerFactory)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<ImageProcessingOrchestratorFunction>();
        private readonly string _functionName = "ProcessImageActivityFunction";

        [Function("ImageProcessingOrchestrator")]
        public async Task<List<string>> RunOrchestratorAsync([OrchestrationTrigger] TaskOrchestrationContext context)
        {
            _logger.LogInformation(" --- Orchestrator Execution Started --- ");
            var outputs = new List<string>();

            var request = context.GetInput<ImageProcessRequest>() ?? new();

            foreach (var image in request.Images.Where(x => x.Size > 0))
            {
                await context.CallActivityAsync<string>(_functionName, image.CreateImageProcessRequest(request));
                outputs.Add(image.Key);
            }

            _logger.LogInformation(" --- Orchestrator Execution Completed --- ");

            return outputs;
        }
    }

}

namespace Abesto.MediaToolKit.Functions.Cloud
{
    public interface ICloudStorageClient
    {
        Task<object> GetAsync(string container, string key, CancellationToken cancellationToken);

        Task<object> GetAllAsync(string container, IDictionary<string, object> propeties);

        Task<object> GetPaginatedResultsAsync<TRequest>(object request,CancellationToken cancellationToken);
    }
}

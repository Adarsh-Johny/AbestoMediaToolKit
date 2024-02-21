using Abesto.MediaToolKit.API.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Abesto.MediaToolKit.API
{
    public interface IApiClient
    {
        [Post("/api/process-image")]
        Task<IActionResult> ResizeMedia([FromBody] ImageConfiguration imageConfiguration);
    }
}

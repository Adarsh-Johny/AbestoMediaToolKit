using Abesto.MediaToolKit.API.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Diagnostics;

namespace Abesto.MediaToolKit.API.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IApiClient apiClient) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IApiClient _apiClient = apiClient;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResizeMedia(ImageConfiguration iImageConfiguration)
        {
            try
            {
                _apiClient.ResizeMedia(iImageConfiguration);

                // TODO: Process the results and show the progress UI

                return RedirectToAction("Index");
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Content);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System.Diagnostics;
using CoreMentoringApp.WebSite.Logging;
using CoreMentoringApp.WebSite.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreMentoringApp.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            

            var message = "Please, see log file to find additional information. Use Request ID to enhance your research.";

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null)
            {
                var path = exceptionHandlerPathFeature.Path;
                _logger.LogError(LogEvents.UnhandledException,exceptionHandlerPathFeature.Error, "An unhandled exception occurred while processing the request {requestId}. Request resource {path}", requestId, path);

            }
            return View(new ErrorViewModel { RequestId = requestId, Message = message});
        }
    }
}

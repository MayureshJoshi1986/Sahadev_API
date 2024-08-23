using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleMVCCoreProject.Models;
using System.Diagnostics;

namespace SampleMVCCoreProject.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// HomeController constructor
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// HomeController index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// HomeController privacy method
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// HomeController error method
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using BeautySalon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BeautySalon.Controllers
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
        public IActionResult Nails()
        {
            return View();
        }
        public IActionResult MakeUp()
        {
            return View();
        }
        public IActionResult Galery()
        {
            return View();
        }
        public IActionResult Clients()
        {
            return View();
        }
        public IActionResult Contacts()
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

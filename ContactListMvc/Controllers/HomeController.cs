using ContactListMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mime;

namespace ContactListMvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private int _value = 0;

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
            // transfer executie catre model

            // obtin date de la model

            // le pun pe view
            return View();
        }

        public IActionResult Increment()
        {
            // transfer executie catre model
            _value++;

            // obtin date de la model
            IncrementViewModel data = new IncrementViewModel
            {
                Value = _value
            };

            // le pun pe view
            // return Content($"Value: {_value}", MediaTypeNames.Text.Plain);
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // transfer executie catre model (no-op)

            // obtin date de la model
            ErrorViewModel data = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            // le pun pe view
            return View(data);
        }
    }
}
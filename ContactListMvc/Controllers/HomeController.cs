using ContactListMvc.Models;
using ContactListMvc.Web.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mime;
using System.Text;

namespace ContactListMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompanySettings _companySettings;
        private readonly CompanySettings _companySettingsSnapshot;
        private readonly IConfiguration _configuration;

        private int _value = 0;

        public HomeController(
            ILogger<HomeController> logger, 
            IOptions<CompanySettings> options,
            IOptionsSnapshot<CompanySettings> optionsSnapshot,
            IConfiguration configuration)
        {
            _logger = logger;
            _companySettings = options.Value;
            _companySettingsSnapshot = optionsSnapshot.Value;
            _configuration = configuration;
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

        public IActionResult CompanyInfo()
        {
            CompanySettings? dynamicReadSettings = _configuration.GetSection("Company")
                                                                 .Get<CompanySettings>();

            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine($"[with options pattern] Company name= {_companySettings.Name}");
            sbOutput.AppendLine($"[with options snapshot] Company name= {_companySettingsSnapshot.Name}");
            sbOutput.AppendLine($"[with configration root] Company name= {dynamicReadSettings?.Name}");

            return Content(sbOutput.ToString(), MediaTypeNames.Text.Plain);
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
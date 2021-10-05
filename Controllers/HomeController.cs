using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KeyVaultMsiWebAppCore.Models;
using Microsoft.Extensions.Configuration;

namespace KeyVaultMsiWebAppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        private IConfiguration Configuration;

        public HomeController(IConfiguration _configuration, ILogger<HomeController> logger)
        {
            Configuration = _configuration;

            Logger = logger;
        }

        public IActionResult Index()
        {
            var section = this.Configuration.GetSection("AppSettings");
            string keyVaultName = section.GetValue<string>("KeyVaultName");
            string secretName = section.GetValue<string>("SecretName");

            ViewBag.MyKeyVaultName = keyVaultName;
            ViewBag.MySecretName = secretName;

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

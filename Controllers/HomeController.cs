using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KeyVaultMsiWebAppCore.Models;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

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
            string keyVaultName = this.Configuration.GetValue<string>("KeyVaultName");
            string secretName = this.Configuration.GetValue<string>("SecretName");            

            ViewBag.MyKeyVaultName = keyVaultName;
            ViewBag.MySecretName = secretName;

            string secretValue;

            try
            {
                var kvUri = "https://" + keyVaultName + ".vault.azure.net";
    
                var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

                KeyVaultSecret secret = client.GetSecret(secretName);
                secretValue = secret.Value;

            }
            catch (System.Exception)
            {   
                secretValue = "Unable to fetch";
            }

            ViewBag.SecretValue = secretValue;

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

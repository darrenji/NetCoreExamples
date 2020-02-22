using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestEase;
using TotalNetCore.RestEaseExample.Interfaces;
using TotalNetCore.RestEaseExample.Models;

namespace TotalNetCore.RestEaseExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGitHupApi _api;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _api = RestClient.For<IGitHupApi>("https://api.github.com");
        }

        public string Index()
        {
            string restult = string.Empty;

            User user = _api.GetUserAsync("darrenji").Result;
            restult = $"Name: {user.Name}. Blog: {user.Blog}. CreatedAt: {user.CreatedAt}";

            return restult;


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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Start()
        {
            HandleCookie("ServerIsSelected", "false");
            return View();
        }
        public IActionResult Startup()
        {
            HandleCookie("ServerIsSelected", "false");
            return RedirectToAction("Start");
        }
        public void HandleCookie(string name, string content)
        {
            HttpContext.Response.Cookies.Append(name, content, new CookieOptions()
            {
                Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0))

            });
        }
    }

}

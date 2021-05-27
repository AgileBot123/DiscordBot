using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.WebClient.ClientLogic;
using System.Threading.Tasks;

namespace ModBot.WebClient.Controllers
{
    public class AuthenticationController : Controller
    {

        private GuildLogic _logic;

        
        public AuthenticationController()
        {
            _logic = new GuildLogic();
        }





        public IActionResult Index()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "Discord")]
        public IActionResult Authentication()
        {

            ViewBag.Test = HttpContext.User.Identity.Name;
            var servers = _logic.GetUserServerAsync().Result;
            ViewBag.Server1 = servers[0].Name;
            return View("ServerList", servers);
        }

        public async Task<IActionResult> Logout()
        {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
               // await HttpContext.SignOutAsync("Discord");
            return RedirectToAction("Start", "Home");
        }
    }
}

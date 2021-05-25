using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;



namespace ModBot.WebClient.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}

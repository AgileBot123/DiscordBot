using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.WebClient.ClientLogic;
using ModBot.WebClient.Models;
using ModBot.WebClient.Models.Endpoints;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.WebClient.Controllers
{
    public class AuthenticationController : Controller
    {

        private GuildLogic _logic;

        private readonly IEndpoints endpoints;

        public AuthenticationController()
        {
            _logic = new GuildLogic(this);
            endpoints = new Endpoints();
        }

        [HttpPost]
        public bool hasbot(ulong guildId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var guildDto = new GuildDto() { Id = guildId };
                    var JsonString = JsonConvert.SerializeObject(guildDto);
                    var stringContent = new StringContent(JsonString, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(endpoints.GetGuild, stringContent).Result;
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return false;

                    var JsonResultString = response.Content.ReadAsStringAsync().Result;
                    var guild = JsonConvert.DeserializeObject<GuildModel>(JsonResultString);
                    return guild.HasBot;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
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

        public IActionResult Dashboard()
        {
            return View();
        }
                    
    }
}

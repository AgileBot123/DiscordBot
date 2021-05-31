﻿using Discord;
using Discord.Rest;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.WebClient.ClientLogic;
using ModBot.WebClient.Extention;
using ModBot.WebClient.Models;
using ModBot.WebClient.Models.Endpoints;
using Newtonsoft.Json;
using System;
using System.Linq;
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
        public async Task<IActionResult> Authentication()
        {
            var token = await _logic.DiscordGetToken();
            Session.Set<string>(HttpContext.Session, "token", token);
            ViewBag.Test = HttpContext.User.Identity.Name;
            var servers = _logic.GetUserServerAsync(Session.Get<string>(HttpContext.Session, "token")).Result;
            return View("ServerList", servers);
        }

        public async Task<IActionResult> Logout()
        {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
               // await HttpContext.SignOutAsync("Discord");
            return RedirectToAction("Start", "Home");
        }

        public IActionResult Dashboard(ulong guildId)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Initializeguild([FromQuery(Name = "Guild_Id")] ulong guildId)
        {
            try
            {
                DiscordRestClient discordRestClient = new DiscordRestClient();
                await discordRestClient.LoginAsync(TokenType.Bearer, Session.Get<string>(HttpContext.Session, "token"));

                var GuildInformation = discordRestClient.GetGuildSummariesAsync();

                var guildModel = new GuildModel();
                await foreach (var guild in GuildInformation)
                {
                    foreach (var item in guild.Where(x => x.Id == guildId))
                    {
                      guildModel = new GuildModel(item.Id, item.Name, item.IconUrl, hasbot(item.Id));                       
                    }
                }

                if (guildModel != null)
                {
                    using (var client = new HttpClient())
                    {
                        var guildDto = new GuildDto()
                        {
                            Id = guildModel.Id,
                            Icon = guildModel.Icon,
                            HasBot = guildModel.HasBot,
                            Name = guildModel.Name
                        };
                        var JsonString = JsonConvert.SerializeObject(guildDto);
                        var stringContent = new StringContent(JsonString, Encoding.UTF8, "application/json");
                        var response = client.PostAsync(endpoints.CreateGuild, stringContent).Result;
                    }
                }
                return RedirectToAction("Dashboard");
            }
            catch(Exception ex)
            {
                throw new Exception("internal error");
            }
        }
    }
}
